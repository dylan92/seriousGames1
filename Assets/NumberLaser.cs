using UnityEngine;
using System.Collections;

public class NumberLaser : MonoBehaviour {

    public GameObject main;
    public GameObject target;

    void Update()
    {
        transform.position = main.transform.position;
        if (Vector3.Distance(main.transform.position, target.transform.position) > 12.5f)
        {
            Destroy(gameObject);
        }
        if (target.GetComponent<Renderer>().enabled == false)
        {
            Destroy(gameObject);
        }
        if (main.GetComponent<Renderer>().enabled == false)
        {
            Destroy(gameObject);
        }
    }
}
