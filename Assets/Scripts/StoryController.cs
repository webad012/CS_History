using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryController : MonoBehaviour 
{
    public UILabel scoreLabel;
    public UILabel scoreRequiredLabel;
    public UILabel storyLabel;
    public GameObject background;
    public GameObject continueButton;
    public UILabel continueLabel;
    public GameObject storyImageSprite;
    public UILabel mainMenuLabel;
    public UILabel restartLabel;

    public GameObject gamePausedWindow;
    private bool gamePaused = false;
    private Vector3 gamePausedTopPos;

    private int score = 0;
    private int scoreRequired = 0;
    private string currentStoryText;
    private List<string[]> storyTexts;
    private int storyIndex;
    private bool minigameStarted = false;
    private bool minigameFinished = false;
    private int levelSelected;
    private int currentRequiredResult;
    private GameObject PlayObject;

    private MiniGameData miniGameData;

    private int selectedLanguage;

	void Start () 
    {
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        mainMenuLabel.text = StaticTexts.Instance.language_MainMenu [selectedLanguage];
        restartLabel.text = StaticTexts.Instance.language_Restart [selectedLanguage];

        gamePausedTopPos = new Vector3(0, Screen.height, 0);
        gamePausedWindow.transform.localPosition = gamePausedTopPos;
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);
        miniGameData = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().towersData[levelSelected].miniGameData;

        continueButton.SetActive(false);
        score = 0;
        storyTexts = StaticTexts.Instance.storyTexts[levelSelected];
        storyIndex = 0;
        currentRequiredResult = 0;
        scoreLabel.text = "";
        scoreRequiredLabel.text = "";
        background.renderer.material.mainTexture = miniGameData.backgroundTexture;
        storyImageSprite.SetActive(false);
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
            
        } 
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StoryLabelClicked();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                PlayerPrefs.SetInt("lastUnlockedStory", levelSelected);
                PlayerPrefs.SetInt("TowerUnlocked" + levelSelected.ToString(), 1);
                GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().towersData [levelSelected].upgradeData.isUnlocked = true;

                Application.LoadLevel("TowerDefense");
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
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

            scoreLabel.text = StaticTexts.Instance.language_Score[selectedLanguage] + score_string;
            scoreRequiredLabel.text = StaticTexts.Instance.language_ScoreRequired[selectedLanguage] + scoreRequired.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        } 
        else
        {
            storyLabel.text = (storyTexts [storyIndex])[selectedLanguage];
        }
    }

    public void AddToScore(float addition)
    {
        score += (int)addition;

        if (score == scoreRequired)
        {
            currentRequiredResult++;

            if(currentRequiredResult == miniGameData.requiredResultRanges.Length)
            {
                minigameFinished = true;
            }
            
            ResetMinigame();
        }

        UpdateGui();
    }

    public void StoryLabelClicked()
    {
        storyIndex++;
        if (storyIndex >= storyTexts.Count)
        {
            storyLabel.transform.parent.gameObject.SetActive(false);
            minigameStarted = true;
            ResetMinigame();
        }

        storyImageSprite.SetActive(false);
        for(int i=0; i<miniGameData.storyImages.Length; i++)
        {
            if(storyIndex == miniGameData.storyImages[i].position)
            {
                storyImageSprite.GetComponent<UISprite>().spriteName = miniGameData.storyImages[i].spriteName;
                storyImageSprite.SetActive(true);
                break;
            }
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

        if (!minigameFinished)
        {
            scoreRequired = (int)Random.Range(miniGameData.requiredResultRanges [currentRequiredResult].x,
                                          miniGameData.requiredResultRanges [currentRequiredResult].y);
        }
        PlayObject = (GameObject)Instantiate(miniGameData.ObjectToPlayWith, miniGameData.objectPosition, Quaternion.identity);
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
