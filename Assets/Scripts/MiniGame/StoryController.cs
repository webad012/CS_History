using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryController : MonoBehaviour 
{
    public UILabel scoreLabel;
    public UILabel scoreRequiredLabel;
    public UILabel completedLabel;
    public UILabel storyLabel;
    public GameObject background;
    public GameObject storyImageSprite;

    public GameObject gamePausedWindow;
    private bool gamePaused = false;
    private Vector3 gamePausedTopPos;
    public GameObject greatWindow;
    private bool scoreAchieved = false;

    public AudioClip congratulationsSound;

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

    private GameDataController gameDataControllerScript;
    private MiniGameData miniGameData;

    private int selectedLanguage;

	void Start () 
    {
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        gamePausedWindow.transform.Find("Button_MainMenu").Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_MainMenu [selectedLanguage];
        gamePausedWindow.transform.Find("Button_Restart").Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Restart [selectedLanguage];
        gamePausedWindow.transform.Find("Label_Title").GetComponent<UILabel>().text = StaticTexts.Instance.language_GamePaused [selectedLanguage];

        greatWindow.transform.Find("Button_Continue").Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Continue [selectedLanguage];
        greatWindow.transform.Find("Label_Title").GetComponent<UILabel>().text = StaticTexts.Instance.language_Great [selectedLanguage];

        gamePausedTopPos = new Vector3(0, Screen.height, 0);
        gamePausedWindow.transform.localPosition = gamePausedTopPos;
        greatWindow.transform.localPosition = gamePausedTopPos;
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);

        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();
        miniGameData = gameDataControllerScript.towersData[levelSelected].miniGameData;

        //continueButton.SetActive(false);
        score = 0;
        storyTexts = StaticTexts.Instance.storyTexts[levelSelected];
        storyIndex = 0;
        currentRequiredResult = 0;
        scoreLabel.text = "";
        scoreRequiredLabel.text = "";
        completedLabel.text = "";
        background.renderer.material.mainTexture = miniGameData.backgroundTexture;
        storyImageSprite.SetActive(false);
        UpdateGui();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gamePaused && !scoreAchieved)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = false;
                TweenPosition.Begin(gamePausedWindow, 0.25f, gamePausedTopPos);
            }
        }
        else if (!scoreAchieved)
        {
            if (!minigameStarted)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    PlayerPrefs.SetInt("lastUnlockedStory", levelSelected);
                    PlayerPrefs.SetInt("TowerUnlocked" + levelSelected.ToString(), 1);
                    GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().towersData [levelSelected].upgradeData.isUnlocked = true;

                    Application.LoadLevel("TowerDefense");
                }
                else if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                {
                    StoryLabelClicked();
                }
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
            completedLabel.text = StaticTexts.Instance.language_Completed[selectedLanguage] + currentRequiredResult.ToString() + "/" + miniGameData.requiredResultRanges.Length.ToString();
        } 
        else
        {
            storyLabel.text = (storyTexts [storyIndex])[selectedLanguage];
        }
    }

    public void AddToScore(float addition)
    {
        if (!gamePaused && !scoreAchieved)
        {
            score += (int)addition;

            if (score == scoreRequired)
            {
                currentRequiredResult++;

                if (currentRequiredResult == miniGameData.requiredResultRanges.Length)
                {
                    minigameFinished = true;
                }

                gameDataControllerScript.PlayAudioClip(congratulationsSound);
                gamePaused = true;
                scoreAchieved = true;
                TweenPosition.Begin(greatWindow, 0.25f, Vector3.zero);
            }

            UpdateGui();
        }
    }

    public void StoryLabelClicked()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
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

    void ResetMinigame()
    {
        gamePaused = false;
        scoreAchieved = false;

        if (PlayObject)
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
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("MainMenu");
    }
    
    public void ButtonRestart()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel(Application.loadedLevel);
    }

    void ButtonContinue()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        if (minigameFinished)
        {
            Application.LoadLevel("TowerDefense");
        }
        else
        {
            TweenPosition.Begin(greatWindow, 0.25f, gamePausedTopPos);
            ResetMinigame();
            UpdateGui();
        }
    }
}
