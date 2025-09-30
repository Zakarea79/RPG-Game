using System.Collections.Generic;

[System.Serializable]
public class Inventory_Desine
{
	[System.Serializable]
	public record object_einventory
	{
		public UnityEngine.Sprite image;
		public string id = null;
		public int item_size = 0;
	}
	public List<object_einventory> inventory = new List<object_einventory>();

	public void add_inventory(object_einventory obj)
	{
		if (inventory.Count == 0)
		{
			inventory.Add(obj);
			return;
		}
		foreach (var item in inventory)
		{
			if (obj.id == item.id)
			{
				item.item_size += obj.item_size;
				return;
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