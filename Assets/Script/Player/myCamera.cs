using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
{
	[SerializeField] private Vector3 PosCamera;
	[SerializeField] private Vector3 RoaCamera;
	[SerializeField] private Transform Player;
	[SerializeField] private Material GlassMaterial;
	private GameObject UseGlassMateral;
	private Material DefultMaterial;
	
    void Update()
    {
	    transform.position = Vector3.Lerp(transform.position , Player.position + PosCamera , .1f);
	    transform.LookAt(Player.position + RoaCamera);
	    RaycastHit hit;
	    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
	    {
		    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
		    if(hit.transform.name != "Player")
		    {
		    	if(UseGlassMateral != hit.transform.gameObject)
		    	{
		    		MeshRenderer mes;
		    		if(hit.transform.TryGetComponent<MeshRenderer>(out mes))
			    	{
		    			UseGlassMateral = hit.transform.gameObject;
				    	DefultMaterial = mes.material;
				    	mes.material = GlassMaterial;
				    	//print("InVisble");
			    	}
		    	}
		    }
		    else
		    {
		    	MeshRenderer mes;
			    if(UseGlassMateral != null && UseGlassMateral.TryGetComponent<MeshRenderer>(out mes))
			    {
			    	UseGlassMateral.GetComponent<MeshRenderer>().material = DefultMaterial;
			    	UseGlassMateral = null;
				    //print("Visble");
			    }
		    }
	    }
    }
}
