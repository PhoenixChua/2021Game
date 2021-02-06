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
	Rigidbody _Rigidbody;
	Collider _Collider;
	MeshRenderer _MeshRenderer;
	
    // Start is called before the first frame update
    void Start()
    {
		if(transform.parent != null)
		{
			_MetaObjectPool = transform.parent.gameObject.GetComponent<MetaObjectPool>();
		}  		
		
		_Rigidbody = GetComponent<Rigidbody>();
		_Collider = GetComponent<Collider>();
		_MeshRenderer = GetComponent<MeshRenderer>();
		
        GlobalCooldown = 0.01f;
		OriginalMaterials = _MeshRenderer.materials;
		OriginalColor = _MeshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !Dead)
		{
			Dead = true;
			_Collider.isTrigger = true;
			_Rigidbody.useGravity = false;
			ChangeDeathMaterial();
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
	
	void OnDisable()
	{
		_MeshRenderer.materials = OriginalMaterials;
		_Collider.isTrigger = false;
		_Rigidbody.useGravity = true;
		Health = HealthMax;	
		Dead = false;
		DeathMaterial = null;		
	}
	
	void ModifyHealth(int Amount)
	{
		Health += Amount;
		
		if(Health > HealthMax)
			Health = HealthMax;
	}
	
	public void Damage(int Power, float Modifier, Material KillMaterialChange)
	{
		float Total = Power * Modifier;
		ModifyHealth(-(int)Total);
		
		if(Health <= 0)
		{
			if(KillMaterialChange != null)
			{
				List<Material> DeathMaterialList = new List<Material>();
			
				for(int i = 0; i < OriginalMaterials.Length; ++i)
				{
					DeathMaterialList.Add(KillMaterialChange);

				}
				DeathMaterial = DeathMaterialList.ToArray();
			}
		}
		else
		{
			DamageFlashRed(0.1f);
		}
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
	
	
	// Material and Color Changes When Hit/Killed.
	Color OriginalColor;	
	Material[] OriginalMaterials;
	Material[] DeathMaterial = null;

	void ChangeDeathMaterial()
	{
		if(DeathMaterial != null)
		{
			_MeshRenderer.materials = DeathMaterial;
		}
	}
	
	void DamageFlashRed(float Time)
	{
		_MeshRenderer.material.color = Color.red;
		Invoke("ResetColor", Time);
	}
	
	void ResetColor()
	{
      _MeshRenderer.material.color = OriginalColor;
	}	
}
