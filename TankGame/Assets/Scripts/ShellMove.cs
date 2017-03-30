using UnityEngine;
using System.Collections;

public class ShellMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate (Vector3.forward*30*Time.deltaTime);
		Destroy (this.gameObject, 1f);
	}
}
