using UnityEngine;

public class Running : MonoBehaviour
{
    [Header("Sprint Toggle")]
    public bool enableSprint = true;
    public bool unlimitedSprint = false;

    [Header("Keybind")]
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Sprint Speed & Duration")]
    public float sprintSpeed = 7f;
    public float sprintDuration = 2.5f;
    public float sprintCooldownTime = 2.5f;

    [Header("Sprint Bar")]
    public bool useSprintBar = true;
    public RectTransform sprintBar;
    public float maxBarWidth = 975f;

    [Header("Running Audio")]
    public AudioSource runningSource;
    public AudioClip runningClip;

    private float sprintRemaining;
    public bool isSprinting = false;
    private bool isCooldown = false;
    private float cooldownTimer;

    private Movement playerMovement;
    private float originalSpeed;

    void Start()
    {
        playerMovement = GetComponent<Movement>();
        if (playerMovement != null)
            originalSpeed = playerMovement.moveSpeed;

        sprintRemaining = sprintDuration;
    }

    void Update()
    {
        Sprinting();
        SprintBar();
        RunningAudio();
    }

    void Sprinting()
    {
        if (!enableSprint || playerMovement == null) return;

        bool sprintKeyDown = Input.GetKey(sprintKey);

        if (sprintKeyDown && !isCooldown && sprintRemaining > 0)
        {
            isSprinting = true;
            playerMovement.moveSpeed = sprintSpeed;
            sprintRemaining -= Time.deltaTime;

            if (sprintRemaining <= 0)
            {
                sprintRemaining = 0;
                isCooldown = true;
                cooldownTimer = sprintCooldownTime;
            }
        }
        else
        {
            isSprinting = false;
            playerMovement.moveSpeed = originalSpeed;

            if (sprintRemaining < sprintDuration && !isCooldown)
            {
                sprintRemaining += Time.deltaTime;
                if (sprintRemaining > sprintDuration)
                    sprintRemaining = sprintDuration;
            }

            if (isCooldown)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    isCooldown = false;
                }
            }
        }
    }

        void SprintBar()
    {
        if (!useSprintBar || unlimitedSprint || sprintBar == null) return;

        float percent = sprintRemaining / sprintDuration;
        float width = maxBarWidth * percent;

        // Center-based shrink: set pivot to 0.5 (center), adjust width
        sprintBar.sizeDelta = new Vector2(width, sprintBar.sizeDelta.y);
    }

    void RunningAudio()
    {
        if (playerMovement == null) return;

        CharacterController controller = playerMovement.GetComponent<CharacterController>();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isMoving = controller.isGrounded && new Vector2(h, v).magnitude > 0.1f;

        if (isSprinting && isMoving)
        {
            if (!runningSource.isPlaying)
            {
                runningSource.clip = runningClip;
                runningSource.loop = true;
                runningSource.Play();
            }
        }
        else
        {
            if (runningSource.isPlaying)
                runningSource.Stop();
        }
    }
}