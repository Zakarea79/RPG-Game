﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Player player;
    [SerializeField] Vector3 vect;
    // private RaycastHit hit;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    protected void FixedUpdate()
    {
	    DrawRay(Vector3.left, Vector3.right, new Vector3(0, 0, 1), new Vector3(0, 0, -1));
    }
    private void DrawRay(params Vector3[] roate)
    {
        RaycastHit hitv;
        foreach (var item in roate)
        {
			#if UNITY_EDITOR
            Debug.DrawRay(transform.position, transform.TransformDirection(item) * player.LenSword, Color.red);
			#endif
            if (Physics.Raycast(transform.position, transform.TransformDirection(item), out hitv, player.LenSword))
            {
            	ai_enemy enemy;
	            if (player.AttakAction && hitv.transform.CompareTag("Enemy") && 
		            hitv.transform.TryGetComponent<ai_enemy>(out enemy) &&
                    hitv.collider.isTrigger == false && player.AttakAction == true)
	            {
		            enemy.HPM = 5;
                    break;
                }
            }
        }
    }
}
