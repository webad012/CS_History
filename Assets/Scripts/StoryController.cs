﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class StorySpecificObjects
{
    public GameObject ObjectToPlayWith;
    public Vector2[] requiredResultRanges;
}

public class StoryController : MonoBehaviour 
{
    public UILabel scoreLabel;
    public UILabel scoreRequiredLabel;
    public UILabel storyLabel;
    public StorySpecificObjects[] storySpec;

    private int score = 0;
    private int scoreRequired = 0;
    private string currentStoryText;
    private string[] storyTexts;
    private int storyIndex;
    private bool minigameStarted = false;
    private bool minigameFinished = false;
    private int levelSelected;
    private int currentRequiredResult;
    private GameObject PlayObject;

	void Start () 
    {
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);
        storyTexts = StaticTexts.Instance.storyTexts[levelSelected];
        storyIndex = 0;
        currentRequiredResult = 0;
        scoreLabel.text = "";
        scoreRequiredLabel.text = "";
        UpdateGui();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void UpdateGui()
    {
        if (minigameStarted)
        {
            if(minigameFinished)
            {
                Application.LoadLevel("MainGame");
            }

            scoreLabel.text = "Score: " + score.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
            scoreRequiredLabel.text = "Score Required: " + scoreRequired.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        } else
        {
            storyLabel.text = storyTexts [storyIndex];
        }
    }

    public void AddToScore(float addition)
    {
        score += (int)addition;

        if (score == scoreRequired)
        {
            currentRequiredResult++;

            if(currentRequiredResult == storySpec[levelSelected].requiredResultRanges.Length)
            {
                minigameFinished = true;
            }
            else
            {
                ResetMinigame();
            }
        }

        UpdateGui();
    }

    public void StoryLabelClicked(GameObject go)
    {
        storyIndex++;
        if (storyIndex >= storyTexts.Length)
        {
            go.SetActive(false);
            //Instantiate(storySpec[levelSelected].ObjectToPlayWith, new Vector3(0f, 0f, 0f), Quaternion.identity);
            minigameStarted = true;
            ResetMinigame();
        }

        UpdateGui();
    }

    void ResetMinigame()
    {
        if (PlayObject != null)
        {
            Destroy(PlayObject);
        }

        score = 0;
        scoreRequired = (int)Random.Range(storySpec[levelSelected].requiredResultRanges[currentRequiredResult].x,
                                          storySpec[levelSelected].requiredResultRanges[currentRequiredResult].y);
        PlayObject = (GameObject)Instantiate(storySpec[levelSelected].ObjectToPlayWith, 
                                             new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
}
