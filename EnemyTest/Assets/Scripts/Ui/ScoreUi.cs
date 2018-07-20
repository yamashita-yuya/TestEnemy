using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUi : MonoBehaviour {
	public TextMesh scoreText;
    private int score;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		scoreText.text = "Score:" + score.ToString();
    }

    public void AddPoint(int point)
    {
        score += point;
    }
}
