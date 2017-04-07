using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public enum MoveDirection {
		LEFT,
		RIGHT,
		NONE
	}

	public Animator Animator;
	public Collider Collider;

	//Animator Triggers
	protected int scream;
	protected int strongAttack;
	protected int basicAttack;
	protected int getHit;
	protected int walk;
	protected int die;
	protected int run;
	protected int idle;

	//internal related
	protected bool moving = false;
	protected float moveIncrement = 0.15f;
	protected MoveDirection moveDirection = MoveDirection.NONE;
	protected Vector3 nextPosition;

	//Initialization
	public void Awake() {
		SetUpAnimatorHashes();
	}

	protected void SetUpAnimatorHashes() {
		if (Animator == null) {
			Animator = GetComponent<Animator>();
		}
		scream = Animator.StringToHash("Scream");
		basicAttack = Animator.StringToHash("Basic Attack");
		strongAttack = Animator.StringToHash("Strong Attack");
		getHit = Animator.StringToHash("Get Hit");
		walk = Animator.StringToHash("Walk");
		die = Animator.StringToHash("Die");
		run = Animator.StringToHash("Run");
		idle = Animator.StringToHash("Idle");
	}

	//Public methods
	public void SetMovementDirection(MoveDirection moveDirection_) {
		moveDirection = moveDirection_;

	}

	public void OnMove() {
		switch(moveDirection) {
		case MoveDirection.LEFT:
			nextPosition = GetMoveLeftPosition;
			Move();
			break;
		case MoveDirection.RIGHT:
			nextPosition = GetMoveRightPosition;
			Move ();
			break;
		case MoveDirection.NONE:
			//Do nothing
			StopMove();
			break;
		default:
			//Do nothing
			StopMove();
			break;
		}
	}

	public void StopMove() {
		if (moving) {
			Debug.Log ("Stop moving");
			AnimateIdle();
		}
	}

	protected void Move() {
		if (!moving) {
			Debug.Log ("Move");
			AnimateRun();
		}
		transform.position = nextPosition;
	}
		
	public void Attack() {
		AnimateBasicAttack();
	}

	public void OnHit() {
		AnimateGetHit();
	}

	//Movement properties
	protected Vector3 GetMoveLeftPosition {
		get {
			//Update the WorldPosition to the left
			Vector3 newWorldPos = transform.position;
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
			Vector3 newWorldPos = transform.position;
			newWorldPos.x += moveIncrement;

			//Clamp the newWorldPos to the viewport's edge
			Vector3 viewportPos = Camera.main.WorldToViewportPoint(newWorldPos);
			viewportPos.x = Mathf.Min(viewportPos.x, 0.9f); //Viewport is from 0.0 to 1.0f
			Vector3 clampedWorldPos = Camera.main.ViewportToWorldPoint(viewportPos);

			return clampedWorldPos;
		}
	}

	//Animation: Attack related
	protected void AnimateScream() {
		Animator.SetTrigger(scream);
	}

	protected void AnimateBasicAttack() {
		Animator.SetTrigger(basicAttack);
	}

	protected void AnimateStrongAttack() {
		Animator.SetTrigger(strongAttack);
	}

	//Animation: Movement related
	protected void AnimateIdle() {
		moving = false;
		Animator.SetTrigger(idle);
	}

	protected void AnimateWalk() {
		moving = true;
		Animator.SetTrigger(walk);
	}

	protected void AnimateRun() {
		moving = true;
		Animator.SetTrigger(run);
	}

	//Damage related
	protected void AnimateGetHit() {
		moving = false;
		Animator.SetTrigger(getHit);
	}

	protected void AnimateDie() {
		moving = false;
		Animator.SetTrigger(die);
	}
}
