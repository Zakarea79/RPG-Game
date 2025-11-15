using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class inventtory_item : MonoBehaviour
{
	[SerializeField] private Inventory_Desine.object_einventory object_Einventory = new Inventory_Desine.object_einventory();
	private inventory inventory;
	private List<string> items = new List<string>();
	private void Start()
	{
		inventory = GameObject.Find("Panel-inventory").GetComponent<inventory>();
		items.AddRange(new string[] { "wod", "sowrd", "shield"});
		// inventory.buttonAddItem.onClick.AddListener(() =>
		// {
		// 	inventory.inventory_Desine.add_inventory(object_Einventory);
		// 	print(inventory.inventory_Desine.inventory.Count);
		// });
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			inventory.inventory_panel_show_object.gameObject.SetActive(true);
			// ScrollViewContent_Additem()
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			inventory.inventory_panel_show_object.gameObject.SetActive(false);
			remove_all_object_in_panle();
		}
	}


	[SerializeField] private GameObject Content;
	[SerializeField] private RectTransform transform_content;
	int y_transform_content = 20;
	int pos_transform_content = 0;

	private void remove_all_object_in_panle()
	{
		for (int i = 0; i < transform_content.childCount; i++)
		{
			Destroy(transform_content.GetChild(i).gameObject);
		}
		y_transform_content = 20;
		pos_transform_content = 0;
	}
	private void ScrollViewContent_Additem(string[] d)
	{
		transform_content.sizeDelta = new Vector2(transform_content.sizeDelta.x, y_transform_content);
		GameObject v = Instantiate(Content, new Vector2(0, 0), transform_content.transform.rotation, transform_content.transform);

		v.GetComponent<RectTransform>().anchoredPosition = (new Vector2(0, -pos_transform_content));
		v.GetComponent<RectTransform>().offsetMin = new Vector2(0, v.GetComponent<RectTransform>().offsetMin.y);
		v.GetComponent<RectTransform>().offsetMax = new Vector2(0, v.GetComponent<RectTransform>().offsetMax.y);
		// v.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = d.question;
		// v.GetComponent<info_dialogs>().info = d;
		v.GetComponent<Button>().onClick.AddListener(() =>
		{
			print("items");
		});

		y_transform_content += 20;
		pos_transform_content += 20;
	}
}
