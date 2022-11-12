﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float Speed = 10f;
	[SerializeField] private Animator animPlayer;
	
	public float LenSword = .5f;
	
	private float jumpspeed = 0;
	private Vector3 JumpBase;
	private CharacterController characterController;
	private bool JumpControl = false;
	
	internal bool AttakAction = false;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
	protected void Start()
	{
		characterController = gameObject.GetComponent<CharacterController>();
		
	}
	
    void Update()
	{
		JumpControl = ChackPlayAnim("jump normal") == true || ChackPlayAnim("jump acrobat") == true ? true : false;
		
		float x  = ZInput.GetAxis("Horizontal");
		float y  = ZInput.GetAxis("Vertical");
		//-----------------------------------------------------
		if(System.Math.Abs(x) > .3)
			animPlayer.SetFloat("Walk" , System.Math.Abs(x));
		else if(System.Math.Abs(y) > .3)
			animPlayer.SetFloat("Walk" , System.Math.Abs(y));
		else
			animPlayer.SetFloat("Walk" , 0);
		//-----------------------------------------------------
		if (ZInput.GetKeyDown("jump") && JumpControl == false)
		{
			animPlayer.SetTrigger("normalJamp");
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit) && JumpControl == false)
			{
				JumpBase = hit.transform.position;
			}
		}
		//------------------------------------------Debug-----------------------------------------------------------------------
		RaycastHit hitDebug;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDebug))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitDebug.distance, Color.green);
		}
		//------------------------------------------Debug-----------------------------------------------------------------------
		if(ZInput.GetKeyDown("attak") && StatuseAnimitonAttack())
		{
			animPlayer.SetInteger("Attak" , Random.Range(1 , 4));
		}
		else if(ZInput.GetKeyDown("battak") && StatuseAnimitonAttack())
		{
			animPlayer.SetInteger("Attak" , 4);
		}
		
		animPlayer.applyRootMotion = ChackPlayAnim("attak3");
		AttakAction = StatuseAnimitonAttack() == false ? true : false;
		
		if(Vector3.Distance(new Vector3(0,transform.position.y , 0) , new Vector3(0 , JumpBase.y ,0)) > 3)
		{
			jumpspeed = -2;
		}
		float xmove = 0;
		float ymove = 0;
		if(System.Math.Abs(x) > .3 || System.Math.Abs(y) > .3)
		{
			xmove = x;
			ymove = y;
			
			var Rotatev = new Vector3(-y, 0, x);
			Quaternion quaternion = Quaternion.LookRotation(Rotatev);
			transform.rotation = quaternion;
		}
		characterController.Move((new Vector3(-ymove, jumpspeed ,xmove) * Speed * Time.deltaTime));
	}
	public void SetJumpControl(int statuse){JumpControl = System.Convert.ToBoolean(statuse);if(JumpControl == true) jumpspeed = 2;}
    
	public void reseatattak(){animPlayer.SetInteger("Attak" , 0);}
    
	private bool ChackPlayAnim(string name){return animPlayer.GetCurrentAnimatorStateInfo(0).IsName(name);}
    
	private bool StatuseAnimitonAttack()
	{
		for (int i = 0; i < 4; i++) 
		{
			if(ChackPlayAnim("attak" + i) == true)
			{
				return false;
			}
		}
		return true;
	}
}
