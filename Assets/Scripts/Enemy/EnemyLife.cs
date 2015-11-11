﻿using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int maxLife = 5;
	int life = 9999;

	void Awake(){
		life = maxLife;
	}

	public void takeDamage(int damage){
		life -= damage;
		if(life <= 0){
			DeathEffects();
		}
	}

	void DeathEffects(){
		Destroy(gameObject);
	}
}
