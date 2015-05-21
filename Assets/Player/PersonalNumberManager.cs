using UnityEngine;
using System.Collections;

public class PersonalNumberManager : MonoBehaviour {

    public int personalNumber;
    public float currentModifier;
    public float modifierSpeed;
    public bool ableToChange;

    public GameObject laser;

    void Start()
    {
        currentModifier = 0;
    }

    void OnTriggerStay(Collider intruder)
    {
        if (intruder.gameObject.tag == "PersonalNumber")
        {
            if (intruder.GetComponent<PersonalNumberManager>().ableToChange && ableToChange)
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
    }

    void OnTriggerEnter(Collider intruder)
    {
        if (intruder.gameObject.tag == "PersonalNumber")
        {
            GameObject myLaser = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            myLaser.GetComponent<NumberLaser>().target = gameObject;
            myLaser.GetComponent<NumberLaser>().main = intruder.gameObject;
            myLaser.GetComponent<LightningBolt>().target = gameObject.transform;
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
