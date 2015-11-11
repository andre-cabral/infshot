using UnityEngine;
using System.Collections;


public class PointsMovement : MonoBehaviour {

	public Transform[] waypoints;
	Vector2[] points;
	public float velocity = 3f;
	private Vector2 startPosition;
	private int vectorIndex = 0;
	/*private bool isFacingRight = true;*/
	/*EnemyCollisions enemyCollisions;*/

	// Use this for initialization
	void Awake () {
		startPosition = transform.localPosition;
		points = new Vector2[waypoints.Length];
		for(int i=0; i < waypoints.Length; i++){
			points[i] = waypoints[i].position;
		}
		/*enemyCollisions = gameObject.GetComponent<EnemyCollisions>();*/
	}
	

	void Update () {
		/*if(!enemyCollisions.getTakingDamage()){*/
			if(vectorIndex < points.Length){
				Movement(points[vectorIndex], velocity*Time.deltaTime);
			}else{
				Movement(startPosition, velocity*Time.deltaTime);
			}
		/*}*/
	}
	

	void Movement(Vector2 end, float velocity){
		/*
		if(transform.localPosition.x < end.x && !isFacingRight){
			Flip();
		}else{
			if(transform.localPosition.x > end.x && isFacingRight){
				Flip();
			}
		}
		*/

		transform.localPosition = Vector2.MoveTowards(transform.localPosition, end, velocity);
		if(transform.localPosition.x == end.x && transform.localPosition.y == end.y){
			if(vectorIndex < points.Length){
				vectorIndex++;
			}else{
				vectorIndex = 0;
			}
		}
	}
	/*
	void Flip(){
		isFacingRight = !isFacingRight;
		
		Vector3 theScale = transform.localScale;
		
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	*/
}
