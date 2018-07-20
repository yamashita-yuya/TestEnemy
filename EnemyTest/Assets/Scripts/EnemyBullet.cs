using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	float bulletSpeed = 1.5f;
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 4);	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, bulletSpeed);
	}
}
