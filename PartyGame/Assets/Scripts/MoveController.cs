using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int PlayerNumber;

    public float speed = 5f;

    private string HorizontalAxis;
    private string VerticalAxis;
    private string GrabAxis;

    void Start()
    {
        InitAxis();
        string[] joysticks = Input.GetJoystickNames();
        for (int i = 0; i < joysticks.Length; i++)
        {
            if (joysticks[i].Length == 0) continue;
            Debug.Log("joystick" + (i + 1) + " is a " + joysticks[i]);
        }
    }

    private void InitAxis()
    {
        HorizontalAxis = "P" + PlayerNumber + "_Horizontal";
        VerticalAxis = "P" + PlayerNumber + "_Vertical";
        GrabAxis = "P" + PlayerNumber + "_Grab";
    }

    void Update()
    {
        if (Input.GetButtonDown(GrabAxis))
        {
            Debug.Log("P" + PlayerNumber + " grabbing stuff");
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
