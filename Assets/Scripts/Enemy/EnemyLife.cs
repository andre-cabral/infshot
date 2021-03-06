﻿using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public SpritesTint damageTint;
	public float tintTime = 0.5f;
	float tintTimePassed = 0f;
	bool takingDamage = false;
	public int maxLife = 5;
	int life = 9999;

	void Awake(){
		life = maxLife;
	}

	void Update(){
		if(takingDamage){
			if(tintTimePassed < tintTime){
				tintTimePassed += Time.deltaTime;
			}else{
				tintTimePassed = 0f;
				takingDamage = false;
				damageTint.EndTint();
			}
		}
	}

	public void TakeDamage(int damage){
		life -= damage;
		if(life <= 0){
			DeathEffects();
		}else{
			takingDamage = true;
			tintTimePassed = 0f;
			damageTint.StartTint();
		}
	}

	void DeathEffects(){
		Destroy(gameObject);
	}
}
