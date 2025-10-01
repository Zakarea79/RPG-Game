using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class attak_manager : MonoBehaviour
{
    [SerializeField] private ZButton main_button;
    [SerializeField] private Button next, previwe;
    private string[] attak_list = new string[] { "sowrd_attak", "sowrd_attak_cercl", "magic_attak" };
    private int index = 0;
    void Start()
    {

        foreach (var item in attak_list)
        {
            ListButtonData.Button_Down.Add(item, false);
            ListButtonData.Button_Up.Add(item, false);
            ListButtonData.Button_Press.Add(item, false);
        }
        main_button.Keycode = attak_list[0];
        next.onClick.AddListener(() =>
        {
            main_button.Keycode = attak_list[index];
            main_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attak_list[index];
            index++;
            if (index > attak_list.Length - 1)
                index = 0;

        });
        previwe.onClick.AddListener(() =>
        {
            main_button.Keycode = attak_list[index];
            main_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attak_list[index];
            index--;
            if (index < 0)
                index = attak_list.Length - 1;
        });
    }

}
