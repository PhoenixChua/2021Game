using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
	public GameObject Target;
	public float Speed = 10;
	Vector3 InitialPositionOffset;
	
    void Awake()
    {
        InitialPositionOffset = transform.position;
    }	
	
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target)
        {
            float interpolation = Speed * Time.fixedDeltaTime;
            transform.position = 
			Vector3.Lerp(transform.position, Target.transform.position + InitialPositionOffset, interpolation);
        }        
    }	
}
