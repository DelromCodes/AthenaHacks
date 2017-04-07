using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSimulator {

	protected ResourceManager resourceManager;
	protected Player player;
	protected Enemy enemy;

	//Projectile related
	protected List<Projectile> projectiles = new List<Projectile>();
	protected List<Projectile> projectilesToRemove = new List<Projectile>();
	protected string FIRE_PROJECTILE = "FireProjectile";

	//State related
	protected bool playerAlive = false;

	//Dialog related
	protected string DRAGON_DIALOG = "DragonDialog";

	public void Init(Player player_, Enemy enemy_, ResourceManager resourceManager_) {
		player = player_;
		enemy = enemy_;
		resourceManager = resourceManager_;
	}

	public void StartGame() {
		playerAlive = true;
	}

	//Update methods
	public void Update() {
		if (playerAlive) {
			ResolvePlayerInput();
			ResolveMovement();
			ResolveCollisions();
			ResolveCleanUp();
		}
	}

	//Collision methods
	protected void ResolveCollisions() {
		ResolvePlayerCollisionWithEnemy();
		ResolveProjectileCollisionWithEnemy();
	}

	protected void ResolvePlayerCollisionWithEnemy() {
		if (HasCollision(player.Collider, enemy.Collider)) {
			//GameOver
			resourceManager.LoadDialog(DRAGON_DIALOG);
			Debug.Log("GameOver!");
			playerAlive = false;
		}
	}

	protected void ResolveProjectileCollisionWithEnemy() {
		foreach (Projectile projectile in projectiles) {
			if (HasCollision(projectile.Collider, enemy.Collider)) {
				//enemy was hit
				Debug.Log("Enemy hit!");
				projectilesToRemove.Add(projectile);
			}
		}
	}

	protected bool HasCollision(Collider collider1, Collider collider2) {
		return collider1.bounds.Intersects(collider2.bounds);
	}


	//Input Methods
	protected void ResolvePlayerInput() {
		ResolvePlayerMovement();
		ResolvePlayerAttack();
	}

	protected void ResolvePlayerMovement() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			player.SetMovementDirection(Player.MoveDirection.LEFT);
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			player.SetMovementDirection(Player.MoveDirection.RIGHT);
		} else if (ShouldStopMoving) {
			player.SetMovementDirection(Player.MoveDirection.NONE);
		}
	}

	protected void ResolvePlayerAttack() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			player.Attack();
			SpawnProjectile();
			Debug.Log("Attacking");

		}
	}

	protected void SpawnProjectile() {
		GameObject go = resourceManager.LoadPrefab(FIRE_PROJECTILE);
		Projectile projectile = go.GetComponent<Projectile>();
		projectile.Init(player.transform.position, Vector3.up);
		projectiles.Add(projectile);
	}

	protected bool ShouldStopMoving {
		get {
			return Input.GetKey(KeyCode.None) //No keys are being pressed
				|| Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)  //We stopped going left and right wasn't held down
				|| Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow); //We stopped going right and left wasn't down
		}
	}

	//Movement methods
	protected void ResolveMovement() {
		player.OnMove();
		enemy.OnMove();

		Vector3 upperBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.9f, 0f));
		foreach (Projectile projectile in projectiles) {
			projectile.OnMove();
			if (projectile.transform.position.y > upperBoundPos.y) {
				if (!projectilesToRemove.Contains(projectile)) {
					projectilesToRemove.Add(projectile);
				}
			}
		}
	}

	//CleanUp methods
	protected void ResolveCleanUp() {
		foreach (Projectile projectile in projectilesToRemove) {
			projectiles.Remove(projectile);
			GameObject.Destroy(projectile.gameObject);
		}
		projectilesToRemove.Clear();
	}

}
