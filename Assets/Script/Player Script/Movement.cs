using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player Speed")]
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public bool allowLook = true;

    [Header("Player Audio")]
    public AudioSource walkingSource;
    public AudioClip walkingClip;

    private CharacterController controller;
    private float pitch = 0f;
    private Vector3 velocity;
    private Running runningScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        runningScript = GetComponent<Running>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        Move();
        Gravity();
        PlayerInput();
        WalkingAudio();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void WalkingAudio()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isMoving = controller.isGrounded && new Vector2(h, v).magnitude > 0.1f;

        if (isMoving && (runningScript == null || !runningScript.isSprinting))
        {
            if (!walkingSource.isPlaying)
            {
                walkingSource.clip = walkingClip;
                walkingSource.loop = true;
                walkingSource.Play();
            }
        }
        else
        {
            if (walkingSource.isPlaying)
                walkingSource.Stop();
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Gravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void PlayerInput()
    {
        if (PlayerNameInput.hasEnteredName)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            Look();
        }
    }
}
