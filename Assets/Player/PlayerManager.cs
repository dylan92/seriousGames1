using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int personalNumber;
    public int minPersonalNumber;
    public int maxPersonalNumber;
    public float respawnTime;
    public float invincibilityTime;
    public string operation;
    bool ableToTakeDamage;

    Counter counter;
    TextMesh personalNumberDisplay;

    void Start()
    {
        counter = GameObject.Find("Counter").GetComponent<Counter>();
        personalNumberDisplay = transform.Find("PersonalNumberDisplay").GetComponent<TextMesh>();
        Respawn();
    }

    void Respawn()
    {
        ableToTakeDamage = false;
        GeneratePersonalNumber();
        foreach (Renderer eachRenderer in GetComponentsInChildren<Renderer>())
        {
            eachRenderer.enabled = true;
        }
        Invoke("EndInvincibility", invincibilityTime);
    }

    void EndInvincibility()
    {
        ableToTakeDamage = true;
    }

    void OnTriggerEnter(Collider intruder)
    {
        if (intruder.gameObject.tag == "Bullet")
        {
            if (ableToTakeDamage)
            {
                ProcessDeath();
            }
        }
        if (intruder.gameObject.tag == "Zone")
        {
            operation = intruder.gameObject.name;
        }
    }

    void ProcessDeath()
    {
        counter.AdjustSharedNumber(personalNumber, operation);
        ableToTakeDamage = false;
        foreach (Renderer eachRenderer in GetComponentsInChildren<Renderer>())
        {
            eachRenderer.enabled = false;
        }
        Invoke("Respawn", respawnTime);
    }

    void GeneratePersonalNumber()
    {
        personalNumber = Random.Range(minPersonalNumber, maxPersonalNumber);
        personalNumberDisplay.text = personalNumber.ToString();
    }
}
