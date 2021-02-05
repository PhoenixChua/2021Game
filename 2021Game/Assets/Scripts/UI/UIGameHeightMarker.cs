using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameHeightMarker : MonoBehaviour
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
			RaycastHit hit;
			Ray ray = new Ray(Player.transform.position, -Vector3.up); 
			
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 8) | (1 << 9)))
			{
				transform.position = hit.point;
				_LineRenderer.SetPosition(0, hit.point);
				_LineRenderer.SetPosition(1, Player.transform.position);
			}
		}			
    }
}
