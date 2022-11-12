using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static ListButtonData;

public static class ListButtonData
{
    public static Dictionary<string , bool> Button_Press = new Dictionary<string , bool>();
    public static Dictionary<string , bool> Button_Down = new Dictionary<string , bool>();
    public static Dictionary<string , bool> Button_Up = new Dictionary<string , bool>();
    //------------------------------------------------------------------------------------
    public static Dictionary<KeyCode , bool> Button_Press_KeyCode = new Dictionary<KeyCode , bool>();
    public static Dictionary<KeyCode , bool> Button_Down_KeyCode = new Dictionary<KeyCode , bool>();
    public static Dictionary<KeyCode , bool> Button_Up_KeyCode = new Dictionary<KeyCode , bool>();
    //-----------------------------------------------------------------------------------
	public static Dictionary<string , float> Axis = new Dictionary<string , float>();
	
	public static Dictionary<string , Vector3> RotationAxis = new Dictionary<string , Vector3>();
}

public static class ZInput
{
    
    public static bool GetKeyDown(string key)
    {
        bool temp = ListButtonData.Button_Down[key];
        ListButtonData.Button_Down[key] = false;
        return temp;
    }

    public static bool GetKeyUp(string key)
    {
        bool temp = ListButtonData.Button_Up[key];
        ListButtonData.Button_Up[key] = false;
        return temp;
    }

    public static bool GetKeyPress(string key)
    {
        return ListButtonData.Button_Press[key];
    }
    //-----------------------------------------------------------
    public static bool GetKeyDown(KeyCode key)
    {
        bool temp = ListButtonData.Button_Down_KeyCode[key];
        ListButtonData.Button_Down_KeyCode[key] = false;
        return temp;
    }

    public static bool GetKeyUp(KeyCode key)
    {
        bool temp = ListButtonData.Button_Up_KeyCode[key];
        ListButtonData.Button_Up_KeyCode[key] = false;
        return temp;
    }

    public static bool GetKeyPress(KeyCode key)
    {
        return ListButtonData.Button_Press_KeyCode[key];
    }
    //------------------------------------------------

	public static float GetAxis(string NameAxis)
	{
		return ListButtonData.Axis[NameAxis];
	}
}