using System.Collections.Generic;
using UnityEngine;
public class sub_manager : MonoBehaviour
{
    public sub main_sub = null;
    private void Start()
    {
        main_sub = subManager.load_Sub("sub-en");
    }
    private List<dialog> temp = new List<dialog>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("non-player"))
        {
            temp = subManager.get_dialogs(other.GetComponent<extra_tag>().value, ref main_sub);
        }
    }
}
