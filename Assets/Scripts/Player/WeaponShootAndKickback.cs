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
	public bool automaticWeapon = false;
	public float automaticWeaponDelay = 0f;
	float automaticWeaponPassed = 0f;
	public GameObject bullet;
	public Transform bulletPositionObject;
	public GameObject[] shootSplashObjects;
	bool BButton = false;

	void Update () {
#if UNITY_EDITOR
		if(automaticWeapon){
			if(Input.GetButtonDown("Fire2")){
				ShootBullet();
				automaticWeaponPassed = 0f;
			}
			if(Input.GetButton("Fire2")){
				if(automaticWeaponPassed >= automaticWeaponDelay){
					ShootBullet();
					automaticWeaponPassed = 0f;
				}else{
					automaticWeaponPassed += Time.deltaTime;
				}
			}
		}else{
			if(Input.GetButtonDown("Fire2")){
				ShootBullet();
			}
		}
#endif

		if(automaticWeapon){
			if(BButton){
				if(automaticWeaponPassed >= automaticWeaponDelay){
					ShootBullet();
					automaticWeaponPassed = 0f;
				}else{
					automaticWeaponPassed += Time.deltaTime;
				}
			}
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

		EnableOneSplashObject();

		if(transform.rotation.eulerAngles.z + kickBackPerShoot <= maximumRotation){
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + kickBackPerShoot);
		}else{
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, maximumRotation);
		}
	}

	void DisableAllSplashObjects(){
		foreach(GameObject splash in shootSplashObjects){
			splash.SetActive(false);
		}
	}

	void EnableOneSplashObject(){
		if(shootSplashObjects.Length > 0){
			DisableAllSplashObjects();
			shootSplashObjects[Random.Range(0, shootSplashObjects.Length)].SetActive(true);
		}
	}

	public void StartBButton(){
		BButton = true;

		if(automaticWeapon){
			ShootBullet();
			automaticWeaponPassed = 0f;
		}else{
			ShootBullet();
		}
	}

	public void EndBButton(){
		BButton = false;
	}
}
