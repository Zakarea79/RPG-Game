using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
{
	[SerializeField] private Vector3 PosCamera;
	[SerializeField] private Vector3 RoaCamera;
	[SerializeField] private Transform Player;
	
	[SerializeField] private Transform Look;

    void Update()
    {
		Look.position = transform.position;
		Look.transform.LookAt(Player.position + RoaCamera);
	    Look.localScale = new Vector3(.01f , .01f , Vector3.Distance(Player.position , Look.position));

	    transform.position = Vector3.Lerp(transform.position , Player.position + PosCamera , .1f);
	    
		#if UNITY_EDITOR
	    Debug.DrawRay(transform.position, Look.TransformDirection(Vector3.forward) * Vector3.Distance(Player.position , Look.position), Color.yellow);
		#endif
    }
}
