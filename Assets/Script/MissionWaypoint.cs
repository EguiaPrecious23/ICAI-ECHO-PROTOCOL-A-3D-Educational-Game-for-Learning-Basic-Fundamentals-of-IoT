using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    [Header("Waypoint Img")]
    public Image img;

    [Header("Switch")]
    public Transform targetSwitch;

    public Camera mainCamera;

    [Header("Meter")]
    public TMP_Text meter;
    public Vector3 offset;

    private void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = mainCamera.WorldToScreenPoint(targetSwitch.position + offset);

        if (Vector3.Dot((targetSwitch.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            } else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(targetSwitch.position, transform.position)).ToString() + "m";
    }
}