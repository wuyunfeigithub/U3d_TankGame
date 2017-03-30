using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	//public Slider slider;
	public Transform Shell;

	private Transform Player;
	private Transform ShellPownerPoint;
	 
	private float speed = 10.0f;
	private float rotSpeed = 200.0f;

	private float coolTimer = 0f;
	private float fireCoolTimer = 0f;

	void Start(){
		Player = GameObject.Find ("PlayerTank").transform;
		//slider = GameObject.Find ("EnemyCanvas/Slider").GetComponent<Slider> ();
		ShellPownerPoint = transform.GetChild (0).GetChild (0).transform;
	}

	void Update(){

		move ();
		coolTimer += Time.deltaTime;
		if (Vector3.Distance (transform.position, Player.position) < 15.0f) {
			FireToPlayerTank();
		} else if (coolTimer > Random.Range (1.0f, 3.0f)) {
			coolTimer = 0;
			FindPointToWay ();
		}
	}

	void FindPointToWay(){
		float x = Random.Range (-40.0f, 40.0f);
		float z = Random.Range (-40.0f, 40.0f);

		Quaternion rot = Quaternion.LookRotation (new Vector3(x, 0, z) - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, rotSpeed * Time.deltaTime);

	}

	void move(){
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	void FireToPlayerTank(){
		Quaternion rot = Quaternion.LookRotation (Player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, rotSpeed * Time.deltaTime);

		if (Vector3.Distance (transform.position, Player.position) <= 10.0f) {
			fireCoolTimer += Time.deltaTime;
			if (fireCoolTimer >= 1.0f) {
				fireCoolTimer = 0;
				Instantiate (Shell, ShellPownerPoint.position, rot);
			}
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "PlayerShell") {
			Slider slider = this.gameObject.GetComponentInChildren<Slider> ();

			slider.value -= 0.1f;
			if (slider.value <= 0f)
				Destroy (this.gameObject);
		}

	}
}
