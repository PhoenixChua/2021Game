using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviourBase : MonoBehaviour
{
    public Unit UserUnit;	
	
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
        else if (!UserUnit || UserUnit != OtherUnit)
        {
			//Resolve effects;
			Debug.Log("Bloop");
		}			
	}		
}
