using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    [Header("Light")]
    public Light spotLight;

    [Header("Keybind")]
    public KeyCode toggleKey = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            spotLight.enabled = !spotLight.enabled;
        }
    }
}