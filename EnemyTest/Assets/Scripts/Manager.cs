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
	void AllInit(){
		//FindObjectOfType<PlayerControl>().InitPlayer();
        //FindObjectOfType<CameraControl>().InitCamera();
        //FindObjectOfType<ScoreUi>().InitScore();
        //FindObjectOfType<LifeUi>().InitLife();
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
		SetTitle("Game Start", Color.HSVToRGB(35,95,100));
        //トータルスコアの表示をかえる
		yourScore.SetActive(true);
		SetYourScoer("まばたきかSpaceキーを押してください", Color.HSVToRGB(50, 95, 0));

	}
    //ゲームスタート処理
	void GameStart(){
		//パネルの非活性化
		ClosePanel();
		//オブジェクトやスコアを初期位置に戻す
        AllInit();
	}
    //ゲームクリアー処理
	void GameClear(){
		//タイトルを変更する
		SetTitle("Game Clear!!", Color.HSVToRGB(50, 95, 0));
		//パネルを表示する
		OpenPanel();
	}
    //ゲームオーバー処理
	void GameOver(){
		
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
