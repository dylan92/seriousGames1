using UnityEngine;
using System.Collections;

public class PersonalNumberManager : MonoBehaviour {

    public int personalNumber;
    public float currentModifier;
    public float modifierSpeed;

    void Start()
    {
        currentModifier = 0;
    }

    void OnTriggerStay(Collider intruder)
    {
        if (intruder.gameObject.tag == "PersonalNumber")
        {
            if (intruder.GetComponent<PersonalNumberManager>().personalNumber > personalNumber)
            {
                currentModifier += Time.deltaTime * modifierSpeed;
            }
            if (intruder.GetComponent<PersonalNumberManager>().personalNumber < personalNumber)
            {
                currentModifier -= Time.deltaTime * modifierSpeed;
            }
        }
    }

    void Update()
    {
        if (currentModifier > 1)
        {
            personalNumber += 1;
            currentModifier = 0;
        }
        if (currentModifier < -1)
        {
            personalNumber -= 1;
            currentModifier = 0;
        }
        GetComponent<TextMesh>().text = personalNumber.ToString();
    }
}
