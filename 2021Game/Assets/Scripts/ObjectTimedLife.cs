using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimedLife : MonoBehaviour
{
    public float Lifetime = 5.0f;
    float TimerLifetime = 0.0f;
	
	MetaObjectPool _MetaObjectPool = null;
	
    // Start is called before the first frame update
    void Start()
    {
		if(transform.parent != null)
		{
			_MetaObjectPool = transform.parent.gameObject.GetComponent<MetaObjectPool>();
		}		
    }

    // Update is called once per frame
    void Update()
    {
        TimerLifetime += Time.deltaTime;
		if(TimerLifetime > Lifetime)
		{
			if(_MetaObjectPool != null)
				_MetaObjectPool.Deactivate(this.gameObject);
			else
				Destroy(this.gameObject);
		}				
    }
	
    private void OnDisable()
    {		
        TimerLifetime = 0.0f;
    }	
}
