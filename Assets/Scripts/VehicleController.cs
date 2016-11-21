using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    // Hover wheel objects
    Transform FLWheel, FRWheel, BLWheel, BRWheel;
    Rigidbody FLrbody, FRrbody, BLrbody, BRrbody;

    // Car body object
    Transform Car;
    Rigidbody CarBody;

    // AWAKE
    void Awake ()
    {
        FLWheel = transform.FindChild("FLWheel");
        FRWheel = transform.FindChild("FRWheel");
        BLWheel = transform.FindChild("BLWheel");
        BRWheel = transform.FindChild("BRWheel");

        FLrbody = FLWheel.GetComponent<Rigidbody>();
        FRrbody = FLWheel.GetComponent<Rigidbody>();
        BLrbody = FLWheel.GetComponent<Rigidbody>();
        BRrbody = FLWheel.GetComponent<Rigidbody>();

        Car = transform.FindChild("car");
        CarBody = Car.GetComponent<Rigidbody>();

    }

	// START
	void Start ()
    {
        // Calculate center of wheels
        Vector3 centroid =
            new Vector3(
                (FLWheel.position.x + FRWheel.position.x + BLWheel.position.x + BRWheel.position.x) / 4,
                (FLWheel.position.y + FRWheel.position.y + BLWheel.position.y + BRWheel.position.y) / 4,
                (FLWheel.position.z + FRWheel.position.z + BLWheel.position.z + BRWheel.position.z) / 4
            );

        // Calculate up vector. Use arbitrary two distance vectors between three wheels.
        Vector3 upOrient = Vector3.Cross(FRWheel.position - BRWheel.position, FRWheel.position - FLWheel.position);

        // Position car object wrt centroid and up vector
        Car.position = centroid;
        Car.rotation = Quaternion.Slerp(Car.rotation, Quaternion.FromToRotation(Car.up, upOrient) * Car.rotation, .2f);
    }
	
	// UPDATE
	void FixedUpdate ()
    {
        OrientVehicle();
	}

    void OrientVehicle()
    {

    }
}
