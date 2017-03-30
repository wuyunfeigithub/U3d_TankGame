using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankController : MonoBehaviour {

	private Slider slider;
	//public GameObject Shell;
	public Transform Shell;
	private float fireCoolTime = 0f;

	private Transform Turret;
	private Transform ShellSpawnerPoint;

	private float curSpeed, targetSpeed, rotSpeed;

	private float turretRotSpeed = 10.0f;
	private float maxForwardSpeed = 15.0f;
	private float maxBackwardSpeed = -15.0f;

	void Start()
	{
		slider = GameObject.Find ("HeroCanvas/Slider").GetComponent<Slider> ();
		//slider = gameObject.GetComponent<Slider>;
		rotSpeed = 200.0f;

		Turret = gameObject.transform.GetChild (0).transform;
		ShellSpawnerPoint = Turret.GetChild (0).transform;
		//Shell = (GameObject)Resources.Load ("Shell");
	}

	void Update()
	{
		this.UpdateControl ();
		this.fire ();
	}	
		

	void UpdateControl()
	{
		Plane playerPlane = new Plane (Vector3.up, transform.position + new Vector3(0,0,0));
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		float hitDist = 0;
		if (playerPlane.Raycast (ray, out hitDist)) 
		{
			Vector3 rayHitPoint = ray.GetPoint (hitDist);
			Quaternion targetPoint = Quaternion.LookRotation (rayHitPoint - transform.position);
			Turret.transform.rotation = Quaternion.Slerp (Turret.rotation, targetPoint, Time.deltaTime*turretRotSpeed);
		}

		if (Input.GetKey (KeyCode.W)) {
			targetSpeed = maxForwardSpeed;
		} else if (Input.GetKey (KeyCode.S)) {
			targetSpeed = maxBackwardSpeed;
		} else {
			targetSpeed = 0;
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0, -rotSpeed * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0, rotSpeed * Time.deltaTime, 0);
		}

		curSpeed = Mathf.Lerp (curSpeed, targetSpeed, 7.0f*Time.deltaTime);
		transform.Translate (Vector3.forward*Time.deltaTime*curSpeed);

	}

	void fire()
	{
		fireCoolTime += Time.deltaTime;
		if(Input.GetMouseButton(0)){
			if (fireCoolTime > 1.0f) {
				Instantiate (Shell, ShellSpawnerPoint.position, ShellSpawnerPoint.rotation);
				fireCoolTime = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "EnemyShell") {
			
			slider.value -= 0.1f;
			if (slider.value <= 0f)
				slider.value = 1;
		}

	}
}
