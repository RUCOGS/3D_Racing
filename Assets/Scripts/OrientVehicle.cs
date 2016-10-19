using UnityEngine;
using System.Collections;

public class OrientVehicle : MonoBehaviour {
    public float raycastDistance = 10f;
    public float GroundDistance;
    public Vector3 normal;

    int groundLayer = 8;
    int vehicleLayer = 9;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        int layermask = 1 << groundLayer;

        RaycastHit hit;
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        Physics.Raycast(transform.position, dwn, out hit, raycastDistance, layermask);
        normal = hit.normal;
        GroundDistance = hit.distance;
        //Debug.Log("Raycast hit " + hit.distance);
        transform.rotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
	}
}
