using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour {

	public SpritesTint damageTint;
	public float tintTime = 0.5f;
	float tintTimePassed = 0f;
	bool takingDamage = false;
	public int playerMaxLife = 10;
	int playerLife = 10;

	void Awake(){
		playerLife = playerMaxLife;
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
		playerLife -= damage;
		if(playerLife <= 0){
			DeathEffects();
		}else{
			takingDamage = true;
			tintTimePassed = 0f;
			damageTint.StartTint();
		}
	}

	public void DeathEffects(){

	}

}
