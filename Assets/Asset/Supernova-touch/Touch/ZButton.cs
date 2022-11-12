using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static ListButtonData;

[HelpURL(@"https://github.com/Zakarea79/Free-Unity-2017-Touchpad")]
public class ZButton : MonoBehaviour
{
    private EventTrigger Et;
    [SerializeField] private string Keycode;
    [SerializeField] private KeyCode XKeyCode;
    public Color PressColor = new Color32(130 , 130 , 130 , 255) , normalColor = new Color32(255,255,255, 255);
    public Sprite PressButton , UpButton;
    private Image BaseColor;

    private void Awake() 
    {
        if(Keycode != ""
            && ListButtonData.Button_Down.ContainsKey(Keycode) == false
            && ListButtonData.Button_Up.ContainsKey(Keycode) == false
            && ListButtonData.Button_Press.ContainsKey(Keycode) == false)
        {
            ListButtonData.Button_Down.Add(Keycode , false);   
            ListButtonData.Button_Up.Add(Keycode , false);   
            ListButtonData.Button_Press.Add(Keycode , false);  
        }
        //--------------------------------
        if(XKeyCode != KeyCode.None
            && ListButtonData.Button_Down_KeyCode.ContainsKey(XKeyCode) == false
            && ListButtonData.Button_Up_KeyCode.ContainsKey(XKeyCode) == false
            && ListButtonData.Button_Press_KeyCode.ContainsKey(XKeyCode) == false)
        {
            ListButtonData.Button_Down_KeyCode.Add(XKeyCode , false);   
            ListButtonData.Button_Up_KeyCode.Add(XKeyCode , false);   
            ListButtonData.Button_Press_KeyCode.Add(XKeyCode , false);   
        }
    }
    void Start()
    {
        BaseColor = GetComponent<Image>();
        Et = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => 
        {
            if(Keycode != "")
            {
                ListButtonData.Button_Up[Keycode] = true;
                ListButtonData.Button_Press[Keycode] = false;
            }
            if(XKeyCode != KeyCode.None)
            {
                ListButtonData.Button_Up_KeyCode[XKeyCode] = true;
                ListButtonData.Button_Press_KeyCode[XKeyCode] = false;
            }
            BaseColor.color = normalColor;
            BaseColor.sprite = UpButton;
        });

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => 
        {
            if(Keycode != "")
            {
                ListButtonData.Button_Down[Keycode] = true;
                ListButtonData.Button_Press[Keycode] = true;
            }
            if(XKeyCode != KeyCode.None)
            {
                ListButtonData.Button_Down_KeyCode[XKeyCode] = true;
                ListButtonData.Button_Press_KeyCode[XKeyCode] = true;
            }
            BaseColor.color = PressColor;
            BaseColor.sprite = PressButton;
        });

        Et.triggers.Add(entryDown);
        Et.triggers.Add(entryUp);
    }
}
