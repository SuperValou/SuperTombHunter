using Assets.Scripts.Teams;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Player player;

    public int JoystickNumber;
    public float speed = 5f;

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

    public void InitAxis() {}

    void Update()
    {
        if (Input.GetButtonDown("P" + JoystickNumber + "_Grab"))
        {
            player.GrabDropAction();
        }

        float horizontal = Input.GetAxis("P" + JoystickNumber + "_Horizontal");
        float vertical = Input.GetAxis("P" + JoystickNumber + "_Vertical");

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
