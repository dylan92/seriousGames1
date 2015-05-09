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
        }
        if (sharedNumber == goalNumber)
        {
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
        /*if (lerpSnapRange >= Mathf.Abs(displaySharedNumber - sharedNumber))
        {
            displaySharedNumber = Mathf.RoundToInt(Mathf.Lerp(displaySharedNumber, sharedNumber, Time.deltaTime * lerpSpeed));
        }
        else
        {
            displaySharedNumber = sharedNumber;
        }*/
        displaySharedNumber = sharedNumber;
        if (sharedNumber == goalNumber)
        {
            equationText.text = sharedNumber + " = " + goalNumber;            
        }
        else
        {
            equationText.text = displaySharedNumber + " != " + goalNumber;
        }
    }
}
