using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Skill))]
public class SkillBehaviourBase : MonoBehaviour
{
	
    // Start is called before the first frame update
    public virtual void Start()
    {
		GetComponent<Skill>().RegisterSkillEffect(this);    
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        
    }	
		
	public virtual bool Resolve(Unit TargetUnit, Unit UserUnit)
    {
        return true;
    }
}
