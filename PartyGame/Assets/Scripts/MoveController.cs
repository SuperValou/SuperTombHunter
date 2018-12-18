using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Player player;

    public int JoystickNumber;
    public float speed = 5f;

    private string HorizontalAxis;
    private string VerticalAxis;
    private string GrabAxis;

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
            Debug.Log("P" + JoystickNumber + " grabbing stuff");
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
    }
}
