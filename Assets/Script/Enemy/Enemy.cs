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
	[SerializeField] private Transform[] point;
	private Transform player;
	private Animator anim;
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
		anim = GetComponent<Animator>();
		HPBAR = Instantiate(HPBAR , transform.position, transform.rotation);
		HPBAR.position = transform.position + Destance;
		HPBAR.SetParent(transform);
		player = GameObject.Find("Player").transform;
	}
	
	private bool findPlayer = false;
	private Transform pointG , pointback;
	private float TimerSlip = 0;
	protected void Update()
	{
		if(Vector3.Distance(new Vector3(transform.position.x , 0 , transform.position.z) , new Vector3(player.transform.position.x , 0 , player.transform.position.z)) < 5)
		{
			if(Vector3.Distance(new Vector3(transform.position.x , 0 , transform.position.z) , new Vector3(player.transform.position.x , 0 , player.transform.position.z)) > 1.5f)
			{
				nav.destination = player.transform.position;
				findPlayer = false;
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("idel") == false)
				{
					anim.SetTrigger("idel");
				}
			}
			else
			{
				if(findPlayer == false) 
				{
					nav.destination = transform.position;
					findPlayer = true;
				}
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attak") == false)
				{
					anim.SetTrigger("Attak");
				}
				
			}
		}
		else
		{
			if(pointG == null)
			{
				beaeainja:
				pointG = point[Random.Range(0 , point.Length)];
				if(pointG == pointback)goto beaeainja;
				pointback = pointG;
			}
			else
			{
				if(Vector3.Distance(new Vector3(transform.position.x , 0 , transform.position.z) , new Vector3(pointG.transform.position.x , 0 , pointG.transform.position.z)) <= .1f)
				{
					TimerSlip += Time.deltaTime;
					if((byte)TimerSlip == 3)
					{
						pointG = null;
						TimerSlip = 0;
					}
					
				}
				else
				{
					nav.destination = pointG.position;					
				}
			}
		}
	}
	public void Attack(){player.GetComponent<HelsePlayer>().damage = 5;}
	//protected void OnTriggerStay(Collider other)
	//{
	//	if(other.CompareTag("Player") && Vector3.Distance(transform.position , other.transform.position) > 2)
	//	{
	//		transform.LookAt(new Vector3(player.position.x , .5f , player.position.z));
	//		transform.position = Vector3.MoveTowards(new Vector3(transform.position.x , .5f , transform.position.z) , new Vector3(other.transform.position.x , .5f , other.transform.position.z) , .1f);
	//	}
	//}
}
