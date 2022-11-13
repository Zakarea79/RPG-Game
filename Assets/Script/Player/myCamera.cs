using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
{
	[SerializeField] private Vector3 PosCamera;
	[SerializeField] private Vector3 RoaCamera;
	[SerializeField] private Transform Player;
	[SerializeField] private Material GlassMaterial;

	private Transform Look;
	private GameObject UseGlassMateral;
	private Material DefultMaterial;
	RaycastHit hit;

	private void Awake() {
		Look = transform.GetChild(0).transform;
	}
    void Update()
    {
	    transform.position = Vector3.Lerp(transform.position , Player.position + PosCamera , .1f);
		Look.position = transform.position;
	    Look.LookAt(Player.position + RoaCamera);
	    if (Physics.Raycast(Look.position, Look.TransformDirection(Vector3.forward), out hit))
	    {
			#if UNITY_EDITOR
		    Debug.DrawRay(transform.position, Look.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			#endif
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
