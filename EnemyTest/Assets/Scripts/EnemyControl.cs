using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
	public GameObject EnemyBullet;
	public GameObject Explosion;
	GameObject target;
	//public GameObject target;


	float speed = 1.7f;
	float intervalTime;
	int Enemylife = 10;


	// Use this for initialization
	void Start () {
		intervalTime = 0;
        //プレイヤーを変数に保存
		target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//enemyの移動
		//transform.Translate(0, 0, 1 * speed);
        //Enemyのプレイヤーを目指した移動
		if (target != null){
			//transformをプレイヤー向きにする
			this.transform.LookAt(target.transform);
            //相対Z軸を標準化したものをspeed分だけpositionに足していく？？
            this.transform.position += this.transform.forward.normalized * Time.deltaTime * speed;	
		}
        //たまの回転の制御
		Quaternion quat = Quaternion.Euler(0, 180, 0);
        //フレームごとに時間をインターバルタイムを計測
		intervalTime += Time.deltaTime;
		//自動でたまの生成
		if (intervalTime >= 0.8f)
		{
			intervalTime = 0.0f;
			var go = Instantiate(EnemyBullet);
			//goの親(座標)を設定
			go.transform.parent = this.transform;
			//ローカル座標を指定
			go.transform.localPosition = new Vector3(0, 0, 0);
			go.transform.localEulerAngles = new Vector3(0, 0, 0);
			//親座標をリセット
			go.transform.parent = null;
		}
	}
	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == "PlayerBullet"){
			Enemylife -= 1;
			Destroy(coll.gameObject);
			//Debug.Log(Enemylife);
			if (Enemylife <= 1)
			{
				Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
				Destroy(this.gameObject);
				FindObjectOfType<ScoreUi>().AddPoint(50);
				FindObjectOfType<Manager>().AddDestroyEnemy();
			}
		}
	}
    //初期化
    public void InitEnemy() {
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
