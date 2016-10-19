using UnityEngine;
using System.Collections;

public class PlayerSteer : MonoBehaviour {

    private bool floored = false;

    // Vehicle parameters
    public float MaxSpeed;
    public float TurnSpeed;
    public float HoverHeight;
    public float Acceleration;

    private Rigidbody RBody;

    //Hover values
    public float gravityScale;
    public float dampningScale;

    //Input variables
    private string MovementAxisName;
    private string TurnAxisName;
    private float MovementInputVal;
    private float TurnInputVal;

    private OrientVehicle Orient;
    
    private void Awake()
    {
        RBody = GetComponent<Rigidbody>();
        Orient = GetComponent<OrientVehicle>();
    }

    private void OnEnable()
    {
        RBody.isKinematic = false;

        MovementInputVal = 0f;
        TurnInputVal = 0f;
    }

    private void OnDisable()
    {
        RBody.isKinematic = true;
    }

    private void Start()
    {
        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";
    }



    private void FixedUpdate()
    {
        Move();
        Turn();
        Hover();
    }


    private void Hover()
    {
        float distanceToFloor = Orient.GroundDistance;
        Vector3 gravity = (-transform.up) * gravityScale;
        Vector3 upForce = Orient.normal * (HoverHeight - distanceToFloor) * gravityScale;
        Vector3 HoverForce = (gravity + upForce);
        //Debug.Log(gravity +  "  "  + upForce + " " + distanceToFloor);
        RBody.AddForce(HoverForce);
    }

    private void Update()
    {
        MovementInputVal = Input.GetAxis(MovementAxisName);
        TurnInputVal = Input.GetAxis(TurnAxisName);
        Debug.Log(TurnInputVal != 0);
    }

    private void Move()
    {
        Vector3 movement = transform.forward * MovementInputVal * Acceleration;

        if(RBody.velocity.magnitude < MaxSpeed)
            RBody.AddForce(movement);
    }

    private void Turn()
    {
        float turn = TurnInputVal * TurnSpeed;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        RBody.MoveRotation(RBody.rotation * turnRotation);
    }
}
