using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsSelectorController : MonoBehaviour 
{
    //public GameObject[] levels;
    public float outLeftX;
    public float outRightX;
    public float levelTransitionSpeed = 0.5f;
    public GameObject levelsAnchor;
    public UILabel knowledgeRequiredLabel;
    public UILabel knowledgeValueLabel;

    private Vector3 outLeftVector;
    private Vector3 outRightVector;
    private int currentLevel = 0;
    //private int lastUnlockedLevel;
    private int lastUnlockedStory;
    private int knowledge_int;
    private string knowledge_string;

    private GameDataController gameDataControllerScript;

    //private Level[] levelObjects;
    private List<GameObject> levelObjects;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);

        //lastUnlockedLevel = PlayerPrefs.GetInt("lastUnlockedLevel", 0);
        lastUnlockedStory = PlayerPrefs.GetInt("lastUnlockedStory", -1);
        knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (knowledge_int == 0)
        {
            knowledge_string = knowledge_int.ToString();
        }
        else
        {
            knowledge_string = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        }

        //for(int i=0; i<levels.Length; i++)
        levelObjects = new List<GameObject>();
        for(int i=0; i<gameDataControllerScript.levels.Length; i++)
        {
            Vector3 level_pos = Vector3.zero;
            if(gameDataControllerScript.levels[i].knowledgeRequired < knowledge_int)
            {
                level_pos = outLeftVector;
            }
            else if(gameDataControllerScript.levels[i].knowledgeRequired > knowledge_int)
            {
                level_pos = outRightVector;
            }

            //levelObjects.Add((GameObject)Instantiate(gameDataControllerScript.levelPrefab, level_pos, Quaternion.identity));
            GameObject childButton = NGUITools.AddChild(levelsAnchor, gameDataControllerScript.levelPrefab);
            childButton.name = "Button_Level_" + i.ToString();
            levelObjects.Add(childButton);
            //levelObjects.Add((GameObject)Instantiate(gameDataControllerScript.levelPrefab, Vector3.zero, Quaternion.identity));
            /*if(i>lastUnlockedLevel)
            {
                //foreach(Transform c in levels[i].transform)
                foreach(Transform c1 in levelObjects[i].transform)
                {
                    if(c1.name == "xMark")
                    {
                        c1.gameObject.SetActive(true);
                    }
                    else if(c1.name == "Button")
                    {
                        c1.gameObject.collider.enabled = false;
                    }
                }
            }*/

            foreach(Transform c1 in levelObjects[i].transform)
            {
                if(c1.name == "xMark")
                {
                    if(gameDataControllerScript.levels[i].knowledgeRequired > knowledge_int)
                    {
                        c1.gameObject.SetActive(true);
                    }
                    else
                    {
                        c1.gameObject.SetActive(false);
                    }
                }
                else if(c1.name == "Button")
                {
                    if(gameDataControllerScript.levels[i].knowledgeRequired > knowledge_int)
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
            //levelObjects[i].transform.parent = levelsAnchor;
            //NGUITools.AddChild(levelsAnchor, levelObjects[i]);
            levelObjects[i].transform.localPosition  = level_pos;
        }

        levelObjects [currentLevel].transform.localPosition = Vector3.zero;

        UpdateGUI();
	}

    void ButtonLeft()
    {
        if (currentLevel > 0)
        {
            //TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, outRightVector);
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, outRightVector);
            currentLevel--;
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, new Vector3(0, 0, 0));
            //TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, new Vector3(0, 0, 0));
            UpdateGUI();
        }
    }

    void ButtonRight()
    {
        //if (currentLevel != levels.Length - 1)
        if (currentLevel != levelObjects.Count - 1)
        {
            //TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, outLeftVector);
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, outLeftVector);
            currentLevel++;
            //TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, new Vector3(0, 0, 0));
            TweenPosition.Begin(levelObjects[currentLevel], levelTransitionSpeed, new Vector3(0, 0, 0));
            UpdateGUI();
        }
    }

    void UpdateGUI()
    {
        knowledgeValueLabel.text = knowledge_string;

        knowledgeRequiredLabel.text = gameDataControllerScript.levels [currentLevel].knowledgeRequired.ToString();
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
