using UnityEngine;
using System.Collections;

public class UpgradeController : MonoBehaviour 
{
    public float optionTransitionSpeed = 0.25f;
    public float outLeftX;
    public float outRightX;

    private int currentTower;
    private Vector3 outLeftVector;
    private Vector3 outRightVector;

    private GameObject[] towers;

	// Use this for initialization
	void Start () 
    {
        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void ButtonLeft()
    {
        if (currentTower > 0)
        {
            TweenPosition.Begin(towers[currentTower], optionTransitionSpeed, outRightVector);
            currentTower--;
            TweenPosition.Begin(towers[currentTower], optionTransitionSpeed, new Vector3(0, 0, 0));
        }
    }
    
    void ButtonRight()
    {
        if (currentTower != towers.Length - 1)
        {
            TweenPosition.Begin(towers[currentTower], optionTransitionSpeed, outLeftVector);
            currentTower++;
            TweenPosition.Begin(towers[currentTower], optionTransitionSpeed, new Vector3(0, 0, 0));
        }
    }

    void ButtonBack()
    {
        Application.LoadLevel("MainMenu");
    }
}
