using UnityEngine;

public class inventtory_item : MonoBehaviour
{
	[SerializeField]
	private Inventory_Desine.object_einventory object_Einventory = new Inventory_Desine.object_einventory();
	private inventory inventory;
	private void Start()
	{
		inventory = GameObject.Find("Panel-inventory").GetComponent<inventory>();
		inventory.buttonAddItem.onClick.AddListener(() =>
		{
			inventory.inventory_Desine.add_inventory(object_Einventory);
			print(inventory.inventory_Desine.inventory.Count);
		});
	}
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			inventory.buttonAddItem.gameObject.SetActive(true);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			inventory.buttonAddItem.gameObject.SetActive(false);
			print("Close Menue Add Item");
		}
	}
}
