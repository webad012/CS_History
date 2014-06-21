using UnityEngine;
using System.Collections;

public class TowerDefenseController : MonoBehaviour 
{
    public bool gameLost = false;
    private bool gameWon = false;
    private bool gamePaused = false;

    public GameObject gameLostWindow;
    private bool lostWindowDisplayed = false;
    public GameObject gameWonWindow;
    private bool wonWindowDisplayed = false;
    public GameObject gamePausedWindow;
    //private bool pausedWindowDisplayed = false;

    //public GameObject buildPanel;
    //public GameObject buildButtonPrefab;
    //public int buildPosX;

    public UILabel knowledgeLabel;
    public UILabel coinsLabel;

    private GameDataController gameDataControllerScript;
    private float coinsCooldown;
    private float knowledgeCooldown;
    private int levelSelected;
    private int knowledgeGained = 0;

	// Use this for initialization
	void Start () 
    {
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();
        
        PlayerPrefs.SetInt("PlayerCoins", gameDataControllerScript.levels [levelSelected].startingCoins);
        coinsCooldown = gameDataControllerScript.levels [levelSelected].house.coinsTimeout;
        knowledgeCooldown = gameDataControllerScript.levels [levelSelected].house.knowledgeTimeout;
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
            }
        }
        else if(gameWon)
        {
            if(!wonWindowDisplayed)
            {
                Time.timeScale = 0;
                wonWindowDisplayed = true;
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
                    TweenPosition.Begin(gamePausedWindow, 0.25f, new Vector3(0, 500, 0));
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
                } else
                {
                    coinsCooldown = gameDataControllerScript.levels [levelSelected].house.coinsTimeout;
                    coins_int += gameDataControllerScript.levels [levelSelected].house.coinsGain;
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
                //int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
                //int knowledge_int = knowledgeGained;
                if (gameObject.GetComponent<WaveManager>().waveStarted)
                {
                    if (knowledgeCooldown > 0)
                    {
                        knowledgeCooldown -= Time.deltaTime;
                    } else
                    {
                        knowledgeCooldown = gameDataControllerScript.levels [levelSelected].house.knowledgeTimeout;
                        //knowledge_int += gameDataControllerScript.levels [levelSelected].house.knowledgeGain;
                        knowledgeGained += gameDataControllerScript.levels [levelSelected].house.knowledgeGain;
                        //PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);
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

                    gameWon = true;
                }
            }
        }
	}

    public void ButtonMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void ButtonRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ButtonNextLevel()
    {
        int next_level = PlayerPrefs.GetInt("LevelSelected", 0) + 1;

        PlayerPrefs.SetInt("LevelUnlocked" + next_level.ToString(), next_level);
        PlayerPrefs.SetInt("LevelSelected", next_level);

        if (PlayerPrefs.GetInt("LevelSelected", 0)+1 <= PlayerPrefs.GetInt("lastUnlockedStory", -1))
        {
            Application.LoadLevel("TowerDefense");
        } 
        else
        {
            Application.LoadLevel("MiniGame");
        }
    }
}
