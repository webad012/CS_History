using UnityEngine;
using System.Collections;

public class LevelsController : MonoBehaviour 
{
    public GameObject[] levels;
    public float outLeft;
    public float outRight;

    private int currentLevel = 0;
    private int[] unlockedLevels;

	// Use this for initialization
	void Start () 
    {
        unlockedLevels = PlayerPrefsX.GetIntArray("UnlockedLevels", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ButtonLeft()
    {
        if (currentLevel > 0)
        {
            TweenPosition.Begin(levels[currentLevel], 0.5f, new Vector3(outRight, 0, 0));
            currentLevel--;
            TweenPosition.Begin(levels[currentLevel], 0.5f, new Vector3(0, 0, 0));
        }
    }

    void ButtonRight()
    {
        if (currentLevel != levels.Length - 1)
        {
            TweenPosition.Begin(levels[currentLevel], 0.5f, new Vector3(outLeft, 0, 0));
            currentLevel++;
            TweenPosition.Begin(levels[currentLevel], 0.5f, new Vector3(0, 0, 0));
        }
    }

    void LevelSelected()
    {
        PlayerPrefs.SetInt("LevelSelected", currentLevel);
        Application.LoadLevel("Story");
    }
}
