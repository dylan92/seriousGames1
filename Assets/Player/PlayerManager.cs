using UnityEngine;
using System.Collections;
using HappyFunTimesExample;
using ParticlePlayground;

public class PlayerManager : MonoBehaviour {

    public int personalNumber;
    public int minPersonalNumber;
    public int maxPersonalNumber;
    public float respawnTime;
    public float invincibilityTime;
    public string operation;
    public GameObject deathParticle;
    public ExampleSimplePlayer player;
    public PlaygroundParticlesC particles;
    bool ableToTakeDamage;
    public GameObject numberDisplay;

    Counter counter;
    TextMesh personalNumberDisplay;

    void Start()
    {
        counter = GameObject.Find("Counter").GetComponent<Counter>();
        personalNumberDisplay = transform.Find("PersonalNumberDisplay").GetComponent<TextMesh>();
        Respawn();
        player.OnSetColor += HandleOnSetColor;
    }

    void HandleOnSetColor (Color c)
    {
		GradientColorKey[] colorKeys = new GradientColorKey[particles.lifetimeColor.colorKeys.Length];
		for (int i = 0; i < particles.lifetimeColor.colorKeys.Length; i++) {
    		colorKeys[i].color = c;
    	}
		particles.lifetimeColor.colorKeys = colorKeys;
    }

    void Respawn()
    {
        ableToTakeDamage = false;
        personalNumberDisplay.GetComponent<PersonalNumberManager>().ableToChange = true;
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

    void OnTriggerStay(Collider intruder)
    {
        if (intruder.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
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
        GameObject myNumberDisplay = Instantiate(numberDisplay, this.transform.position, Quaternion.identity) as GameObject;
        if (operation == "add")
        {
            myNumberDisplay.GetComponent<TextMesh>().text = "+" + personalNumberDisplay.GetComponent<PersonalNumberManager>().personalNumber;
        }
        if (operation == "subtract")
        {
            myNumberDisplay.GetComponent<TextMesh>().text = "-" + personalNumberDisplay.GetComponent<PersonalNumberManager>().personalNumber;
        }
        if (operation == "multiply")
        {
            myNumberDisplay.GetComponent<TextMesh>().text = "x" + personalNumberDisplay.GetComponent<PersonalNumberManager>().personalNumber;
        }
        if (operation == "divide")
        {
            myNumberDisplay.GetComponent<TextMesh>().text = "÷" + personalNumberDisplay.GetComponent<PersonalNumberManager>().personalNumber;
        }
        counter.AdjustSharedNumber(personalNumberDisplay.GetComponent<PersonalNumberManager>().personalNumber, operation);
        ableToTakeDamage = false;
        personalNumberDisplay.GetComponent<PersonalNumberManager>().ableToChange = false;
        foreach (Renderer eachRenderer in GetComponentsInChildren<Renderer>())
        {
            eachRenderer.enabled = false;
        }
        Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        Invoke("Respawn", respawnTime);
    }

    void GeneratePersonalNumber()
    {
        float coin = Random.Range(-10, 10);
        if (Mathf.Sign(coin) == 1)
        {
            personalNumber = Random.Range(7, 9);
        }
        if (Mathf.Sign(coin) == -1)
        {
            personalNumber = Random.Range(1, 3);
        }
        personalNumberDisplay.text = personalNumber.ToString();
        personalNumberDisplay.GetComponent<PersonalNumberManager>().personalNumber = personalNumber;
    }
}
