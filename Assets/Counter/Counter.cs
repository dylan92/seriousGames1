using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Counter : Singleton<Counter> {

	public Text scoreText;
	public Text equationText;
    public float sharedNumber;
    public float goalNumber;
    public float lerpSpeed;
    public float lerpSnapRange;
    public int minGoalNumber;
    public int maxGoalNumber;
    public int score;
    public float goalGenerationTime;
    float displaySharedNumber;

    public GameObject numberDisplay;
    
    public static event Action<int> OnGoalReached;

	void Start () {
        displaySharedNumber = sharedNumber;
        GenerateGoalNumber();
	}

    public void AdjustSharedNumber(int receivedNumber, string receivedOperation)
    {
        if (receivedOperation == "add")
        {
            sharedNumber += receivedNumber;
        }
        else if (receivedOperation == "subtract")
        {
            sharedNumber -= receivedNumber;
        }
        else if (receivedOperation == "multiply")
        {
            sharedNumber *= receivedNumber;
        }
        else if (receivedOperation == "divide")
        {
            sharedNumber /= receivedNumber;
            sharedNumber = Mathf.RoundToInt(sharedNumber);
        }
        if (sharedNumber == goalNumber)
        {
            GameObject myNumberDisplay = Instantiate(numberDisplay, new Vector3(70, 40, 0), Quaternion.identity) as GameObject;
            myNumberDisplay.GetComponent<TextMesh>().text = goalNumber + " = " + goalNumber;
            myNumberDisplay.GetComponent<TextMesh>().fontSize = 100;
            displaySharedNumber = sharedNumber;
            score += 1;
            scoreText.text = "Score: " + score;
            Invoke("GenerateGoalNumber", goalGenerationTime);
			if (OnGoalReached != null) {
				OnGoalReached(score);
			}
        }
    }

    void GenerateGoalNumber()
    {
        goalNumber = UnityEngine.Random.Range(minGoalNumber, maxGoalNumber);
    }

    void Update()
    {
        /*if (lerpSnapRange <= Mathf.Abs(displaySharedNumber - sharedNumber))
        {
            Debug.Log("asdf");
            displaySharedNumber = Mathf.RoundToInt(Mathf.Lerp(displaySharedNumber, sharedNumber, Time.deltaTime * lerpSpeed));
        }
        else
        {
            Debug.Log("fdsa");
            displaySharedNumber = sharedNumber;
        }*/
        if (sharedNumber > displaySharedNumber)
        {
            displaySharedNumber += 1;
        }
        if (sharedNumber < displaySharedNumber)
        {
            displaySharedNumber -= 1;
        }
        //displaySharedNumber = sharedNumber;
        if (sharedNumber == goalNumber)
        {
            GameObject myNumberDisplay = Instantiate(numberDisplay, transform.position, Quaternion.identity) as GameObject;
            myNumberDisplay.GetComponent<TextMesh>().text = goalNumber + " = " + goalNumber;
            myNumberDisplay.GetComponent<TextMesh>().fontSize = 48;
            equationText.text = sharedNumber + " = " + goalNumber;            
        }
        else
        {
            equationText.text = displaySharedNumber + " != " + goalNumber;
        }
    }
}
