using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSimulator {

	protected bool DEBUG_PLAYER_INVICIBLE = false;

	protected ResourceManager resourceManager;
	protected Player player;
	protected Enemy enemy;
	protected ScoreView scoreView;

	//Projectile related
	protected List<Projectile> projectiles = new List<Projectile>();
	protected List<Projectile> projectilesToRemove = new List<Projectile>();

	//Gems related
	protected List<GameObject> gems = new List<GameObject>();
	protected List<GameObject> gemstoRemove = new List<GameObject>();

	//State related
	protected bool playerAlive = false;
	protected int score = 0;

	public void Init(Player player_, Enemy enemy_, ScoreView scoreView_, ResourceManager resourceManager_) {
		player = player_;
		enemy = enemy_;
		scoreView = scoreView_;
		resourceManager = resourceManager_;
	}

	public void StartGame() {
		playerAlive = true;
		score = 0;
	}

	//Update methods
	public void Update() {
		if (playerAlive) {
			ResolvePlayerInput();
			ResolveMovement();
			ResolveCollisions();
			ResolveCleanUp();
			scoreView.UpdateScore(score);
		}
	}

	//Collision methods
	protected void ResolveCollisions() {
		if (!DEBUG_PLAYER_INVICIBLE) {
			ResolvePlayerCollisionWithEnemy ();
		}
//		ResolveProjectileCollisionWithEnemy();
	}

	protected void ResolvePlayerCollisionWithEnemy() {
		if (HasCollision(player.Collider, enemy.Collider)) {
			//GameOver
			resourceManager.LoadDialog(ResourceManager.DRAGON_DIALOG);
			playerAlive = false;
		}
	}

//	protected void ResolveProjectileCollisionWithEnemy() {
//		foreach (Projectile projectile in projectiles) {
//			if (HasCollision(projectile.Collider, enemy.Collider)) {
//				//Enemy was hit
//				score += 10;
//				projectilesToRemove.Add(projectile);
//				enemy.OnHit();
//				SpawnGems();
//			}
//		}
//	}

	protected bool HasCollision(Collider collider1, Collider collider2) {
		return collider1.bounds.Intersects(collider2.bounds);
	}


	//Input Methods
	protected void ResolvePlayerInput() {
		ResolvePlayerMovement();
//		ResolvePlayerAttack();
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

//	protected void ResolvePlayerAttack() {
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			player.Attack();
//			SpawnProjectile();
//			Debug.Log("Attacking");
//
//		}
//	}

	protected bool ShouldStopMoving {
		get {
			return Input.GetKey(KeyCode.None) //No keys are being pressed
				|| Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)  //We stopped going left and right wasn't held down
				|| Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow); //We stopped going right and left wasn't down
		}
	}

	//Spawn methods
	protected void SpawnProjectile() {
		GameObject go = resourceManager.LoadPrefab(ResourceManager.FIRE_PROJECTILE);
		Projectile projectile = go.GetComponent<Projectile>();
		projectile.Init(new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z), Vector3.up);
		projectiles.Add(projectile);
	}

	protected void SpawnGems() {
		SpawnGem(ResourceManager.SINGLE_CUT);
		SpawnGem(ResourceManager.PEAR_CUT);
		SpawnGem(ResourceManager.ROSE_CUT);
		SpawnGem(ResourceManager.ROUND_CUT);
	}

	protected void SpawnGem(string gemPrefabName) {
		GameObject go = resourceManager.LoadPrefab(gemPrefabName);
		go.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, enemy.transform.position.z);
		go.GetComponent<Rigidbody>().AddForce(Random.Range (-3f, 3f), Random.Range (0f, 2f), 0f);
		gems.Add(go);
	}

	//Movement methods
	protected void ResolveMovement() {
		player.OnMove();
		enemy.OnMove();

		Vector3 upperBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
		foreach (Projectile projectile in projectiles) {
			projectile.OnMove();
			if (projectile.transform.position.y > upperBoundPos.y) {
				if (!projectilesToRemove.Contains(projectile)) {
					projectilesToRemove.Add(projectile);
				}
			}
		}

		Vector3 lowerBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
		foreach (GameObject gem in gems) {
			if (gem.transform.position.y < lowerBoundPos.y) {
				gemstoRemove.Add(gem);
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

		foreach (GameObject gem in gemstoRemove) {
			gems.Remove(gem);
			GameObject.Destroy(gem);
		}
		gemstoRemove.Clear();
	}

}
