using System.Collections.Generic;
using UnityEngine;
public static class subManager
{
    public static sub load_Sub(string sub_adress)
    {
        var temp = Resources.Load<TextAsset>(sub_adress);
        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<sub>(temp.ToString());
        return data;
    }

    public static List<dialog> get_dialogs(string Actor, ref sub main_sub)
    {
        foreach (var item in main_sub.structure.dialogs)
        {
            if (Actor == item.actor)
            {
                return item.dialog;
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}