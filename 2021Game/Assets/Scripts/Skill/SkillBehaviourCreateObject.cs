using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates the specified objects when this skill is triggered by a valid target.
// Only implemented with the object pool.
public class SkillBehaviourCreateObject : SkillBehaviourBase
{
	public MetaObjectPool _MetaObjectPool;
	public List<string> ObjectsToCreate;
	
    // Start is called before the first frame update
    public override void Start()
    {
		base.Start();		
        _MetaObjectPool = this.GetComponent<Skill>()._MetaObjectPool;
    }

    // Update is called once per frame
    public override bool Resolve(Unit Target, Unit User)
    {
        if(_MetaObjectPool)
		{
			for(int i = 0; i < ObjectsToCreate.Count; ++i)
				_MetaObjectPool.Instantiate(ObjectsToCreate[i], this.transform.position);
		}
		return true;
    }
}
