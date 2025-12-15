using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [Header("Door Slides")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("Distance & Speed")]
    public float slideDistance = 3f;
    public float slideSpeed = 2f;

    public bool open = false;

    private Vector3 leftClosedPos, leftOpenPos;
    private Vector3 rightClosedPos, rightOpenPos;

    void Start()
    {
        leftClosedPos = leftDoor.localPosition;
        rightClosedPos = rightDoor.localPosition;

        leftOpenPos = leftClosedPos + Vector3.left * slideDistance;
        rightOpenPos = rightClosedPos + Vector3.right * slideDistance;
    }

    void Update()
    {
        Vector3 leftTarget = open ? leftOpenPos : leftClosedPos;
        Vector3 rightTarget = open ? rightOpenPos : rightClosedPos;

        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftTarget, slideSpeed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightTarget, slideSpeed * Time.deltaTime);
    }

    public void ToggleDoor() => open = !open;
    public void Open() => open = true;
    public void Close() => open = false;
}