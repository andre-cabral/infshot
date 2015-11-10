using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 1;
	public float velocity = 10f;
	public bool destroyOnHitEnemy = true;
	bool startProjectile = false;

	void Update () {
		/*if(startProjectile){*/
			transform.Translate(Vector2.right * velocity * Time.deltaTime);
		/*}*/
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag(Tags.enemy)){
			collider.gameObject.GetComponent<EnemyLife>().takeDamage(damage);
			if(destroyOnHitEnemy){
				Destroy(gameObject);
			}
		}
	}

	/*
	public void setStartProjectile(bool startProjectile){
		this.startProjectile = startProjectile;
	}
	*/
}