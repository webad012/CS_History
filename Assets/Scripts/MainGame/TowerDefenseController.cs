using UnityEngine;
using System.Collections;


public class TowerDefenseController : MonoBehaviour 
{
    public GameObject[] groundPlanes;

    public bool gameLost = false;
    private bool gameWon = false;
    private bool gamePaused = false;

    public GameObject gameLostWindow;
    private bool lostWindowDisplayed = false;
    public GameObject gameWonWindow;
    private bool wonWindowDisplayed = false;
    public GameObject gamePausedWindow;
    private Vector3 windowsTopPos;

    public UILabel knowledgeLabel;
    public UILabel knowledgeRequiredLabel;
    public UILabel requiredLabel;
    public UILabel coinsLabel;

    public GameObject sprinkleStartingObject;
    public GameObject sprinkleParentAnchor;
    
    // sprinkle coin
    public GameObject coinDestination;
    public GameObject coinIconPrefab;
    public AudioClip coinSound;

    // sprinkle knowledge
    public GameObject knowledgeDestination;
    public GameObject knowledgeIconPrefab;
    public AudioClip knowledgeSound;

    public AudioClip victorySound;
    public AudioClip gameOverSound;
    
    private GameDataController gameDataControllerScript;
    private float coinsCooldown;
    private float knowledgeCooldown;
    private int levelSelected;
    private int knowledgeGained = 0;

    private Vector3 coinsRealDestination;
    private Vector3 knowledgeRealDestination;

    private int selectedLanguage;
    private float soundsVolume;

    public UISprite timeForwardSprite;
    private Color timeForwardOnColor = new Color32 (220, 135, 30, 255);
    private Color timeForwardOffColor = new Color32 (150, 150, 150, 150);
    private bool timeForwardSelected = false;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();
        gameDataControllerScript.PlayBackgroundMusic(gameDataControllerScript.sounds.backgroundTowerDefense);

        soundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        requiredLabel.text = StaticTexts.Instance.language_Required[selectedLanguage];
        gameLostWindow.transform.Find("Button_MainMenu").transform.Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_MainMenu[selectedLanguage];
        gameLostWindow.transform.Find("Button_Restart").transform.Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Restart[selectedLanguage];
        gameLostWindow.transform.Find("Label_Title").GetComponent<UILabel>().text = StaticTexts.Instance.language_GameLost[selectedLanguage];
        gamePausedWindow.transform.Find("Button_MainMenu").transform.Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_MainMenu[selectedLanguage];
        gamePausedWindow.transform.Find("Button_Restart").transform.Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Restart[selectedLanguage];
        gamePausedWindow.transform.Find("Label_Title").GetComponent<UILabel>().text = StaticTexts.Instance.language_GamePaused[selectedLanguage];
        gameWonWindow.transform.Find("Button_Back").transform.Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Back[selectedLanguage];
        gameWonWindow.transform.Find("Label_Title").GetComponent<UILabel>().text = StaticTexts.Instance.language_GameWon[selectedLanguage];

        windowsTopPos = new Vector3(0, Screen.height, 0);
        gameLostWindow.transform.localPosition = windowsTopPos;
        gameWonWindow.transform.localPosition = windowsTopPos;
        gamePausedWindow.transform.localPosition = windowsTopPos;

        coinsRealDestination = new Vector3(-Camera.main.WorldToScreenPoint(coinDestination.transform.position).x,
                                              Camera.main.WorldToScreenPoint(coinDestination.transform.parent.position).y,
                                              0f);
        
        
        knowledgeRealDestination = new Vector3(-Camera.main.WorldToScreenPoint(knowledgeDestination.transform.position).x,
                                             Camera.main.WorldToScreenPoint(knowledgeDestination.transform.parent.position).y,
                                             0f);
        Time.timeScale = 1;
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);

        for (int i=0; i<groundPlanes.Length; i++)
        {
            groundPlanes[i].renderer.material.mainTexture = gameDataControllerScript.levels [levelSelected].groundTexture;
        }

        knowledgeRequiredLabel.text = gameDataControllerScript.levels [levelSelected].knowledgeRequired.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        
        PlayerPrefs.SetInt("PlayerCoins", gameDataControllerScript.levels [levelSelected].startingCoins);
        coinsCooldown = gameDataControllerScript.levels [levelSelected].house.coinsTimeout;
        knowledgeCooldown = gameDataControllerScript.levels [levelSelected].house.knowledgeTimeout;

        Camera.main.backgroundColor = gameDataControllerScript.levels [levelSelected].cameraBackground;

        timeForwardSprite.color = timeForwardOffColor;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (gameLost)
        {
            if(!lostWindowDisplayed)
            {
                int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
                knowledge_int += knowledgeGained;
                PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);

                Time.timeScale = 0;
                lostWindowDisplayed = true;

                TweenPosition.Begin(gameLostWindow, 0.25f, Vector3.zero);
                gameDataControllerScript.PlayAudioClip(gameOverSound);
                //AudioSource.PlayClipAtPoint(gameOverSound, Vector3.zero, PlayerPrefs.GetFloat("SoundsVolume", 1));
            }
        }
        else if(gameWon)
        {
            if(!wonWindowDisplayed)
            {
                Time.timeScale = 0;
                wonWindowDisplayed = true;

                gameDataControllerScript.PlayAudioClip(victorySound);
                //AudioSource.PlayClipAtPoint(victorySound, Vector3.zero, PlayerPrefs.GetFloat("SoundsVolume", 1));
                TweenPosition.Begin(gameWonWindow, 0.25f, Vector3.zero);
            }
        }
        else
        {
            if(gamePaused)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 1;
                    gamePaused = false;
                    TweenPosition.Begin(gamePausedWindow, 0.25f, windowsTopPos);
                }

            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 0;
                    gamePaused = true;
                    TweenPosition.Begin(gamePausedWindow, 0.25f, Vector3.zero);
                }

                /// coins part
                string coins_string;
                int coins_int = PlayerPrefs.GetInt("PlayerCoins", 0);

                if (coinsCooldown > 0)
                {
                    coinsCooldown -= Time.deltaTime;
                } 
                else
                {
                    coinsCooldown = gameDataControllerScript.levels [levelSelected].house.coinsTimeout;
                    coins_int += gameDataControllerScript.levels [levelSelected].house.coinsGain;
                    StartCoroutine(SprinkleCoins(gameDataControllerScript.levels [levelSelected].house.coinsGain, 
                                                 10,
                                                 sprinkleStartingObject.transform.position));
                }

                if (coins_int == 0)
                {
                    coins_string = coins_int.ToString();
                } else
                {
                    coins_string = coins_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                }
                PlayerPrefs.SetInt("PlayerCoins", coins_int);

                /// knowledge part
                string knowledge_string;
                if (gameObject.GetComponent<WaveManager>().waveStarted)
                {
                    if (knowledgeCooldown > 0)
                    {
                        knowledgeCooldown -= Time.deltaTime;
                    } else
                    {
                        knowledgeCooldown = gameDataControllerScript.levels [levelSelected].house.knowledgeTimeout;
                        knowledgeGained += gameDataControllerScript.levels [levelSelected].house.knowledgeGain;
                        StartCoroutine(SprinkleKnowledge(gameDataControllerScript.levels [levelSelected].house.knowledgeGain, 
                                                         10,
                                                         sprinkleStartingObject.transform.position));
                    }
                }
                if (knowledgeGained == 0)
                {
                    knowledge_string = knowledgeGained.ToString();
                } else
                {
                    knowledge_string = knowledgeGained.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                }

                coinsLabel.text = coins_string;
                knowledgeLabel.text = knowledge_string;

                if(knowledgeGained == gameDataControllerScript.levels [levelSelected].knowledgeRequired)
                {
                    int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
                    knowledge_int += knowledgeGained;
                    PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);

                    int next_level = PlayerPrefs.GetInt("LevelSelected", 0) + 1;
                    PlayerPrefs.SetInt("LevelUnlocked" + next_level.ToString(), 1);

                    gameWon = true;
                }
            }
        }

        if (timeForwardSelected)
        {
            timeForwardSprite.color = timeForwardOnColor;
        }
        else
        {
            timeForwardSprite.color = timeForwardOffColor;
        }
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

    public void ButtonBack()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("LevelSelector");
    }

    public void ButtonForwardTime()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);

        if (timeForwardSelected)
        {
            Time.timeScale = 1f;
            timeForwardSelected = false;
        }
        else
        {
            Time.timeScale = 1.5f;
            timeForwardSelected = true;
        }
    }

    public IEnumerator SprinkleCoins(int coins, int step, Vector3 sourceObjectPos)
    {
        Vector3 objectToScreen = Camera.main.WorldToScreenPoint(sourceObjectPos);
        Vector3 sourcePos = new Vector3((objectToScreen.x - (Camera.main.pixelWidth/2)), 
                                        (objectToScreen.y - (Camera.main.pixelHeight/2)), 0f);
        int count = coins / step;
        for (int i=0; i<count; i++)
        {
            GameObject coinObject = NGUITools.AddChild(sprinkleParentAnchor, coinIconPrefab);
            coinObject.transform.localPosition = sourcePos;
            TweenPosition.Begin(coinObject, 0.5f, coinsRealDestination);
            AudioSource.PlayClipAtPoint(coinSound, sourceObjectPos, soundsVolume);

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    public IEnumerator SprinkleKnowledge(int knowledge, int step, Vector3 sourceObjectPos)
    {
        Vector3 objectToScreen = Camera.main.WorldToScreenPoint(sourceObjectPos);
        Vector3 sourcePos = new Vector3((objectToScreen.x - (Camera.main.pixelWidth/2)), 
                                        (objectToScreen.y - (Camera.main.pixelHeight/2)), 0f);
        int count = knowledge / step;
        for (int i=0; i<count; i++)
        {
            GameObject knowledgeObject = NGUITools.AddChild(sprinkleParentAnchor, knowledgeIconPrefab);
            knowledgeObject.transform.localPosition = sourcePos;
            TweenPosition.Begin(knowledgeObject, 0.5f, knowledgeRealDestination);
            AudioSource.PlayClipAtPoint(knowledgeSound, sourceObjectPos, soundsVolume);
            
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return null;
    }
}
