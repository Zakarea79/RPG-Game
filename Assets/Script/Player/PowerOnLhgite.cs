using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnLhgite : MonoBehaviour
{
	protected void OnTriggerEnter(Collider other)
	{
		Enemy enemy;
		if(other.CompareTag("Enemy") && other.transform.TryGetComponent<Enemy>(out enemy) && other.isTrigger == false)
			enemy.HP = 30;
	}
}
