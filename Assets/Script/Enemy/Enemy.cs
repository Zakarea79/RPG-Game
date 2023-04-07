using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	private bool dmg = true;
	internal bool Live = true;
	//[SerializeField] private float Speed = 5;
	[SerializeField] private Transform HPBAR;
	[SerializeField] private Vector3 Destance;
	[Range(10 , 1000)][SerializeField] private int _HP = 100;
	[Range(.1f ,10f)][SerializeField] private float section = 1;
	[SerializeField] private NavMeshAgent nav;
	private Transform player;
	public int HP
	{
		set
		{
			if(dmg == true)
			{
				dmg = false;
				_HP -= value;
				Invoke("truedmg" , .1f);
			}
			if(_HP <= 0)
			{
				_HP = 0;
				print("Play Die Animtion");
				Live = false;
				HPBAR.gameObject.SetActive(false);
				//After Die Animtion setActive False
				gameObject.SetActive(false);
			}
			HPBAR.GetChild(0).transform.localScale = new Vector3((float)(_HP/section) / 100, 1, 1);
		}
	}
	
	private void truedmg(){dmg = true;}
	
	protected void Start()
	{
		Live = true;
		HPBAR = Instantiate(HPBAR , transform.position, transform.rotation);
		HPBAR.position = transform.position + Destance;
		HPBAR.SetParent(transform);
		player = GameObject.Find("Player").transform;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(Vector3.Distance(transform.position , player.transform.position) < 5)
		{
			if(Vector3.Distance(transform.position , player.transform.position) > 1.5f)
			{
				nav.destination = player.transform.position;
			}else
			{
				nav.destination = transform.position;
			}
		}
	}
	//protected void OnTriggerStay(Collider other)
	//{
	//	if(other.CompareTag("Player") && Vector3.Distance(transform.position , other.transform.position) > 2)
	//	{
	//		transform.LookAt(new Vector3(player.position.x , .5f , player.position.z));
	//		transform.position = Vector3.MoveTowards(new Vector3(transform.position.x , .5f , transform.position.z) , new Vector3(other.transform.position.x , .5f , other.transform.position.z) , .1f);
	//	}
	//}
}
