using UnityEngine;

public class DoorAutoTrigSide : MonoBehaviour
{
    [Header("Sliding Door Side")]
    public SlidingDoorSide slidingDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Open the door when the player enters the trigger zone
            slidingDoor.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Close the door when the player exits the trigger zone
            slidingDoor.Close();
        }
    }
}