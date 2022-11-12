using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
//using static ListButtonData;

[HelpURL(@"https://github.com/Zakarea79/Free-Unity-2017-Touchpad")]
public class ZJoystoke : MonoBehaviour
{
    private EventTrigger Et;
    //--------------------------------------------AxisName-----------------------------------------------------
	[SerializeField] private string AxisName_H , AxisName_V;
    public bool BlockX , BlockY;
    //----------------------------------------------Range------------------------------------------------------
    [Range(0 , 1f)][SerializeField] private float Range =.8f;
    //----------------------------------------------Color-----------------------------------------------------
    public Color PressColor = new Color32(130 , 130 , 130 , 255) , NormalColor = new Color32(255,255,255, 255);
    public Color BakgrundPressColor = new Color32(130 , 130 , 130 , 255) , BakgrundNormalColor = new Color32(255,255,255, 255);
    //----------------------------------------------Sprite----------------------------------------------------
    public Sprite PressButton , UpButton , BakgrundPressButton , BakgrundUpButton;
    //-------------------------------------------------Image--------------------------------------------------
    private Image BaseColor;
    private Image BagrundBaseColor;
    //------------------------------------------------------------------------------------------------------
    private void Awake() 
    {
        if(AxisName_H != "" && ListButtonData.Axis.ContainsKey(AxisName_H) == false) 
            ListButtonData.Axis.Add(AxisName_H , 0);
        if(AxisName_V != "" && ListButtonData.Axis.ContainsKey(AxisName_V) == false) 
	        ListButtonData.Axis.Add(AxisName_V , 0);
    }
    void Start()
    {
        BaseColor = GetComponent<Image>();
        BagrundBaseColor = transform.parent.GetComponent<Image>();
        Et = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((dtat) => 
        {
            DragEvent((PointerEventData)dtat);
        });

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => 
        {
            BaseColor.color = NormalColor;
            BaseColor.sprite = UpButton;

            BagrundBaseColor.color = BakgrundNormalColor;
            BagrundBaseColor.sprite = BakgrundUpButton;

            transform.position = transform.parent.position;
            ListButtonData.Axis[AxisName_H] = 0;
	        ListButtonData.Axis[AxisName_V] = 0;
            
	       
        });

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => 
        {
            BaseColor.color = PressColor;
            BaseColor.sprite = PressButton;

            BagrundBaseColor.color = BakgrundPressColor;
            BagrundBaseColor.sprite = BakgrundPressButton;
        });

        Et.triggers.Add(entry);
        Et.triggers.Add(entryDown);
        Et.triggers.Add(entryUp);
    }
    
	public void RestData()
	{
		ListButtonData.Axis[AxisName_H] = 0;
		ListButtonData.Axis[AxisName_V] = 0;
	}
    
    public void DragEvent(PointerEventData data)
    {
        transform.position = data.position;
        float X = Mathf.Clamp(transform.localPosition.x , -Range * 100 , Range * 100);
        float Y = Mathf.Clamp(transform.localPosition.y , -Range * 100 , Range * 100);
        transform.localPosition = new Vector2(BlockX == true ? 0 : X , BlockY == true ? 0 : Y);

        if(AxisName_H != "")
        {
            float Axis_H = Mathf.Clamp(transform.localPosition.x , -Range * 100 , Range * 100f) / (Range  * 100);
            ListButtonData.Axis[AxisName_H] = Axis_H.Equals(System.Single.NaN) ? 0 : Axis_H;
        }
        else
        {
            Debug.LogError("AxisName_H Equals Null");
        }
        if(AxisName_V != "")
        {
            float Axis_V = Mathf.Clamp(transform.localPosition.y , -Range * 100 , Range * 100f) / (Range  * 100);
            ListButtonData.Axis[AxisName_V] = Axis_V.Equals(System.Single.NaN) ? 0 : Axis_V;
        }
        else
        {
            Debug.LogError("AxisName_V Equals Null");
        }
    }
}
