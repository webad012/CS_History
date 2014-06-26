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

    public UILabel knowledgeLabel;
    public UILabel coinsLabel;

    public Vector3 sprinkleStartingLocation;
    public GameObject sprinkleParentAnchor;
    
    // sprinkle coin
    public GameObject coinDestination;
    public GameObject coinIconPrefab;

    // sprinkle knowledge
    public GameObject knowledgeDestination;
    public GameObject knowledgeIconPrefab;
    
    private GameDataController gameDataControllerScript;
    private float coinsCooldown;
    private float knowledgeCooldown;
    private int levelSelected;
    private int knowledgeGained = 0;

	// Use this for initialization
	void Start () 
    {
        Time.timeScale = 1;
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        for (int i=0; i<groundPlanes.Length; i++)
        {
            groundPlanes[i].renderer.material.mainTexture = gameDataControllerScript.levels [levelSelected].groundTexture;
        }
        
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
                    StartCoroutine(SprinkleCoins(gameDataControllerScript.levels [levelSelected].house.coinsGain/10));
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
                        StartCoroutine(SprinkleKnowledge(gameDataControllerScript.levels [levelSelected].house.knowledgeGain/10));
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

    IEnumerator SprinkleCoins(int count)
    {
        for (int i=0; i<count; i++)
        {
            GameObject coinObject = NGUITools.AddChild(sprinkleParentAnchor, coinIconPrefab);
            coinObject.transform.localPosition = sprinkleStartingLocation;
            TweenPosition.Begin(coinObject, 0.5f, coinDestination.transform.localPosition);

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    IEnumerator SprinkleKnowledge(int count)
    {
        for (int i=0; i<count; i++)
        {
            GameObject knowledgeObject = NGUITools.AddChild(sprinkleParentAnchor, knowledgeIconPrefab);
            knowledgeObject.transform.localPosition = sprinkleStartingLocation;
            TweenPosition.Begin(knowledgeObject, 0.5f, knowledgeDestination.transform.localPosition);
            
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return null;
    }
}
