using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Unit UserUnit;	
	List<SkillBehaviourBase> SkillEffects = new List<SkillBehaviourBase>();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
    void OnTriggerStay(Collider other)
    {
        Unit OtherUnit = other.gameObject.GetComponent<Unit>();

        if (!OtherUnit)
        {
			return;
		}
        else// if (!UserUnit || UserUnit != OtherUnit)
        {
			//Resolve effects;
			for(int i = 0; i < SkillEffects.Count; ++i)
			{
				SkillEffects[i].Resolve(OtherUnit, UserUnit);
			}				
		}			
	}

	public void RegisterSkillEffect(SkillBehaviourBase Effect)
	{
		SkillEffects.Add(Effect);
	}		
}
