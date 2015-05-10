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
        counter.AdjustSharedNumber(personalNumber, operation);
        ableToTakeDamage = false;
        foreach (Renderer eachRenderer in GetComponentsInChildren<Renderer>())
        {
            eachRenderer.enabled = false;
        }
        Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        Invoke("Respawn", respawnTime);
    }

    void GeneratePersonalNumber()
    {
        personalNumber = Random.Range(minPersonalNumber, maxPersonalNumber);
        personalNumberDisplay.text = personalNumber.ToString();
    }
}
