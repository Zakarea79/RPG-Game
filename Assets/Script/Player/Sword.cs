using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	Player player;
	private RaycastHit hit;
    void Start()
    {
	    player = GameObject.Find("Player").GetComponent<Player>();
    }
	protected void FixedUpdate()
	{
		Debug.DrawRay(transform.position , transform.TransformDirection(Vector3.right) * player.LenSword, Color.red);
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) , out hit , player.LenSword))
		{	
			if(player.AttakAction && hit.transform.CompareTag("Enemy"))
			{
				hit.transform.GetComponent<Enemy>().HP = 10;
			}
	    }
    }
}
