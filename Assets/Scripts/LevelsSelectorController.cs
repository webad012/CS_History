using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsSelectorController : MonoBehaviour 
{
    public float outLeftX;
    public float outRightX;
    public float levelTransitionSpeed = 0.5f;
    public GameObject levelsAnchor;

    private Vector3 outLeftVector;
    private Vector3 outRightVector;
    private int currentLevel = 0;
    private int lastUnlockedStory;

    private GameDataController gameDataControllerScript;

    private List<GameObject> levelObjects;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);

        currentLevel = PlayerPrefs.GetInt("LevelSelected", 0);
        lastUnlockedStory = PlayerPrefs.GetInt("lastUnlockedStory", -1);

        levelObjects = new List<GameObject>();
        for(int i=0; i<gameDataControllerScript.levels.Length; i++)
        {
            Vector3 level_pos = Vector3.zero;
            if(i == 0 || PlayerPrefs.GetInt("LevelUnlocked" + i.ToString(), 0) == 1)
            {
                level_pos = outLeftVector;
            }
            else
            {
                level_pos = outRightVector;
            }

            GameObject childButton = NGUITools.AddChild(levelsAnchor, gameDataControllerScript.levelPrefab);
            childButton.name = "Button_Level_" + i.ToString();
            levelObjects.Add(childButton);

            foreach(Transform c1 in levelObjects[i].transform)
            {
                if(c1.name == "xMark")
                {
                    //if(gameDataControllerScript.levels[i].knowledgeRequired > knowledge_int)
                    if(i == 0 || PlayerPrefs.GetInt("LevelUnlocked" + i.ToString(), 0) == 1)
                    {
                        c1.gameObject.SetActive(false);
                    }
                    else
                    {
                        c1.gameObject.SetActive(true);
                    }
                }
                else if(c1.name == "Button")
                {
                    if(i == 0 || PlayerPrefs.GetInt("LevelUnlocked" + i.ToString(), 0) == 1)
                    {
                        c1.gameObject.collider.enabled = true;
                    }
                    else
                    {
                        c1.gameObject.collider.enabled = false;
                    }

                    c1.gameObject.GetComponent<UIButtonMessage>().target = gameObject;

                    foreach(Transform c2 in c1.transform)
                    {
                        if(c2.name == "Background")
                        {
                            c2.gameObject.GetComponent<UISprite>().spriteName = gameDataControllerScript.levels[i].spritename;
                        }
                        else if(c2.name == "Label")
                        {
                            string level_text = gameDataControllerScript.levels[i].levelName
                                + "\n" + gameDataControllerScript.levels[i].location 
                                    + "\n" + gameDataControllerScript.levels[i].time;
                            c2.gameObject.GetComponent<UILabel>().text = level_text;
                        }
                    }
                }
            }
            levelObjects[i].transform.localPosition  = level_pos;
        }

        levelObjects [currentLevel].transform.localPosition = Vector3.zero;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("MainMenu");
        }
    }

    void ButtonLeft()
    {
        if (currentLevel > 0)
        {
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, outRightVector);
            currentLevel--;
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, Vector3.zero);
        }
    }

    void ButtonRight()
    {
        if (currentLevel != levelObjects.Count - 1)
        {
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, outLeftVector);
            currentLevel++;
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, Vector3.zero);
        }
    }

    void LevelSelected()
    {
        PlayerPrefs.SetInt("LevelSelected", currentLevel);

        if (currentLevel <= lastUnlockedStory)
        {
            Application.LoadLevel("TowerDefense");
        } 
        else
        {
            Application.LoadLevel("MiniGame");
        }
    }
}
