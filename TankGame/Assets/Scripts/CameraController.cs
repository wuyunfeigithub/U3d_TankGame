using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;

	private float startingDistance = 8f;
	private float maxDistance = 30.0f;
	private float minDistance = 3.0f;
	private float zoomSpeed = 20.0f;
	private float targetHeight = 2.0f;
	private float camRotationSpeed = 70.0f;
	private float rotationDaming = 3.0f;
	private float camXAngle = 15.0f;

	private float maxCamAngle = 90;
	private float minCamAngle = 0;

	private Transform myTransform;
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		myTransform.position = target.position;
	}

	void LateUpdate()
	{
		if (target == null)
			return;
		//la jin la yuan she xiang ji
		float mw = Input.GetAxis ("Mouse ScrollWheel");
		if (mw > 0) {
			startingDistance -= Time.deltaTime * zoomSpeed;
			if (startingDistance < minDistance)
				startingDistance = minDistance;
		} else if (mw < 0) {
			startingDistance += Time.deltaTime * zoomSpeed;
			if (startingDistance > maxDistance)
				startingDistance = maxDistance;
		}
		//she xiang tou fu shi xiaoguo
		if (Input.GetButton ("Fire3")) {
			float v = Input.GetAxis ("Mouse Y");
			if (v > 0) {
				camXAngle += camRotationSpeed * Time.deltaTime;
				if (camXAngle > maxCamAngle)
					camXAngle = maxCamAngle;
			}
			else if (v < 0) {
				camXAngle += -camRotationSpeed * Time.deltaTime;
				if (camXAngle < minCamAngle)
					camXAngle = minCamAngle;
			}
		}

		float wantedRotationAngle = target.eulerAngles.y;
		float currentRotationAngle = myTransform.eulerAngles.y;

		currentRotationAngle = Mathf.LerpAngle (wantedRotationAngle, currentRotationAngle, rotationDaming*Time.deltaTime);
		Quaternion curRotion = Quaternion.Euler(camXAngle, currentRotationAngle, 0);

		myTransform.position = target.position;
		myTransform.position -= curRotion * Vector3.forward * startingDistance + new Vector3 (0, -1 * targetHeight, 0);

		Vector3 targetLookAt = target.position;
		targetLookAt.y += targetHeight;
		myTransform.LookAt (targetLookAt);
	}
	// Update is called once per frame
	void Update () 
	{	

	}
}
