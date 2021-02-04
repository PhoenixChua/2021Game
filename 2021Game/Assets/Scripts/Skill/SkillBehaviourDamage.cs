using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviourDamage : SkillBehaviourBase
{
    // Update is called once per frame
    void Update()
    {
        
    }
	
    public override bool Resolve(Unit Target, Unit User)
    {
		//Debug.Log("Damage");
		return true;
	}
}
