using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainGame : MonoBehaviour {

	public Player Player;

	protected float MoveSensitivity = 0.1f;

	public void Start() {
		//initialize gameboard
		//initialize HUD
	}

	public void Update() {
		HandleInput();
	}

	protected void HandleInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			Player.Move(new Vector3 (Player.transform.position.x - MoveSensitivity, Player.transform.position.y, Player.transform.position.z));
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			Player.Move(new Vector3 (Player.transform.position.x + MoveSensitivity, Player.transform.position.y, Player.transform.position.z));
		} else if (ShouldStopMoving) {
			Player.StopMove();
		}
	}

	protected bool ShouldStopMoving {
		get {
			return Input.GetKey(KeyCode.None) //No keys are being pressed
				|| Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)  //We stopped going left and right wasn't held down
				|| Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow); //We stopped going right and left wasn't down
		}
	}

	protected void InitializeGameboard() {
		
	}

	protected void InitializeHUD() {
		
	}

}
