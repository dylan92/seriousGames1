using UnityEngine;
using System.Collections;

public class NumberDisplay : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += new Vector3(0, 3 * Time.deltaTime, 0);
        GetComponent<TextMesh>().color = Color.Lerp(GetComponent<TextMesh>().color, Color.clear, Time.deltaTime);
    }
}
