﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

	public int damage = 1;
	public float velocity = 10f;
	public bool destroyOnHitDestructibleObject = true;
	public bool destroyOnHitEnemy = true;
	public bool destroyOnHitScenarioObject = true;
	public bool destroyOnHitWall = true;
	List<EnemyLife> enemyLifesHit = new List<EnemyLife>();

	void Update () {
		transform.Translate(Vector2.right * velocity * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag(Tags.destructibleObject)){
			collider.gameObject.GetComponent<DestructibleObject>().takeDamage(damage);
			if(destroyOnHitDestructibleObject){
				Destroy(gameObject);
			}
		}
		if(collider.CompareTag(Tags.enemy)){
			EnemyLife enemyLife = collider.gameObject.GetComponent<EnemyLife>();
			HitAnEnemy(enemyLife);
		}
		if(collider.CompareTag(Tags.enemyExtraCollider)){
			EnemyLife enemyLife = collider.gameObject.GetComponent<EnemyExtraCollider>().enemyLifeToUse;
			HitAnEnemy(enemyLife);
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

	void HitAnEnemy(EnemyLife enemyLife){
		if(!enemyLifesHit.Contains(enemyLife)){
			enemyLifesHit.Add(enemyLife);
			enemyLife.TakeDamage(damage);
			if(destroyOnHitEnemy){
				Destroy(gameObject);
			}
		}
	}

}