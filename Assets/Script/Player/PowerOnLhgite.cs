using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnLhgite : MonoBehaviour
{
	private Player player;
	private void Start() {
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	protected void OnTriggerEnter(Collider other)
	{
		ai_enemy enemy;
		if(other.CompareTag("Enemy") && other.transform.TryGetComponent<ai_enemy>(out enemy) && other.isTrigger == false) 
			enemy.HPM = 500;
	}
}
