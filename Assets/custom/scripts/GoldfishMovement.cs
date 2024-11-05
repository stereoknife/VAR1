using UnityEngine;

public class GoldfishMovement : MonoBehaviour
{
    public float speed = 0.5f;             // Speed of the goldfish
    private Vector3 targetPosition;

    public GameObject referenceObjectMax;  // The upper bound of movement
    public GameObject referenceObjectMin;  // The lower bound of movement

    void Start()
    {
        SetNewTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();

        // If the goldfish is close to its target, set a new target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        // Calculate the movement bounds based on the positions of referenceObjectMin and referenceObjectMax
        float minX = referenceObjectMin.transform.position.x;
        float maxX = referenceObjectMax.transform.position.x;
        float minZ = referenceObjectMin.transform.position.z;
        float maxZ = referenceObjectMax.transform.position.z;

        // Randomize a new position within the specified bounds
        targetPosition = new Vector3(
            Random.Range(minX, maxX),
            transform.position.y,  // Keep the y position unchanged
            Random.Range(minZ, maxZ)
        );
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
