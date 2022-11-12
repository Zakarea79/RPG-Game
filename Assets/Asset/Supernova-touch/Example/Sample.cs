using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Update()
    {
        float x = ZInput.GetAxis("X");
        float y = ZInput.GetAxis("Y");
        transform.Translate(new Vector3(x, y , 0) * 5 * Time.deltaTime);
        //-------------------------------------------------------------
        if(ZInput.GetKeyDown("button1"))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
        if(ZInput.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
    }
}
