using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListButtonData
{
    public static Dictionary<string, bool> Button_Press = new Dictionary<string, bool>();
    public static Dictionary<string, bool> Button_Down = new Dictionary<string, bool>();
    public static Dictionary<string, bool> Button_Up = new Dictionary<string, bool>();
    //------------------------------------------------------------------------------------
    public static Dictionary<KeyCode, bool> Button_Press_KeyCode = new Dictionary<KeyCode, bool>();
    public static Dictionary<KeyCode, bool> Button_Down_KeyCode = new Dictionary<KeyCode, bool>();
    public static Dictionary<KeyCode, bool> Button_Up_KeyCode = new Dictionary<KeyCode, bool>();
    //-----------------------------------------------------------------------------------
    public static Dictionary<string, float> Axis = new Dictionary<string, float>();
}

public static class ZInput
{

    public static bool GetKeyDown(string key)
	{
		if (ListButtonData.Button_Down.ContainsKey(key) == true)
		{
        	bool temp = ListButtonData.Button_Down[key];
			ListButtonData.Button_Down[key] = false;
			return temp;
		}
		return false;
    }

    public static bool GetKeyUp(string key)
	{
		if (ListButtonData.Button_Up.ContainsKey(key) == true)
		{
			bool temp = ListButtonData.Button_Up[key];
        	ListButtonData.Button_Up[key] = false;
        	return temp;
		}
		return false;
    }

    public static bool GetKeyPress(string key)
	{
		if (ListButtonData.Button_Press.ContainsKey(key) == true)
		{
			return ListButtonData.Button_Press[key];
		}
		return false;
    }
    //-----------------------------------------------------------
    public static bool GetKeyDown(KeyCode key)
	{
		if (ListButtonData.Button_Down_KeyCode.ContainsKey(key) == true)
		{
        	bool temp = ListButtonData.Button_Down_KeyCode[key];
        	ListButtonData.Button_Down_KeyCode[key] = false;
			return temp;
		}
		return false;
    }

    public static bool GetKeyUp(KeyCode key)
	{
		if (ListButtonData.Button_Up_KeyCode.ContainsKey(key) == true)
		{
        	bool temp = ListButtonData.Button_Up_KeyCode[key];
        	ListButtonData.Button_Up_KeyCode[key] = false;
			return temp;
		}
		return false;
    }

    public static bool GetKeyPress(KeyCode key)
	{
		if (ListButtonData.Button_Press_KeyCode.ContainsKey(key) == true)
		{
			return ListButtonData.Button_Press_KeyCode[key];
		}
		return false;
    }
    //------------------------------------------------

    public static float GetAxis(string NameAxis)
	{
		if (ListButtonData.Axis.ContainsKey(NameAxis) == true)
		{
			return ListButtonData.Axis[NameAxis];
		}
		return System.Single.NaN;
    }
}