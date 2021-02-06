using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviourMovement : SkillBehaviourBase
{
	public float SpeedForward = 1;
	Rigidbody _Rigidbody;	
	
    public override void Start()
    {
		base.Start();
        _Rigidbody = GetComponent<Rigidbody>();        
    }	
	
    // Update is called once per frame
    public override void FixedUpdate()
    {
        _Rigidbody.MovePosition(transform.position + SpeedForward * transform.forward * Time.fixedDeltaTime);    
    }
	
    public override bool Resolve(Unit Target, Unit User)
    {
		return true;
	}		
}
