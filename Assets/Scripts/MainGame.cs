using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainGame : MonoBehaviour {

	public Player Player;

	protected float moveIncrement = 0.1f;

	//Initialization methods
	public void Start() {
		//initialize gameboard
		//initialize HUD
	}

	protected void InitializeGameboard() {

	}

	protected void InitializeHUD() {

	}

	//Update methods
	public void Update() {
		HandleInput();
	}

	//Input Methods
	protected void HandleInput() {
		HandleMovement();
		HandleAttack();
	}

	protected void HandleMovement() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			Player.Move(GetMoveLeftPosition);
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			Player.Move(GetMoveRightPosition);
		} else if (ShouldStopMoving) {
			Player.StopMove();
		}
	}

	protected void HandleAttack() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Player.Attack();
		}
	}

	protected bool ShouldStopMoving {
		get {
			return Input.GetKey(KeyCode.None) //No keys are being pressed
				|| Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)  //We stopped going left and right wasn't held down
				|| Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow); //We stopped going right and left wasn't down
		}
	}

	protected Vector3 GetMoveLeftPosition {
		get {
			//Update the WorldPosition to the left
			Vector3 newWorldPos = Player.transform.position;
			newWorldPos.x -= moveIncrement;

			//Clamp the newWorldPos to the viewport's edge
			Vector3 viewportPos = Camera.main.WorldToViewportPoint(newWorldPos);
			viewportPos.x = Mathf.Max(viewportPos.x, 0.1f); //Viewport is from 0.0f to 1.0f
			Vector3 clampedWorldPos = Camera.main.ViewportToWorldPoint(viewportPos);

			return clampedWorldPos;
		}
	}

	protected Vector3 GetMoveRightPosition {
		get {
			//Update the WorldPosition to the left
			Vector3 newWorldPos = Player.transform.position;
			newWorldPos.x += moveIncrement;

			//Clamp the newWorldPos to the viewport's edge
			Vector3 viewportPos = Camera.main.WorldToViewportPoint(newWorldPos);
			viewportPos.x = Mathf.Min(viewportPos.x, 0.9f); //Viewport is from 0.0 to 1.0f
			Vector3 clampedWorldPos = Camera.main.ViewportToWorldPoint(viewportPos);

			return clampedWorldPos;
		}
	}
}
