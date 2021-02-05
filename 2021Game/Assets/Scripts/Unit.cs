using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Health = 100;
    public int HealthMax = 100;
    public int Power = 10;
	public string Team;	
	
	// A state where the unit is playing its death animation after
	// which it is destroyed by coroutine.
	bool Dead = false;
	// Placeholder
	public float DeathAnimationTime = 1f; 
	
	public float GlobalCooldown = 0.01f;
	
	MetaObjectPool _MetaObjectPool = null;
	
    // Start is called before the first frame update
    void Start()
    {
		if(transform.parent != null)
		{
			_MetaObjectPool = transform.parent.gameObject.GetComponent<MetaObjectPool>();
		}  		
		
        GlobalCooldown = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !Dead)
		{
			Dead = true;
			StartCoroutine("Kill");
		}		
		
		GlobalCooldown -= Time.deltaTime;		
    }
	
	IEnumerator Kill()
	{
		if (DeathAnimationTime != 0)
           yield return new WaitForSeconds(DeathAnimationTime);	

		if(_MetaObjectPool != null)
				_MetaObjectPool.Deactivate(this.gameObject);
			else
				Destroy(this.gameObject);
	}
	
	void OnEnable()
	{
		Health = HealthMax;	
		Dead = false; 		
	}
	
	void ModifyHealth(int Amount)
	{
		Health += Amount;
		
		if(Health > HealthMax)
			Health = HealthMax;
	}
	
	public void Damage(int Power, float Modifier)
	{
		float Total = Power * Modifier;
		ModifyHealth(-(int)Total);
	}
	
	public void Heal(int Power, float Modifier)
	{
		float Total = Power * Modifier;
		ModifyHealth((int)Total);		
	}
	
	public bool IsDead()
	{
		return Dead;
	}
}
