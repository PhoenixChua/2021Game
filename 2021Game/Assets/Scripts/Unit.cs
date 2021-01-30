using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Health = 100;
    public int HealthMax = 100;
    public int Power = 10;
	public string Team;	
	
	public float GlobalCooldown = 0.01f;
	
    // Start is called before the first frame update
    void Start()
    {
        GlobalCooldown = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
		GlobalCooldown -= Time.deltaTime;
    }
}
