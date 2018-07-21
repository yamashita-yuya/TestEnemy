using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnContinue : MonoBehaviour {
	public bool mouseOnContinue;
	float mouseOnContinueTime;
    //彩度
    float saturation;
    // Use this for initialization
    void Start()
    {
        mouseOnContinue = false;
		mouseOnContinueTime = 0;
        saturation = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (mouseOnContinue == true)
		{
			mouseOnContinueTime += Time.deltaTime;
            saturation += Time.deltaTime / 1.5f;
            GetComponent<Renderer>().material.color = Color.HSVToRGB(0.33f, saturation, 0.74f);
            //Debug.Log(mouseOnContinueTime);
			if (mouseOnContinueTime >= 2)
			{
				FindObjectOfType<Manager>().GameOpening();
			}
		}else if(mouseOnContinue == false){
            saturation = 0;
            mouseOnContinueTime = 0;
            //Debug.Log("OnMouseExit");
        }
    }
    public void OnMouseOver()
    {
        mouseOnContinue = true;
    }
    public void OnMouseExit()
    {
        mouseOnContinue = false;
		mouseOnContinueTime = 0;
    }
}
