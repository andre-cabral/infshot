using UnityEngine;
using System.Collections;

public class GroundedCheck : MonoBehaviour {

	public PlayerMovementJumpVelocity playerMovementJumpVelocity;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == Tags.platform || other.tag == Tags.destructibleObject || other.tag == Tags.scenarioObject || other.tag == Tags.wall){
			playerMovementJumpVelocity.setTouchingGround(true);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == Tags.platform || other.tag == Tags.destructibleObject || other.tag == Tags.scenarioObject || other.tag == Tags.wall){
			if(!playerMovementJumpVelocity.getTouchingGround()){
				playerMovementJumpVelocity.setTouchingGround(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == Tags.platform || other.tag == Tags.destructibleObject || other.tag == Tags.scenarioObject || other.tag == Tags.wall){
			playerMovementJumpVelocity.setTouchingGround(false);
		}
	}
}
