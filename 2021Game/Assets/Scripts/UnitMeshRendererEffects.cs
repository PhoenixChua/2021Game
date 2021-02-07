using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMeshRendererEffects : MonoBehaviour
{
	// Material and Color Changes When Hit/Killed.
	Color OriginalColor;	
	Material[] OriginalMaterials;
	Material[] DeathMaterial = null;	
	
	MeshRenderer _MeshRenderer;
	
	void Start()
	{
		_MeshRenderer = GetComponent<MeshRenderer>();		
		OriginalMaterials = _MeshRenderer.materials;
		OriginalColor = _MeshRenderer.material.color;
	}
	

	public void ChangeDeathMaterial(Material KillMaterialChange)
	{
		List<Material> DeathMaterialList = new List<Material>();
			
		for(int i = 0; i < OriginalMaterials.Length; ++i)
		{
			DeathMaterialList.Add(KillMaterialChange);
		}
		
		DeathMaterial = DeathMaterialList.ToArray();		
		
		if(DeathMaterial != null)
		{
			_MeshRenderer.materials = DeathMaterial;
		}
	}
	
	public void DamageFlashRed(float Time)
	{
		_MeshRenderer.material.color = Color.red;
		Invoke("ResetColor", Time);
	}
	
	public void ResetColor()
	{
      _MeshRenderer.material.color = OriginalColor;
	}

	void OnDisable()	
	{		
		_MeshRenderer.materials = OriginalMaterials;
		DeathMaterial = null;	
	}
}
