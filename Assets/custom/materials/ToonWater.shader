Shader "Custom/ToonPoolWaterShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.0, 0.5, 0.8, 1.0)
        _Transparency ("Transparency", Range(0, 1)) = 0.7
        _WaveSpeed ("Wave Speed", Range(0.1, 3.0)) = 1.0
        _WaveFrequency ("Wave Frequency", Range(0.1, 30.0)) = 10.0
        _Shininess ("Shininess", Range(0, 1)) = 0.5
        _EdgeFoamColor ("Foam Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _FoamWidth ("Foam Width", Range(0.0, 1.0)) = 0.0
        _PoolMin ("Pool Min Bounds", Vector) = (-5.0, 0.0, -5.0)
        _PoolMax ("Pool Max Bounds", Vector) = (5.0, 0.0, 5.0)
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            // Variables from Properties
            fixed4 _BaseColor;
            float _Transparency;
            float _WaveSpeed;
            float _WaveFrequency;
            float _Shininess;
            fixed4 _EdgeFoamColor;
            float _FoamWidth;
            float3 _PoolMin;
            float3 _PoolMax;

            // Vertex Shader
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.vertex.xz * _WaveFrequency;
                return o;
            }

            // Fragment Shader
            fixed4 frag(v2f i) : SV_Target
            {
                // Base color with transparency
                fixed4 color = _BaseColor;
                color.a = _Transparency;

                // Toon shading (step shading for a flat look)
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float NdotL = dot(i.worldNormal, lightDir);
                float toonShade = saturate(floor(NdotL * 3.0) / 3.0);
                color.rgb *= toonShade;

                // Animated wave effect with sine waves
                float waveX = sin(i.worldPos.x * _WaveFrequency + _Time.y * _WaveSpeed) * 0.1;
                float waveZ = cos(i.worldPos.z * _WaveFrequency + _Time.y * _WaveSpeed) * 0.1;
                float wave = waveX + waveZ;

                // Apply wave effect
                color.rgb += wave * 0.1;

                // Refraction effect (distortion based on wave movement)
                float refraction = wave * 0.05;
                color.rgb = lerp(color.rgb, _BaseColor.rgb * 0.8, refraction);

                // Edge foam effect (around edges based on defined pool boundaries)
                float distToEdgeX = min(abs(i.worldPos.x - _PoolMin.x), abs(i.worldPos.x - _PoolMax.x));
                float distToEdgeZ = min(abs(i.worldPos.z - _PoolMin.z), abs(i.worldPos.z - _PoolMax.z));
                float edgeDistance = min(distToEdgeX, distToEdgeZ) / _FoamWidth;
                float foamFactor = saturate(1.0 - edgeDistance);

                // Blend foam color based on edge distance
                color.rgb = lerp(color.rgb, _EdgeFoamColor.rgb, foamFactor);

                // Specular highlight for shininess
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float specular = pow(saturate(dot(reflect(-lightDir, i.worldNormal), viewDir)), _Shininess * 128);
                color.rgb += specular * _Shininess;

                return color;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
