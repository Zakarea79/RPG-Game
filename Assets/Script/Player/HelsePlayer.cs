using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelsePlayer : MonoBehaviour
{
	[SerializeField] private Slider UI_Helse;
	[Range(0 , 100)] [SerializeField] private int Helse = 100;
	private int MaxHelse = 0;
	protected void Start()
	{
		UI_Helse.maxValue = Helse;
		MaxHelse = Helse;
	}
	public int hell
	{
		set 
		{
			Helse += value;
			UI_Helse.value = Helse;
			if(Helse > MaxHelse)
			{
				Helse = MaxHelse;
				UI_Helse.value = Helse;
			}
		}
	}
	public int damage
	{
		set 
		{
			Helse -= value;
			UI_Helse.value = Helse;
			if(Helse < 0)
			{
				Helse = 0;
				UI_Helse.value = Helse;
				Time.timeScale = 0;
				Debug.Log("You Lose");
			}
		}
	}
}
