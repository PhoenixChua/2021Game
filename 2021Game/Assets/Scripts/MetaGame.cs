using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGame : MonoBehaviour
{
	MetaObjectPool _MetaObjectPool;
	
    // Start is called before the first frame update
    void Start()
    {
		_MetaObjectPool = GetComponent<MetaObjectPool>();
		_MetaObjectPool.Instantiate("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
