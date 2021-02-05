﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGame : MonoBehaviour
{
	MetaObjectPool _MetaObjectPool;
	
    // Start is called before the first frame update
    void Start()
    {
		_MetaObjectPool = GetComponent<MetaObjectPool>();
		_MetaObjectPool.Instantiate("MetaGameCamera");
		_MetaObjectPool.Instantiate("UnitPlayer");
		_MetaObjectPool.Instantiate("UIGameMousePointer");
		_MetaObjectPool.Instantiate("UIGameHeightMarker");
		_MetaObjectPool.Instantiate("UnitCrate",new Vector3(14,0.5f,15));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
