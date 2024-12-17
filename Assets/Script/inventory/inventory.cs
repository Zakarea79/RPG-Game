using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]


public class Inventory_Desine
{
	public class object_einventory
	{
		public string id = null;
		public int item_size = 0;
	}
	public List<object_einventory> inventory = new List<object_einventory>();

	public void add_inventory(object_einventory obj)
	{
		foreach (var item in inventory)
		{
			if (obj.id == item.id)
			{
				item.item_size += obj.item_size;
			}
			else
			{
				inventory.Add(obj);
			}
		}
	}
	public void Use_inventory_item(string item_id, int Size)
	{
		foreach (var item in inventory)
		{
			if (item.id == item_id && item.item_size >= Size)
			{
				item.item_size -= Size;
			}
			if (item.item_size == 0)
			{
				inventory.Remove(item);
			}
		}
	}
}
public class inventory : MonoBehaviour
{
	[SerializeField] private Animator animator_Invet_Panel;
	[SerializeField] private Button button_invet_Handel;
	[SerializeField] private Transform ScrollViewContent;
	public string[] item_id = { "jem", "wood", "sord" };
	private Inventory_Desine inventory_Desine = new Inventory_Desine();
	[SerializeField] private RectTransform transform_content;
	[SerializeField] private GameObject Content;
	public Button buttonAddItem;
	int y_transform_content = 50;
	int pos_transform_content = 0;
	private void ScrollViewContent_Additem()
	{
		transform_content.sizeDelta = new Vector2(transform_content.sizeDelta.x, y_transform_content);
		GameObject v = Instantiate(Content, new Vector2(0, 0), transform_content.transform.rotation, transform_content.transform);
		v.GetComponent<RectTransform>().anchoredPosition = (new Vector2(0, -pos_transform_content));
		y_transform_content += 50;
		pos_transform_content += 50;
	}
	protected void Start()
	{
		
		button_invet_Handel.onClick.AddListener(() =>
		{
			if (animator_Invet_Panel.GetBool("handel") == false)
			{
				animator_Invet_Panel.SetBool("handel", true);
				for (int i = 0; i < 10; i++)
				{
					ScrollViewContent_Additem();
				}
			}
			else
			{
				animator_Invet_Panel.SetBool("handel", false);
			}
		});
	}
}
