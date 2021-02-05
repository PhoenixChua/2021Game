using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameMousePointer : MonoBehaviour
{
	GameObject Player;
	LineRenderer _LineRenderer;
    // Start is called before the first frame update
    void Start()
    {
		_LineRenderer = GetComponent<LineRenderer>();
		Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if(Player)
		{
			Plane plane = new Plane(Vector3.up, Player.transform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
		
			float hitdist = 0.0f;
			if (plane.Raycast(ray, out hitdist))
			{
				transform.position = ray.GetPoint(hitdist);
				_LineRenderer.SetPosition(0, ray.GetPoint(hitdist));
				_LineRenderer.SetPosition(1, Player.transform.position);
			}
		}		
    }
}
