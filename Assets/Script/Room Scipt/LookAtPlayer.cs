using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Rotation Options")]
    public bool Rotate = true;
    public Vector3 rotationOffset;

    void Update()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position;

        if (Rotate)
            targetPosition.y = transform.position.y;

        transform.LookAt(targetPosition);

        transform.Rotate(rotationOffset);
    }
}
