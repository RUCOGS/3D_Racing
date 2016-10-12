using UnityEngine;
using System.Collections;

public class PlayerSteer : MonoBehaviour {
    public float Speed = 12f;
    //public float mass = 0.3f;
    public float TurnSpeed = 180f;
    //public float jumpForce = 20;
    //public float jumpReact = 5;
    private bool floored = false;
    //private int jumpCounter = 0;
    //private float lastJumpAsk;

    //public float jumpTimeMargin = 0.2f;

    //public int jumpAllowed = 2;
    private Vector3 acceleration = new Vector3(0f, 0f, 0f);
    private Vector3 speed = new Vector3(0f, 0f, 0f);
    private float moveSpeed = 0f;

    private string MovementAxisName;
    private string TurnAxisName;
    private Rigidbody RBody;
    private float MovementInputVal;
    private float TurnInputVal;
    
    private void Awake()
    {
        RBody = GetComponent<Rigidbody>();
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
    }

    private void Update()
    {
        MovementInputVal = Input.GetAxis(MovementAxisName);
        TurnInputVal = Input.GetAxis(TurnAxisName);

        /*if (Input.GetButtonDown("Fire1"))
        {
            jump();
        }
        */
    }

    private void Move()
    {
        Vector3 movement = transform.forward * MovementInputVal * Speed * Time.deltaTime;

        RBody.MovePosition(RBody.position + movement);
    }

    private void Turn()
    {
        float turn = TurnInputVal * TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        RBody.MoveRotation(RBody.rotation * turnRotation);
    }

    /*
    void jump()
    {
        if (floored || jumpCounter < jumpAllowed)
        {
            acceleration.y += mass * jumpForce;
            speed.y += jumpReact;
            jumpCounter++;
            floored = false;
        }
        else
        {
            lastJumpAsk = Time.time;
        }
    }*/
}
