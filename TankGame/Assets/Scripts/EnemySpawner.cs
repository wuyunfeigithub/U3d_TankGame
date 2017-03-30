using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyTank;
	private float coolTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		coolTime += Time.deltaTime;
		if (coolTime >= 5f) {
			if(GameObject.FindGameObjectsWithTag("EnemyTank").Length <= 10)
				Instantiate (this.enemyTank, getPoint(), this.transform.rotation);
			coolTime = 0;
		}
	}

	Vector3 getPoint(){
		float x = Random.Range (-40.0f, 40.0f);
		float z = Random.Range (-40.0f, 40.0f);
		return new Vector3 (x, this.transform.position.y, z);
	}
}
