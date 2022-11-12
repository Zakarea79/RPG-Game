using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	internal bool Live = true;
	[SerializeField] private Transform HPBAR;
	[SerializeField] private Vector3 Destance;
	[Range(10 , 1000)][SerializeField] private int _HP = 100;
	[Range(.1f ,10f)][SerializeField] private float section = 1;
	private bool dmg = true;
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
	}
	protected void Update()
	{
		HPBAR.position = transform.position + Destance;
	}
}
