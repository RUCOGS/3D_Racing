using UnityEngine;
using System.Collections;
using System;

public class WheelHover : MonoBehaviour {

    // Physics
    Rigidbody CarBody;
    Rigidbody RBody;
    Vector3 fallDirection;
    public float gravityScale;
    public float HoverHeight;
    public float HoverThreshold;

    // Raycast
    public RaycastHit hit;
    bool grounded;

    public float raycastDistance = 10f;
    int groundLayer = 8;
    int vehicleLayer = 9;
    int groundMask;

    // AWAKE
    void Awake()
    {
        grounded = false;
        groundMask = 1 << groundLayer;
    }

	// Use this for initialization
	void Start () {
        fallDirection = -transform.up;
        CarBody = GetComponentInParent<Rigidbody>();
        RBody = GetComponent<Rigidbody>();
    }
	
	// FIXED UPDATE
	void FixedUpdate () {
        Raycast();
        Hover();
	}

    // Perform raycast
    private void Raycast()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        grounded = Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, groundMask);
        //Debug.Log("Raycast hit " + hit.distance);
    }

    //Orient vehicle to ground
    private void OrientVertical()
    {
        float distanceToFloor = hit.distance;
        Vector3 groundNormal = hit.normal;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, .1f);
        Debug.DrawRay(transform.position, -transform.up);
    }

    // Apply hover forces
    private void Hover()
    {
        OrientVertical();

        float distanceToFloor = hit.distance;
        //Debug.Log(hit.distance);

        //Decide if default gravity is needed
        Vector3 HoverForce = Vector3.zero;
        if (grounded)
        {
            if (distanceToFloor < HoverThreshold)
            {
                transform.Translate(transform.up * (HoverThreshold - distanceToFloor));
            }
            HoverForce = transform.up * gravityScale * Mathf.Pow((HoverHeight / distanceToFloor), 1);
        }
        HoverForce += -transform.up * gravityScale;
        //Debug.Log(HoverForce+" "+hit.distance+" "+grounded);
        //RBody.AddForceAtPosition(HoverForce, transform.position);
        RBody.AddForce(HoverForce);
    }
}
