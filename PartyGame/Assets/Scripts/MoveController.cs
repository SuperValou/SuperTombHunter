using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int JoystickNumber;

    public float speed = 5f;

    private string HorizontalAxis;
    private string VerticalAxis;
    private string GrabAxis;

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
