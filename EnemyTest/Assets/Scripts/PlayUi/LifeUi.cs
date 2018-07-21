using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUi : MonoBehaviour {
	public TextMesh lifeUiText;
	private int life = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeUiText.text = "Life:" + life.ToString() + "/100";
	}
    
	public void RemoveLife(int damage){
		life -= damage;
	}
    //ライフの初期化
    public void InitLife() {
        life = 100;
    }
}
