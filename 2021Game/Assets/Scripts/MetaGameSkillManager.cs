using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGameSkillManager : MonoBehaviour
{
    [System.Serializable]
    public class SkillData
    {
        public string Name = "Skill";
        public float GlobalCooldown = 0.0f;

        [System.Serializable]
        public class SkillEffect
        {
            public GameObject SpawnObject;
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
	
	public void ResolveSkill(string SkillName, GameObject User)
	{
		_MetaObjectPool.Instantiate(SkillName, User.transform.position);
	}
}
