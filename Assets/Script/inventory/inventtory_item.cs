using UnityEngine;

public class inventtory_item : MonoBehaviour
{
	[SerializeField]
	private Inventory_Desine.object_einventory object_Einventory = new Inventory_Desine.object_einventory();
	private void Start()
	{
		var inventory = GameObject.Find("Player").GetComponent<inventory>();
		inventory.buttonAddItem.onClick.AddListener(()=>
		{
			inventory.inventory_Desine.add_inventory(object_Einventory);
		});
	}
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<inventory>().buttonAddItem.gameObject.SetActive(true);
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
