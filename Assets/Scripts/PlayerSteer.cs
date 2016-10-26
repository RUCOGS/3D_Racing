using UnityEngine;
using System.Collections;

public class PlayerSteer : MonoBehaviour {

    private bool floored = false;

    // Vehicle parameters
    public float MaxSpeed;
    public float TurnSpeed;
    public float HoverHeight;
    public float Acceleration;
    public float HoverHeightThreshold;

    private Rigidbody RBody;

    //Hover values
    public float gravityScale;
    public float dampningScale;

    private Vector3 gravDirCache;
    public float defaultGravScale;

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
        //Debug.Log(distanceToFloor + " " + HoverHeight);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, Orient.normal) * transform.rotation, .5f);

        //Decide if default gravity is needed
        Vector3 HoverForce = Vector3.zero;
        if (distanceToFloor != 0)
        {
            
            if (distanceToFloor > HoverHeight + HoverHeightThreshold)
            {
                HoverForce = -transform.up * gravityScale * Mathf.Abs(HoverHeight - distanceToFloor);
                gravDirCache = -transform.up;
            }
            else if (distanceToFloor < HoverHeight - HoverHeightThreshold)
            {
                HoverForce = transform.up * gravityScale * gravityScale * Mathf.Abs(HoverHeight - distanceToFloor);
            }
            
        }
        else
        {
            HoverForce = gravDirCache * defaultGravScale;
        }
        RBody.AddForce(HoverForce);
    }

    private void Update()
    {
        MovementInputVal = Input.GetAxis(MovementAxisName);
        TurnInputVal = Input.GetAxis(TurnAxisName);
    }

    private void Move()
    {
        Vector3 movement = transform.forward * MovementInputVal * Acceleration;

        if(RBody.velocity.magnitude < MaxSpeed && Orient.GroundDistance != 0)
            RBody.AddForce(movement);
    }

    private static float prevYRot = Quaternion.identity.eulerAngles.y;

    private void Turn()
    {
        float turn = TurnInputVal * TurnSpeed;
        transform.RotateAround(transform.position, transform.up, turn);
    }
}
