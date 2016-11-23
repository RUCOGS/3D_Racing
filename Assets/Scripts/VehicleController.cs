using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    // Wheels
    public Rigidbody FLWheel;
    public Rigidbody FRWheel;

    // Physics
    Rigidbody RBody;
    public float Acceleration;
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
        RBody.AddForce(transform.right * MovementInputVal * Acceleration, ForceMode.Acceleration);
        //Debug.Log("Speed = " + RBody.velocity);
    }

    void Turn()
    {
        float turn = TurnInputVal * TurnSpeed;
        FLWheel.AddForce(FLWheel.transform.right * turn);
        FRWheel.AddForce(FRWheel.transform.right * turn);
        //transform.RotateAround(transform.position, transform.up, turn);
        //RBody.velocity = velocity;
    }
}
