using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float scale;
    public float minScale;
    public float maxScale;
    public Vector3 direction;

	void Start () {
        speed = Random.Range(minSpeed, maxSpeed);
        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
        direction = Vector3.Normalize(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0));
        if (direction == Vector3.zero)
        {
            direction = new Vector3(1, 0, 0);
        }
        Destroy(gameObject, 10f);
	}

    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
    }	
}
