using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
	List<SkillBehaviourBase> SkillEffects = new List<SkillBehaviourBase>();		
	
	int Activations;
	public int MaxActivations = 1;
		
    public Unit UserUnit = null;
	MetaObjectPool _MetaObjectPool = null;	
	
    // Start is called before the first frame update
    void Start()
    {
		if(transform.parent != null)
		{
			_MetaObjectPool = transform.parent.gameObject.GetComponent<MetaObjectPool>();
		}  
    }

    // Update is called once per frame
    void Update()
    {
        if(Activations == 0)
		{
			if(_MetaObjectPool != null)
				_MetaObjectPool.Deactivate(this.gameObject);
			else
				Destroy(this.gameObject);
		}
    }
	
    void OnTriggerStay(Collider other)
    {
        Unit OtherUnit = other.gameObject.GetComponent<Unit>();
		
		// Ignore object collisions that are not units.
        if (!OtherUnit || OtherUnit.IsDead())
        {
			return;
		}
        else if (!UserUnit || UserUnit != OtherUnit)
        {
			if(Activations != 0)
			{
				//Resolve effects;
				for(int i = 0; i < SkillEffects.Count; ++i)
				{
					SkillEffects[i].Resolve(OtherUnit, UserUnit);
				}
				Activations -= 1;
			}			
		}			
	}

	public void RegisterSkillEffect(SkillBehaviourBase Effect)
	{
		SkillEffects.Add(Effect);
	}	

	void OnEnable()
	{
		Activations = MaxActivations;
	}
}
