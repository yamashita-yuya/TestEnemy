using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	[SerializeField] private GameObject Player;
	Vector3 targetPos;
	// Use this for initialization
	void Start()
	{
	    Player = GameObject.Find("Player");
		targetPos = Player.transform.position;
	}
	// Update is called once per frame
	void Update () {
		if (Player != null)
		{
			//targetの移動量分、カメラも移動する
			transform.position += Player.transform.position - targetPos;
			targetPos = Player.transform.position;
			//カメラの回転
			if(Input.GetMouseButton(0)){
				float horizontal = Input.GetAxis("Mouse X");
				transform.RotateAround(targetPos,Vector3.up, horizontal*Time.deltaTime*200f);
			}
            /*
			if (Input.GetKey(KeyCode.RightArrow))
			{
				float horizontal = 1.0f;
				transform.RotateAround(targetPos, Vector3.up, horizontal * Time.deltaTime * 200f);
				//Debug.Log(horizontal);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
            {
                float horizontal = -1.0f;
                transform.RotateAround(targetPos, Vector3.up, horizontal * Time.deltaTime * 200f);
                //Debug.Log(horizontal);
            }
            */
		}
	}
}
