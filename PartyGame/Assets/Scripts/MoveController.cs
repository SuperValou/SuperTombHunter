using Assets.Scripts.Teams;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private const float DashTimeToRecover = 1.5f;
    public Player player;
    public float speed = 5f;
    
    public int dashFrame = 6;
    public float dashSpeed = 10f;
    public float dashCooltime;

    private string horizontalAxis;
    private string verticalAxis;
    private string grabAxis;
    private string dashAxis;

    private float _horizontalInput;
    private float _verticalInput;

    private int triggerDash;
    public bool IsDashing
    {
        get { return triggerDash > 0; }
    }

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError($"No {nameof(Player)} is attached to {this}.");
        }
    }

    public void InitAxis(ControllerType type, int joystickNumber = 0)
    {
        if (type == ControllerType.Pad)
        {
            horizontalAxis = $"P{joystickNumber}_Horizontal";
            verticalAxis = $"P{joystickNumber}_Vertical";
            grabAxis = $"P{joystickNumber}_Grab";
            dashAxis = $"P{joystickNumber}_Dash";
        }
        else if (type == ControllerType.Keyboard)
        {
            horizontalAxis = "Keyboard_Horizontal";
            verticalAxis = "Keyboard_Vertical";
            grabAxis = "Keyboard_Grab";
        }
    }

    void Update()
    {
        dashCooltime -= Time.deltaTime;

        if (Input.GetButtonDown(grabAxis))
        {
            player.GrabDropAction();
        }

        _horizontalInput = Input.GetAxis(horizontalAxis);
        _verticalInput = Input.GetAxis(verticalAxis);
        
        Vector3 movement = new Vector3(_horizontalInput, _verticalInput, 0f).normalized;

        if (Input.GetButtonDown(dashAxis))
        {
            Debug.Log("Dashing");
            DashAction();
        }


        FlipSprite(_horizontalInput);

        if (movement.y > 0.01f)
        {
            animator.SetBool("moveUp", true);
            animator.SetBool("moveDown", false);
            animator.SetBool("moveHorizontaly", false);
        }

        if (movement.y < -0.01f)
        {
            animator.SetBool("moveUp", false);
            animator.SetBool("moveDown", true);
            animator.SetBool("moveHorizontaly", false);
        }

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetBool("moveUp", false);
            animator.SetBool("moveDown", false);
            animator.SetBool("moveHorizontaly", true);
        }

        if (Mathf.Abs(movement.x) < 0.01f && Mathf.Abs(movement.y) < 0.01f)
        {
            animator.SetBool("moveUp", false);
            animator.SetBool("moveDown", false);
            animator.SetBool("moveHorizontaly", false);
        }
    }

    private void DashAction()
    {
        if (dashCooltime > 0f) return;
        dashCooltime = DashTimeToRecover;
        triggerDash = dashFrame;
    }

    void FixedUpdate()
    {
        var frameSpeed = speed;
        if (triggerDash > 0)
        {
            triggerDash--;
            frameSpeed = dashSpeed;
        }

        Vector3 movement = new Vector3(_horizontalInput, _verticalInput, 0f).normalized;
        Vector3 acceleration = movement * frameSpeed * Time.deltaTime;
        Vector3 newPos = gameObject.transform.position + acceleration;
        rigidbody2d.MovePosition(newPos);
    }

    void FlipSprite(float horizontal)
    {
        if (horizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
