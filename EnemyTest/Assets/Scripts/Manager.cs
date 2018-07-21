using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	//ゲームステート
	public enum GameState
	{
		Opening,
        Playing,
        Clear,
        Over
	}
	//今のステート
	public GameState currentState = GameState.Opening;
	//パネル
	private GameObject panel;
	//パネルタイトル
	private GameObject title;
    //パネルタイトルテキスト
	private TextMesh titleText;
    //パネルトータルスコア
	private GameObject yourScore;
    //パネルトータルスコアテキスト
	private TextMesh yourScoreText;
	//パネルイグジット
	private GameObject exitPanel;
    //パネルエンド3dテキスト
	private GameObject exitPanelText;
    //パネルコンティニュー
	private GameObject continuePanel;
	//パネルコンティニュー3dテキスト
	private GameObject continuePanelText;
	//倒した敵の数
	int countDestroyEnemy;
    //色相
    int hueTitle;
    int hueYourScore;
    //ゲーム中に得られたスコア
    int nowScore;
    //敵の存在の確認
    GameObject enemy;

	// Use this for initialization
	void Start () {
		//ステージ上のパネル上のUI系ゲームオブジェクトを取得(タグまとめとかもあり)
		panel = GameObject.Find("Panel");
		title = GameObject.Find("Title");
		yourScore = GameObject.Find("YourScore");
		exitPanel = GameObject.Find("ExitPanel");
		exitPanelText = GameObject.Find("ExitPanelText");
		continuePanel = GameObject.Find("ContinuePanel");
		continuePanelText = GameObject.Find("CoutinuePanelText");
        //パネルを非表示
		panel.SetActive(false);
        title.SetActive(false);
        yourScore.SetActive(false);
        exitPanel.SetActive(false);
        exitPanelText.SetActive(false);
        continuePanel.SetActive(false);
        continuePanelText.SetActive(false);
		//タイトルのテキスト
		titleText = title.GetComponent<TextMesh>();
		//トータルスコアのテキスト
        yourScoreText = yourScore.GetComponent<TextMesh>();
        //オープニング
		GameOpening();
		//倒した敵の数
        countDestroyEnemy = 0;
        //色相
        hueTitle = 0;
        hueYourScore = 0;
        //ゲーム中に得られたスコア
        nowScore = 0;
        //敵の存在の確認
        enemy = GameObject.Find("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
		//ゲーム開始時でかつ、Spaceキーがおされたら実行
		if(currentState == GameState.Opening && Input.GetKeyDown(KeyCode.Space)){
			Dispatch(GameState.Playing);
		}
        //プレイ中
		if(currentState == GameState.Playing){
			//ゲームクリアーになる(クリアー条件は仮で敵10体)
			if(countDestroyEnemy >= 10){
				Dispatch(GameState.Clear);
			}
		}
	}
    //倒した敵の数の加算
	public void AddDestroyEnemy(){
		countDestroyEnemy += 1;
	}
    //トータルスコアの計算
	public void AddYourScore(int point){
		yourScoreText.text = "Your Score:" + point;
	}
	//パネルUIの活性化
	void OpenPanel(){
		panel.SetActive(true);
		title.SetActive(true);
		yourScore.SetActive(true);
		exitPanel.SetActive(true);
		exitPanelText.SetActive(true);
		continuePanel.SetActive(true);
		continuePanelText.SetActive(true);
	}
	//パネルUIの非活性化
	void ClosePanel(){
		panel.SetActive(false);
        title.SetActive(false);
        yourScore.SetActive(false);
        exitPanel.SetActive(false);
		exitPanelText.SetActive(false);
        continuePanel.SetActive(false);
        continuePanelText.SetActive(false);
	}
	//オブジェクトやスコアを初期位置に戻す
	void AllInit()
    {
        //敵の初期化
        if (enemy != null)
        {
            FindObjectOfType<EnemyControl>().InitEnemy();
        }
        //カメラの初期化
        FindObjectOfType<CameraControl>().InitCamera();
        //スコアの初期化
        FindObjectOfType<ScoreUi>().InitScore();
        countDestroyEnemy = 0;
        //ライフの初期化
        FindObjectOfType<LifeUi>().InitLife();  
        //タイムの初期化
        FindObjectOfType<TimeUi>().InitTime();  
        //プレイヤーの初期化？？
        //FindObjectOfType<PlayerControl>().InitPlayer();
    }
	//ステートによる、ゲーム画面の切り分け
	public void Dispatch(GameState state){
		GameState oldState = currentState;
		currentState = state;
		switch (state)
		{
			case GameState.Opening:
				GameOpening();
				break;
			case GameState.Playing:
				GameStart();
				break;
			case GameState.Clear:
				GameClear();
				break;
			case GameState.Over:
    			if (oldState == GameState.Playing){
    			GameOver();
    		    }
		        break;
		}
	}
    //オープニング処理
	public void GameOpening(){
		//ステート変更
		currentState = GameState.Opening;
        //ContinueとExit非表示
		exitPanel.SetActive(false);
        exitPanelText.SetActive(false);
        continuePanel.SetActive(false);
        continuePanelText.SetActive(false);
		//タイトルを表示する
		panel.SetActive(true);
        title.SetActive(true);
        hueTitle = 35;
       float hueTitleChanged = hueTitle / 360;
        SetTitle("Game Start", Color.HSVToRGB(0.3f,1,1));
        //トータルスコアの表示をかえる
		yourScore.SetActive(true);
        hueYourScore = 75;
        float hueYourScoreCanged = hueYourScore / 360;
        SetYourScoer("まばたきかSpaceキーを押してください", Color.HSVToRGB(0.1f, 1, 1));
        //オブジェクトやスコアを初期位置に戻す
        AllInit();
    }
    //ゲームスタート処理
	void GameStart(){
		//パネルの非活性化
		ClosePanel();
	}
    //ゲームクリアー処理
	void GameClear(){
		//タイトルを変更する
		SetTitle("Game Clear!!", Color.HSVToRGB(0.2f, 1, 1));
        //トータルスコアを変更する
        nowScore = FindObjectOfType<ScoreUi>().score;
        SetYourScoer("Your Score:" + nowScore, Color.HSVToRGB(0.1f, 1, 1));
        //パネルを表示する
        OpenPanel();
	}
    //ゲームオーバー処理
	public void GameOver(){
        //タイトルを変更する
        SetTitle("Game Over!!", Color.HSVToRGB(0, 1, 1));
        //トータルスコアを変更する
        nowScore = FindObjectOfType<ScoreUi>().score;
        SetYourScoer("Your Score:" + nowScore, Color.HSVToRGB(0.1f, 1, 1));
        //パネルを表示する
        OpenPanel();
    }
    //パネルタイトルの変更
	void SetTitle(string message, Color color){
		titleText.text = message;
		titleText.color = color;
	}
	void SetYourScoer(string message, Color color)
    {
		yourScoreText.text = message;
		yourScoreText.color = color;
    }
}
