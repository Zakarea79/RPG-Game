using UnityEngine;

public class inventtory_item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// other.GetComponent<inventory>().buttonAddItem.transform.position = transform.position;
			other.GetComponent<inventory>().buttonAddItem.gameObject.SetActive(true);
			print("Open Menue Add Item");
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<inventory>().buttonAddItem.gameObject.SetActive(false);
			print("Close Menue Add Item");
		}
	}
}
