using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

    public float sharedNumber;
    public float goalNumber;
    public float lerpSpeed;
    public float lerpSnapRange;
    public int minGoalNumber;
    public int maxGoalNumber;
    public int score;
    public float goalGenerationTime;
    float displaySharedNumber;

    TextMesh myTextMesh;
    TextMesh scoreTextMesh;

	void Start () {
        myTextMesh = GetComponent<TextMesh>();
        scoreTextMesh = transform.FindChild("Score").GetComponent<TextMesh>();
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
            sharedNumber *= (1 / receivedNumber);
        }
        if (sharedNumber == goalNumber)
        {
            displaySharedNumber = sharedNumber;
            score += 1;
            scoreTextMesh.text = "Score = " + score;
            Invoke("GenerateGoalNumber", goalGenerationTime);
        }
    }

    void GenerateGoalNumber()
    {
        goalNumber = Random.Range(minGoalNumber, maxGoalNumber);
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
            myTextMesh.text = sharedNumber + " = " + goalNumber;            
        }
        else
        {
            myTextMesh.text = displaySharedNumber + " /= " + goalNumber;
        }
    }
}
