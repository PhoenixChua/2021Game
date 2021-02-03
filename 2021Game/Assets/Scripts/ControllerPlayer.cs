using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    public float Speed = 10.0f;
    public float SpeedMax = 30.0f;	
    public float SpeedRotation = 3.0f;		
	public float SpeedHover = 15.0f;
	public float DurationHover = 0.75f;
	
	float TimerHover = 0.0f;
	float DistToGround;
	
	Rigidbody _Rigidbody;
	MetaGameSkillManager _MetaGameSkillManager;
	Unit _Unit;
	
    Vector3 WorldUp;
    Vector3 WorldDown;
    Vector3 WorldLeft;
    Vector3 WorldRight;	
	
    // Start is called before the first frame update
    void Start()
    {
		_Rigidbody = GetComponent<Rigidbody>();
		// Like to the meta game object to allow the use of skills.
		_MetaGameSkillManager = GameObject.Find("MetaGame").GetComponent<MetaGameSkillManager>();
		_Unit = GetComponent<Unit>();
		
	    DistToGround = GetComponent<Collider>().bounds.extents.y;
		
		// Adapt movement to isometric world.
		// Quarternion sets a vector in the isometric world by rotating 45* around up vector.
        WorldUp = (Quaternion.AngleAxis(45, Vector3.up) * -Vector3.forward).normalized;
        WorldDown = (Quaternion.AngleAxis(45, Vector3.up) * Vector3.forward).normalized;
        WorldLeft = (Quaternion.AngleAxis(45, Vector3.up) * Vector3.right).normalized;
        WorldRight = (Quaternion.AngleAxis(45, Vector3.up) * -Vector3.right).normalized;	  
    }
	
	// Non physics based updates.
	// Implement skill input in update as it is not physics based.
	void Update()
	{
		// Implement use weapon skill.
        if (Input.GetMouseButton(0))
        {
			_MetaGameSkillManager.ResolveSkill("Pulse Blaster", this.gameObject, _Unit);
		}			
	}

	// Physics based updates.
    // Implement movement in fixedupdate as it is physics based.
    void FixedUpdate()
    {	
		// Player rotation towards mouse.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
        float hitdist = 0.0f;
        if (new Plane(Vector3.up, transform.position).Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the plane the player is on at the ame y value.
            Vector3 RotationTemp = ray.GetPoint(hitdist) - transform.position;
			
            if (RotationTemp != Vector3.zero)
            {
                // Determine the target rotation.  This is the rotation if the player looks at the mouse.
                Quaternion targetRotation = Quaternion.LookRotation(RotationTemp);
                // Smoothly rotate the player towards the mouse.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, SpeedRotation * Time.fixedDeltaTime);
            }
        }	
	
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
		
		// Hovering
        if (TimerHover < DurationHover)
        {
            if (Input.GetMouseButton(1))
            {
                TimerHover += Time.fixedDeltaTime;
                _Rigidbody.AddForce(transform.up * SpeedHover);
            }
        }		
        
		// Reset hovering cooldown when the player is on the ground.
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
