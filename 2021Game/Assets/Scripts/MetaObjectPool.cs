using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaObjectPool : MonoBehaviour
{
	[System.Serializable]
	public class ObjectPoolEntry 
	{
		public string Name;
		public GameObject Objects;
	}
	
	public List<ObjectPoolEntry> ObjectPool;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
