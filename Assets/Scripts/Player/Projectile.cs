using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	int damage = 1;
	public float velocity = 10f;
	bool startProjectile = false;

	void Update () {
		/*if(startProjectile){*/
			transform.Translate(Vector2.up * velocity * Time.deltaTime);
		/*}*/
	}

	void OnTriggerEnter(Collider collider){
		/*
		if(startProjectile){
			if(collider.CompareTag(Tags.wall)){
				Destroy(gameObject);
			}
			if(collider.CompareTag(targetTag)){
				collider.gameObject.GetComponent<UnitStats>().takeDamage(unitUsing, unitStats.attack);
				Destroy(gameObject);
			}
		}
		*/
	}

	/*
	public void setStartProjectile(bool startProjectile){
		this.startProjectile = startProjectile;
	}
	*/
}