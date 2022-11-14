using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
	[SerializeField] private Material GlassMaterial;
	private Dictionary<GameObject , Material> objectRaymat = new Dictionary<GameObject , Material>();
    private void OnTriggerEnter(Collider other) 
	{
		MeshRenderer mes;
		if(other.name != "Player" && other.TryGetComponent<MeshRenderer>(out mes))
		{
			objectRaymat.Add(other.gameObject , mes.material);
			mes.material = GlassMaterial;
		}
	}

	private void OnTriggerExit(Collider other) 
	{
		if(objectRaymat.ContainsKey(other.gameObject))
		{
			other.gameObject.GetComponent<MeshRenderer>().material = objectRaymat[other.gameObject];
			objectRaymat.Remove(other.gameObject);
		}
	}
}
