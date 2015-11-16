using UnityEngine;
using System.Collections;

public class CeilingCheck : MonoBehaviour {

	public PlayerMovementJumpVelocity playerMovementJumpVelocity;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == Tags.wall){
			playerMovementJumpVelocity.setTouchingCeiling(true);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == Tags.wall){
			if(!playerMovementJumpVelocity.getTouchingCeiling()){
				playerMovementJumpVelocity.setTouchingCeiling(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == Tags.wall){
			playerMovementJumpVelocity.setTouchingCeiling(false);
		}
	}
}
