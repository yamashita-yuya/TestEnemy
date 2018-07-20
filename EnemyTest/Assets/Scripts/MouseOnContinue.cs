using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnContinue : MonoBehaviour {
	public bool mouseOnContinue;
	float mouseOnContinueTime;
	GameObject continuePanel;
    // Use this for initialization
    void Start()
    {
        mouseOnContinue = false;
		mouseOnContinueTime = 0;
		continuePanel = GameObject.Find("ContinuePanel");
    }

    // Update is called once per frame
    void Update()
    {
		if (mouseOnContinue == true)
		{
			mouseOnContinueTime += Time.deltaTime;
			Debug.Log(mouseOnContinueTime);
			if (mouseOnContinueTime >= 3)
			{
				FindObjectOfType<Manager>().GameOpening();
			}
		}else if(mouseOnContinue == false){
			Debug.Log("OnMouseExit");
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
