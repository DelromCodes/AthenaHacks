using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Animator Animator;

	//Animator Triggers
	protected int scream;
	protected int strongAttack;
	protected int basicAttack;
	protected int getHit;
	protected int walk;
	protected int die;
	protected int run;
	protected int idle;

	//State related
	protected bool moving = false;

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
	public void StopMove() {
		if (moving) {
			Debug.Log ("Stop moving");
			AnimateIdle();
		}
	}

	public void Move(Vector3 position) {
		if (!moving) {
			Debug.Log ("Move");
			AnimateRun();
		}
		transform.position = position;
	}

	public void Attack() {
		AnimateBasicAttack();
	}

	public void OnHit() {
		AnimateGetHit();
	}

	//Attack related
	protected void AnimateScream() {
		Animator.SetTrigger(scream);
	}

	protected void AnimateBasicAttack() {
		Animator.SetTrigger(basicAttack);
	}

	protected void AnimateStrongAttack() {
		Animator.SetTrigger(strongAttack);
	}

	//Movement related
	protected void AnimateIdle() {
		Animator.SetTrigger(idle);
		moving = false;
	}

	protected void AnimateWalk() {
		Animator.SetTrigger(walk);
		moving = true;
	}

	protected void AnimateRun() {
		Animator.SetTrigger(run);
		moving = true;
	}

	//Damage related
	protected void AnimateGetHit() {
		Animator.SetTrigger(getHit);
		moving = false;
	}

	protected void AnimateDie() {
		Animator.SetTrigger(die);
		moving = false;
	}
}
