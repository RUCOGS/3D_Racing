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

    // Rigidbody
    private Rigidbody RBody;

    // Raycast
    public RaycastHit hit;
    bool grounded;

    public float raycastDistance = 10f;  
    int groundLayer = 8;
    int vehicleLayer = 9;
    int groundMask;

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


    
    private void Awake()
    {
        RBody = GetComponent<Rigidbody>();
        groundMask = 1 << groundLayer;
    }

    private void Start()
    {
        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";
    }

    private void Update()
    {
        MovementInputVal = Input.GetAxis(MovementAxisName);
        TurnInputVal = Input.GetAxis(TurnAxisName);
    }

    private void FixedUpdate()
    {
        Raycast();
        Hover();
        Turn();
        Move();
    }

    private void Raycast()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        grounded = Physics.Raycast(transform.position, dwn, out hit, raycastDistance, groundMask);
        //Debug.Log("Raycast hit " + hit.distance);
    }

    //Orient vehicle to ground
    private void OrientVertical()
    {
        float distanceToFloor = hit.distance;
        Vector3 groundNormal = hit.normal;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, .2f);
    }


    private void Hover()
    {
        OrientVertical();

        float distanceToFloor = hit.distance;
        Debug.Log(hit.distance);

        //Decide if default gravity is needed
        Vector3 HoverForce = Vector3.zero;
        if (grounded)
        {
            HoverForce = transform.up * gravityScale * Mathf.Pow((HoverHeight / distanceToFloor), 3);
        }
        HoverForce += -transform.up * gravityScale;
        RBody.AddForce(HoverForce);
    }

    private void Move()
    {
        Vector3 movement = transform.forward * MovementInputVal * Acceleration;

        if(RBody.velocity.magnitude < MaxSpeed && hit.distance != 0)
            RBody.AddForce(movement);
    }

    private static float prevYRot = Quaternion.identity.eulerAngles.y;

    private void Turn()
    {
        float turn = TurnInputVal * TurnSpeed;
        transform.RotateAround(transform.position, transform.up, turn);
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
}
