using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGameSkillManager : MonoBehaviour
{
	// A skill that can be modified in the editor.
    [System.Serializable]
    public class SkillData
    {
        public string Name = "Skill";
		// Using this skill prevents the user from acting for this amount of time.
        public float GlobalCooldown = 0.0f;

        [System.Serializable]
        public class SkillEffect
        {
            public string SpawnObjectName;
            public enum EffectType
            {
                SpawnAtUser,
                SpawnAtTarget,
                Self
            };

            public EffectType SpawnType;
            public float OffsetForward = 0;
            public float OffsetRight = 0;
            public float OffsetUp = 0;
        }

        public List<SkillEffect> SkillEffects = new List<SkillEffect>();
    }
	
    public List<SkillData> Skills = new List<SkillData>();	
	
	MetaObjectPool _MetaObjectPool;
	
    // Start is called before the first frame update
    void Start()
    {
		_MetaObjectPool = GetComponent<MetaObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ResolveSkill(string SkillName, GameObject User, Unit UserUnitComponent)
	{
		for(int i = 0; i < Skills.Count; ++i)
		{
			if(Skills[i].Name == SkillName && UserUnitComponent.GlobalCooldown < 0.0f)
			{
				UserUnitComponent.GlobalCooldown = Skills[i].GlobalCooldown;
				for(int j = 0; j < Skills[i].SkillEffects.Count; ++j)
				{
					// Create all skill objects (projectiles, particles etc.) associated with skill.
					GameObject SkillObject = _MetaObjectPool.Instantiate(
					Skills[i].SkillEffects[j].SpawnObjectName, User.transform.position,
					User.transform.rotation);
					
					SkillObject.GetComponent<Skill>().UserUnit = UserUnitComponent;
					
				}
				break;
			}
		}
	}
}
