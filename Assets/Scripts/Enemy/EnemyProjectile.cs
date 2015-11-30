using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyProjectile : MonoBehaviour {

	public int damage = 1;
	public float velocity = 10f;
	public bool destroyOnHitDestructibleObject = true;
	public bool destroyOnHitPlayer = true;
	public bool destroyOnHitScenarioObject = true;
	public bool destroyOnHitWall = true;
	List<PlayerLife> playerLifesHit = new List<PlayerLife>();

	void Update () {
		transform.Translate(-Vector2.right * velocity * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag(Tags.destructibleObject)){
			collider.gameObject.GetComponent<DestructibleObject>().takeDamage(damage);
			if(destroyOnHitDestructibleObject){
				Destroy(gameObject);
			}
		}
		if(collider.CompareTag(Tags.player)){
			PlayerLife playerLife = collider.gameObject.GetComponent<PlayerLife>();
			HitPlayer(playerLife);
		}
		if(collider.CompareTag(Tags.scenarioObject)){
			if(destroyOnHitScenarioObject){
				Destroy(gameObject);
			}
		}
		if(collider.CompareTag(Tags.wall)){
			if(destroyOnHitWall){
				Destroy(gameObject);
			}
		}

	}

	void HitPlayer(PlayerLife playerLife){
		if(!playerLifesHit.Contains(playerLife)){
			playerLifesHit.Add(playerLife);
			playerLife.TakeDamage(damage);
			if(destroyOnHitPlayer){
				Destroy(gameObject);
			}
		}
	}

}