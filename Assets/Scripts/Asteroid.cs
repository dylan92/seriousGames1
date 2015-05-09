using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public static int minForce = -10;
	public static int maxForce = 10;
	
	private Rigidbody r; 
	private float forceX;
	private float forceY;
	void Start() {
		r = this.GetComponent<Rigidbody>();
		forceX = Random.Range(minForce, maxForce);
		forceY = Random.Range(minForce, maxForce);
	}
	
	// Update is called once per frame
	void Update () {
		r.AddForce(new Vector3(forceX, forceY, 0));
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
	}
	
	void OnCollisionEnter(Collision c) {
		//if (c.collider.gameObject.layer == LayerMask.NameToLayer("Border")) {
			forceX = Random.Range(minForce, maxForce);
			forceY = Random.Range(minForce, maxForce);
		//}
	}
	
	public void Destroy() {
		Destroy(this.gameObject);
	}
}
