using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    // Wheels
    public Rigidbody FLWheel;
    public Rigidbody FRWheel;

    // Physics
    Rigidbody RBody;
    public float acceleration;
    public float TurnSpeed;

    // Input
    private string MovementAxisName;
    private string TurnAxisName;
    private float MovementInputVal;
    private float TurnInputVal;

    // AWAKE
    void Awake ()
    {
        RBody = GetComponent<Rigidbody>();
    }

	// START
    private void Start()
    {
        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";
    }

    private void Update()
    {
        MovementInputVal = Input.GetAxis(MovementAxisName);
        TurnInputVal = Input.GetAxis(TurnAxisName);
        //Debug.Log(MovementInputVal + " " + TurnInputVal);
    }

    // UPDATE
    void FixedUpdate ()
    {
        Move();
        Turn();
	}

    // MOVEMENT
    void Move()
    {
        // For some reason the car is oriented the wrong way so we need to use the right vector
        RBody.AddForce(transform.right * MovementInputVal * acceleration, ForceMode.Acceleration);
        RBody.velocity = transform.TransformVector(Vector3.Scale(transform.InverseTransformVector(RBody.velocity), new Vector3(1, 1, 0)));
        //Debug.Log("Speed = " + RBody.velocity);
    }

    void Turn()
    {
        float turn = TurnInputVal * TurnSpeed;
        Vector3 offset = -transform.right * 3;
        //FLWheel.AddForce(FLWheel.transform.right * turn);
        //FRWheel.AddForce(FRWheel.transform.right * turn);
        transform.RotateAround(transform.position + offset, transform.up, turn);
        //RBody.velocity = velocity;
    }
}
