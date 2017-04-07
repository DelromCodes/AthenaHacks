using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

	public Text ScoreLabel;

	public void UpdateScore(int score) {
		ScoreLabel.text = "Score: " + score.ToString("N0");
	}
}
