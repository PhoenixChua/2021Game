using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviourApplyForce : SkillBehaviourBase
{
	public float Modifier = 300;
    public enum Type
    {
       Push,
	   Pull
    };
	
	public Type Effect = SkillBehaviourApplyForce.Type.Push;
	
    public override bool Resolve(Unit Target, Unit User)
    {
		Rigidbody _Rigidbody = Target.GetComponent<Rigidbody>();
		Vector3 Direction;
		
		if(_Rigidbody)					
		{		
			if(Effect == SkillBehaviourApplyForce.Type.Push)
			{
				Direction = Target.transform.position - User.transform.position;
			}
			else
			{
				Direction = User.transform.position - Target.transform.position;
			}
			
			
			_Rigidbody.AddExplosionForce(Modifier, 
				Target.transform.position - 0.1f * Direction, 2);
		}
		
		return true;
	}	
}
