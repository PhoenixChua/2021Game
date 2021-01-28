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
	
	public void Instantiate(string Name)
	{
		foreach(var i in ObjectPool)
		{
			if(i.Key == Name)
			{
				return;
			}
		}
		ObjectPool.Add(Name, new List<GameObject>());
		
		GameObject NewObject = (GameObject)Instantiate(Resources.Load(Name));
		ObjectPool[Name].Add(NewObject);
	}
}
