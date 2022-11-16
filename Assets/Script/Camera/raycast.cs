using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
	[SerializeField] private Material GlassMaterial;
	
	private Dictionary<GameObject , List<Material>> objectRaymat = new Dictionary<GameObject , List<Material>>();
	
	private void OnTriggerEnter(Collider other)
	{
		MeshRenderer meshRenderer;
		SkinnedMeshRenderer skinnedMeshRenderer;
		var ListGlas = new List<Material>();

		if(other.name != "Player" && other.TryGetComponent<MeshRenderer>(out meshRenderer))
		{
			var list = new List<Material>();
			list.AddRange(meshRenderer.materials);
			objectRaymat.Add(other.gameObject , list);
			
			for (int i = 0; i < meshRenderer.materials.Length; i++) 
			{
				ListGlas.Add(GlassMaterial);
			}
			meshRenderer.materials = ListGlas.ToArray();
		}
		else if(other.name != "Player" && other.TryGetComponent<SkinnedMeshRenderer>(out skinnedMeshRenderer))
		{
			var list = new List<Material>();
			list.AddRange(skinnedMeshRenderer.materials);
			objectRaymat.Add(other.gameObject , list);
			
			for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++) 
			{
				ListGlas.Add(GlassMaterial);
			}
			skinnedMeshRenderer.materials = ListGlas.ToArray();
		}
	}

	private void OnTriggerExit(Collider other) 
	{
		if(objectRaymat.ContainsKey(other.gameObject))
		{
			MeshRenderer meshRenderer;
			SkinnedMeshRenderer skinnedMeshRenderer;
			var ListGlas = new List<Material>();
			
			if(other.TryGetComponent<MeshRenderer>(out meshRenderer))
			{
				
				for (int i = 0; i < meshRenderer.materials.Length; i++) 
				{
					ListGlas.Add(objectRaymat[other.gameObject][i]);
				}
				meshRenderer.materials = ListGlas.ToArray();
				objectRaymat.Remove(other.gameObject);
			}
			else if(other.TryGetComponent<SkinnedMeshRenderer>(out skinnedMeshRenderer))
			{
				for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++) 
				{
					ListGlas.Add(objectRaymat[other.gameObject][i]);
				}
				skinnedMeshRenderer.materials = ListGlas.ToArray();
				objectRaymat.Remove(other.gameObject);
			}
		}
	}
}
