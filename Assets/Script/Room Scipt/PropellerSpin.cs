using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    [Header("Rotation Speed")]
    public float rotationSpeed = 150f;

    void Update()
    {
        transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
    }
}