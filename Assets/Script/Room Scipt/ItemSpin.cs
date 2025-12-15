using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    [Header("Rotation Speed")]
    public float rotationSpeed = 75f;

    void Update()
    {
        transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
    }
}