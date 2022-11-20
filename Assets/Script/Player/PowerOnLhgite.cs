using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnLhgite : MonoBehaviour
{
	protected void OnTriggerEnter(Collider other)
	{
		Enemy enemy;
		if(other.CompareTag("Enemy") && other.transform.TryGetComponent<Enemy>(out enemy))
			enemy.HP = 30;
	}
}
