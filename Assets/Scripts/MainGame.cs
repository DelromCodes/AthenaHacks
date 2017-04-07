using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainGame : MonoBehaviour {

	public ResourceManager ResourceManager;
	public Player Player;
	public Enemy Enemy;
	public ScoreView ScoreView;

	protected UpdateSimulator updateSimulator = new UpdateSimulator();

	//Initialization methods
	public void Start() {
		updateSimulator.Init(Player, Enemy, ScoreView, ResourceManager);
		updateSimulator.StartGame();
	}

	public void Update() {
		updateSimulator.Update();
	}
}
