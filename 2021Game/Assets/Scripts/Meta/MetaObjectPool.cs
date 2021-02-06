using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaObjectPool : MonoBehaviour
{
	public Dictionary<string, List<GameObject>> ObjectPool = new Dictionary<string, List<GameObject>>();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Object pool instantiate.
	public GameObject Instantiate(string Name)
	{
		if(ObjectPool.ContainsKey(Name))
		{
			// Search for an inactive prefab in the pool first.
			for(int i = 0; i < ObjectPool[Name].Count; ++i)
			{
				// If an inactive prefab is found, return it.
				if(ObjectPool[Name][i].activeSelf == false)
				{
					ObjectPool[Name][i].SetActive(true);
					return ObjectPool[Name][i];
				}
			
			}				
			// No inactive prefab so add a new one to the pool.
			return(Instantiate_Helper(Name));
		}
		
		// No pool for this prefab found, create a pool for it 
		ObjectPool.Add(Name, new List<GameObject>());							
		return(Instantiate_Helper(Name));		
	}
		
	
	// Creates the game object if there are not enough in the pool.
	public GameObject Instantiate_Helper(string Name)
	{
		GameObject NewObject = (GameObject)Instantiate(Resources.Load(Name));
		ObjectPool[Name].Add(NewObject);
		NewObject.transform.SetParent(this.gameObject.transform);		
		return NewObject;
	}
	
	// Instantiate overload for position.
	public GameObject Instantiate(string Name, Vector3 Position)
	{
		GameObject NewObject = this.Instantiate(Name);
		NewObject.transform.position = Position;
		return NewObject;
	}

	// Instantiate overload for position and rotation.
	public GameObject Instantiate(string Name, Vector3 Position, Quaternion Rotation)
	{
		GameObject NewObject = this.Instantiate(Name, Position);
		NewObject.transform.rotation = Rotation;
		return NewObject;
	}		
	
	// Destroy a pooled object, i.e. deactivates it.
	public void Deactivate(GameObject Object)
	{
		Object.SetActive(false);
	}
}
