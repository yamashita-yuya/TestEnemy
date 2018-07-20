using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnExit : MonoBehaviour {
	public bool mouseOnExit;
    // Use this for initialization
    void Start()
    {
        mouseOnExit = false;
    }

    // Update is called once per frame
    void Update()
    {

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
