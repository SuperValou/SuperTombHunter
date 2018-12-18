using Assets.Scripts.Teams;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Player player;

    public int JoystickNumber;
    public float speed = 5f;

    private string HorizontalAxis;
    private string VerticalAxis;
    private string GrabAxis;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError($"No {nameof(Player)} is attached to {this}.");
        }
    }

    public void InitAxis()
    {
        HorizontalAxis = "P" + JoystickNumber + "_Horizontal";
        VerticalAxis = "P" + JoystickNumber + "_Vertical";
        GrabAxis = "P" + JoystickNumber + "_Grab";
    }

    void Update()
    {
        if (Input.GetButtonDown(GrabAxis))
        {
            Debug.Log("P" + JoystickNumber + " is performing an action");
            player.GrabDropAction();
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis(HorizontalAxis);
        float vertical = Input.GetAxis(VerticalAxis);

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        Vector3 acceleration = movement * speed * Time.deltaTime;
        gameObject.transform.Translate(acceleration);

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
        bool flipSprite = spriteRenderer.flipX ? horizontal < 0.01f : horizontal > 0.01f;
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
