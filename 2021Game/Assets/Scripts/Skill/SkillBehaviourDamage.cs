using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviourDamage : SkillBehaviourBase
{
	public float Modifier = 1;
	
	public Material KillMaterialChange;
	
    public override bool Resolve(Unit Target, Unit User)
    {
		Target.Damage(User.Power, Modifier, KillMaterialChange);
		return true;
	}
}
