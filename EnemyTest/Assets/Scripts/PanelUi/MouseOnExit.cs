using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnExit : MonoBehaviour {
	public bool mouseOnExit;
    float mouseOnExitTime;
    //彩度
    float saturation;
    // Use this for initialization
    void Start()
    {
        mouseOnExit = false;
        mouseOnExitTime = 0;
        saturation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOnExit == true)
        {
            mouseOnExitTime += Time.deltaTime;
            saturation += Time.deltaTime/1.5f;
            GetComponent<Renderer>().material.color = Color.HSVToRGB(0.83f, saturation, 0.74f);
            Debug.Log(saturation);
            if (mouseOnExitTime >=2)
            {
                FindObjectOfType<Manager>().GameOpening();
            }
        }
        else if (mouseOnExit == false)
        {
            saturation = 0;
            mouseOnExitTime = 0;
           // Debug.Log("noLonger");
        }
    }
    public void OnMouseOver()
    {
        mouseOnExit = true;
    }
    public void OnMouseExit()
    {
        mouseOnExit = false;
    }
}
