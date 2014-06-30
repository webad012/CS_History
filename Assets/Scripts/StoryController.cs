using UnityEngine;
using System.Collections;

public class StoryController : MonoBehaviour 
{
    public UILabel scoreLabel;
    public UILabel scoreRequiredLabel;
    public UILabel storyLabel;
    public GameObject background;
    public GameObject continueButton;
    public UILabel continueLabel;

    public GameObject gamePausedWindow;
    private bool gamePaused = false;
    private Vector3 gamePausedTopPos;

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

    private MiniGameData miniGameData;

	void Start () 
    {
        gamePausedTopPos = new Vector3(0, Screen.height, 0);
        gamePausedWindow.transform.localPosition = gamePausedTopPos;
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);
        miniGameData = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().towersData[levelSelected].miniGameData;

        continueButton.SetActive(false);
        score = 0;
        storyTexts = StaticTexts.Instance.storyTexts[levelSelected];
        //storyTexts = miniGameData.storyIntroTexts;
        storyIndex = 0;
        currentRequiredResult = 0;
        scoreLabel.text = "";
        scoreRequiredLabel.text = "";
        background.renderer.material.mainTexture = miniGameData.backgroundTexture;
        UpdateGui();
	}
	
	// Update is called once per frame
	void Update () 
    {   
        if (gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = false;
                TweenPosition.Begin(gamePausedWindow, 0.25f, gamePausedTopPos);
            }
            
        } else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PlayerPrefs.SetInt("lastUnlockedStory", levelSelected);
                PlayerPrefs.SetInt("TowerUnlocked" + levelSelected.ToString(), 1);
                GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().towersData [levelSelected].upgradeData.isUnlocked = true;

                Application.LoadLevel("TowerDefense");
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = true;
                TweenPosition.Begin(gamePausedWindow, 0.25f, Vector3.zero);
            }
        }
	}

    void UpdateGui()
    {
        if (minigameStarted)
        {
            if(minigameFinished)
            {
                PlayerPrefs.SetInt("lastUnlockedStory", levelSelected);
                PlayerPrefs.SetInt("TowerUnlocked" + levelSelected.ToString(), 1);
                GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().towersData[levelSelected].upgradeData.isUnlocked = true;

                continueButton.SetActive(true);
                continueLabel.text = "Click to continue";
            }

            string score_string;
            if(score > 0)
            {
                score_string = score.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                score_string = score.ToString();
            }

            scoreLabel.text = "Score: " + score_string;
            scoreRequiredLabel.text = "Score Required: " + scoreRequired.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        } 
        else
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

            //if(currentRequiredResult == storySpec[levelSelected].requiredResultRanges.Length)
            if(currentRequiredResult == miniGameData.requiredResultRanges.Length)
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
            minigameStarted = true;
            ResetMinigame();
        }

        UpdateGui();
    }

    public void ContinueButtonClicked()
    {
        Application.LoadLevel("TowerDefense");
    }

    void ResetMinigame()
    {
        if (PlayObject != null)
        {
            Destroy(PlayObject);
        }

        score = 0;
        //scoreRequired = (int)Random.Range(storySpec[levelSelected].requiredResultRanges[currentRequiredResult].x,
        //                                  storySpec[levelSelected].requiredResultRanges[currentRequiredResult].y);
        scoreRequired = (int)Random.Range(miniGameData.requiredResultRanges[currentRequiredResult].x,
                                          miniGameData.requiredResultRanges[currentRequiredResult].y);
        //PlayObject = (GameObject)Instantiate(storySpec[levelSelected].ObjectToPlayWith, 
        PlayObject = (GameObject)Instantiate(miniGameData.ObjectToPlayWith, 
                                             new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    public void ButtonMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
    
    public void ButtonRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
