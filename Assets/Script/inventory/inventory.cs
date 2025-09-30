using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class inventory : MonoBehaviour
{
	[SerializeField] private Animator animator_Invet_Panel;
	[SerializeField] private Button button_invet_Handel;
	public Inventory_Desine inventory_Desine = new Inventory_Desine();
	[SerializeField] private RectTransform transform_content;
	[SerializeField] private GameObject Content;
	public Button buttonAddItem;
	int y_transform_content = 100;
	int pos_transform_content = 0;
	private void ScrollViewContent_Additem(Sprite image, string name, string abut)
	{
		transform_content.sizeDelta = new Vector2(transform_content.sizeDelta.x, y_transform_content);
		GameObject v = Instantiate(Content, new Vector2(0, 0), transform_content.transform.rotation, transform_content.transform);

		v.GetComponent<RectTransform>().anchoredPosition = (new Vector2(0, -pos_transform_content));
		v.GetComponent<RectTransform>().offsetMin = new Vector2(0, v.GetComponent<RectTransform>().offsetMin.y);
		v.GetComponent<RectTransform>().offsetMax = new Vector2(0, v.GetComponent<RectTransform>().offsetMax.y);
		v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
		v.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = abut;

		y_transform_content += 100;
		pos_transform_content += 100;
	}

	void remove_all_object_in_inventory()
	{
		if (animator_Invet_Panel.GetBool("handel") == false)
		{
			for (int i = 0; i < transform_content.childCount; i++)
			{
				Destroy(transform_content.GetChild(i).gameObject);
				print(animator_Invet_Panel.GetBool("handel"));
			}
			y_transform_content = 100;
			pos_transform_content = 0;
		}
	}
	protected void Start()
	{
		button_invet_Handel.onClick.AddListener(() =>
		{
			print(inventory_Desine.inventory.Count);
			if (animator_Invet_Panel.GetBool("handel") == false)
			{
				print(inventory_Desine.inventory.Count);
				foreach (var item in inventory_Desine.inventory)
				{
					print("add item");
					ScrollViewContent_Additem(item.image, item.id, System.Convert.ToString(item.item_size));
				}
				animator_Invet_Panel.SetBool("handel", true);
			}
			else
			{
				animator_Invet_Panel.SetBool("handel", false);
			}
		});
	}
}
