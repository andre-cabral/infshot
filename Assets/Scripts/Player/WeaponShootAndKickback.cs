using UnityEngine;
using System.Collections;

public class WeaponShootAndKickback : MonoBehaviour {

	public float minimumRotation = 270f;
	public float maximumRotation = 359.9f;
	public float kickBackPerShoot = 10f;
	public float velocityToReturn = 80f;
	public float timeToStartReturn = 0.2f;
	float timeToStartReturnPassed = 0f;
	bool returning = true;
	public GameObject bullet;
	public Transform bulletPositionObject;

	void Update () {
		if(Input.GetButton("Fire1")){
			ShootBullet();
		}

		if(Input.GetButtonDown("Fire2")){
			ShootBullet();
		}

		if(!returning){
			if(timeToStartReturnPassed < timeToStartReturn){
				timeToStartReturnPassed += Time.deltaTime;
			}else{
				timeToStartReturnPassed = 0f;
				returning = true;
			}
		}

		if(returning && transform.rotation.eulerAngles.z > minimumRotation){
			WeaponDown();
		}
	}

	void WeaponDown(){
		if(transform.rotation.eulerAngles.z - Time.deltaTime*velocityToReturn > minimumRotation){
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - Time.deltaTime*velocityToReturn);
		}else{
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, minimumRotation);
		}
	}

	public void ShootBullet(){
		returning = false;
		timeToStartReturnPassed = 0f;
		Instantiate(bullet, bulletPositionObject.position, bulletPositionObject.rotation);
		if(transform.rotation.eulerAngles.z + kickBackPerShoot <= maximumRotation){
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + kickBackPerShoot);
		}else{
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, maximumRotation);
		}
	}
}
