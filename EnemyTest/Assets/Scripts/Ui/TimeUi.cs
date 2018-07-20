using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUi : MonoBehaviour {
	private TextMesh timeUiText;
    private float time = 20;
	private float oldTime = 0;
    
    // Use this for initialization
    void Start()
    {
		timeUiText = GetComponent<TextMesh>();
    }
    
    // Update is called once per frame
    void Update()
    {   
		if(time <= 0 ){
			return;
		}
		time -= Time.deltaTime;
		if(time != oldTime){
			timeUiText.text = "Time:" + time.ToString() + "/20";
		}
		oldTime = time;
    }
}
