using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class object_einventory
{
	[SerializeField] private int How_Item;
	public int max_item = 10;
	public int min_item = 0;
	public string name_item ;
	public string descreption;
	public Sprite image_item ;
	
	public int HowItem
	{
		set 
		{
			
			How_Item = value;
			if(How_Item > max_item)
			{
				How_Item = max_item;
			}
			else if(How_Item < min_item)
			{
				How_Item = min_item;
			}
		}
		get{return How_Item;}
	}
}

public class Enventory : MonoBehaviour
{
	public object_einventory[] columns;
	
	protected void Start()
	{
		InvokeRepeating("test" , .3f , .3f);
	}
	private void test()
	{
		columns[0].HowItem += 1;
		print(columns[0].HowItem);
	}
}
