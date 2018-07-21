using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	//ライフ
	int playersLife = 100;
	GameObject lifeUi;
	//スコア
	GameObject scoreUi;
	//タイム
	GameObject timeUi;
    //移動系
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    float moveSpeed = 3f;
    //たま
    public GameObject Bullet;
    //銃弾が発射されるときの銃との相対位置
    private Vector3 bulletStart = new Vector3(0.0f, 0.0f, 0.0f);
    //銃弾が発射されるときの銃弾からの相対角度
    private Vector3 bulletRotate;
    //衝突判定系
    public GameObject Explosion;
    public GameObject Trap;
    float intervalTime;
    //敵の接触でのダメージ間隔
    float enemyIntervalTime;

    // Use this for initialization
    void Start () {
        //たまの相対角度の初期値
        bulletRotate = new Vector3(-1.0f, 0.0f, 0);
        //Playerのリジットボディの取得
        rb = GetComponent<Rigidbody>();
        //たまの発射間隔の初期化
        intervalTime = 0;
        //Enemyの発生間隔の初期化
        enemyIntervalTime = 0.0f;
        //Trapの生成
        for (int i = 0; i < 20;i++){
            Instantiate(Trap, new Vector3(0, 1, 1+3*i), Quaternion.identity);
        }
		//ライフとスコアとタイムのUi
		lifeUi = GameObject.Find("LifeUi");
		scoreUi = GameObject.Find("ScoreUi");
		timeUi = GameObject.Find("TimeUi");
    }
    
    // Update is called once per frame
    void Update () {
        //移動系
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
		//たまの生成
		intervalTime += Time.deltaTime;
        if(Input.GetKey("space")){
            if(intervalTime >= 0.1f){
                intervalTime = 0.0f;
                //insutansiateしたBulletを変数に保存する
                var go = Instantiate(Bullet);
                //goの親をPlayerにする
                go.transform.parent = this.transform;
                //親に対するローカル座標を与える
                go.transform.localPosition = bulletStart;
                go.transform.localEulerAngles = bulletRotate;
                //goの親をリセットする
                go.transform.parent = null;
            }
        }
     }
    //
    private void FixedUpdate()
    {
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
        //移動方向にスピードをかける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルをたす。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        //プレイヤーの向きを進行方向に
        if (moveForward!=Vector3.zero){
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
    //衝突判定系
    private void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "EnemyBullet"||coll.gameObject.tag == "Enemy"){
			playersLife -= 3;
			FindObjectOfType<LifeUi>().RemoveLife(3);
			Destroy(coll.gameObject);
        }
		if (playersLife <= 0)
        {
            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
	private void OnTriggerStay(Collider coll)
	{   
		enemyIntervalTime += Time.deltaTime;
		if (enemyIntervalTime >= 0.5f){
			enemyIntervalTime = 0.0f;
			playersLife -= 2;
			FindObjectOfType<LifeUi>().RemoveLife(2);
		}
		if (playersLife <= 0)
        {
            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
			Destroy(lifeUi);
			Destroy(scoreUi);
			Destroy(timeUi);
            FindObjectOfType<Manager>().GameOver();
        }
	}
}
