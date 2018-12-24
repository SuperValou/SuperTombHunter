using Assets.Scripts.Teams;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Player player;
    public float speed = 5f;

    private string horizontalAxis;
    private string verticalAxis;
    private string grabAxis;

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
            horizontalAxis = "P" + joystickNumber + "_Horizontal";
            verticalAxis = "P" + joystickNumber + "_Vertical";
            grabAxis = "P" + joystickNumber + "_Grab";
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
        if (Input.GetButtonDown(grabAxis))
        {
            player.GrabDropAction();
        }

        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        Vector3 acceleration = movement * speed * Time.deltaTime;
        Vector3 newPos = gameObject.transform.position + acceleration;
        rigidbody2d.MovePosition(newPos);

        FlipSprite(horizontal);

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
