using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnLhgite : MonoBehaviour
{
	protected void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
			other.GetComponent<Enemy>().HP = 30;
	}
}
