using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    public float Speed = 10.0f;
    public float SpeedMax = 30.0f;	
	public float SpeedHover = 15.0f;
	public float DurationHover = 0.75f;
	
	float TimerHover = 0.0f;
	float DistToGround;
	
	Rigidbody _Rigidbody;
	
    Vector3 WorldUp;
    Vector3 WorldDown;
    Vector3 WorldLeft;
    Vector3 WorldRight;	
	
    // Start is called before the first frame update
    void Start()
    {
		_Rigidbody = GetComponent<Rigidbody>();
	    DistToGround = GetComponent<Collider>().bounds.extents.y;
		
		// Adapt movement to isometric world.
		// Quarternion sets a vector in the isometric world by rotating 45* around up vector.
        WorldUp = (Quaternion.AngleAxis(45, Vector3.up) * -Vector3.forward).normalized;
        WorldDown = (Quaternion.AngleAxis(45, Vector3.up) * Vector3.forward).normalized;
        WorldLeft = (Quaternion.AngleAxis(45, Vector3.up) * Vector3.right).normalized;
        WorldRight = (Quaternion.AngleAxis(45, Vector3.up) * -Vector3.right).normalized;	  
    }

    // Update is called once per frame
    void FixedUpdate()
    {	
		// Directional movement
        if (new Vector3(_Rigidbody.velocity.x, 0, _Rigidbody.velocity.z).sqrMagnitude < SpeedMax * SpeedMax)
        {
            Vector3 MoveDirection = Vector3.zero;
            if (Input.GetAxisRaw("Vertical") > 0)
                MoveDirection += WorldUp;
            if (Input.GetAxisRaw("Vertical") < 0)
                MoveDirection += WorldDown;
            if (Input.GetAxisRaw("Horizontal") > 0)
                MoveDirection += WorldRight;
            if (Input.GetAxisRaw("Horizontal") < 0)
                MoveDirection += WorldLeft;

            _Rigidbody.AddForce(MoveDirection.normalized * Speed);
        } 
		
        if (TimerHover < DurationHover)
        {
            if (Input.GetMouseButton(1))
            {
                TimerHover += Time.fixedDeltaTime;
                _Rigidbody.AddForce(transform.up * SpeedHover);
            }
        }		

        if(IsGrounded())
        {
            TimerHover = 0;
        }		
    }
	
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, DistToGround + 0.5f, (1 << 8) | (1 << 9));
    }	
}
