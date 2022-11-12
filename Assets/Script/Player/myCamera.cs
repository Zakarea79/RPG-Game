using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
{
	[SerializeField] private Vector3 PosCamera;
	[SerializeField] private Transform Player;
   
    void Update()
    {
	    transform.position = Vector3.Lerp(transform.position , Player.position + PosCamera , .1f);
    }
}
