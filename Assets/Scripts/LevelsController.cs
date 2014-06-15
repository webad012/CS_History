using UnityEngine;
using System.Collections;

public class LevelsController : MonoBehaviour 
{
    public GameObject[] levels;
    public float outLeftX;
    public float outRightX;
    public float levelTransitionSpeed = 0.5f;
    //public UISprite xMark;
    //public Vector3 xMarkUnusedPos;

    private Vector3 outLeftVector;
    private Vector3 outRightVector;
    private int currentLevel = 0;
    private int lastUnlockedLevel;
    private int lastUnlockedStory;

	// Use this for initialization
	void Start () 
    {
        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);

        lastUnlockedLevel = PlayerPrefs.GetInt("lastUnlockedLevel", 0);
        lastUnlockedStory = PlayerPrefs.GetInt("lastUnlockedStory", -1);

        for(int i=0; i<levels.Length; i++)
        {
            if(i>lastUnlockedLevel)
            {
            //Debug.Log
                foreach(Transform c in levels[i].transform)
                {
                    if(c.name == "xMark")
                    {
                        c.gameObject.SetActive(true);
                    }
                    else if(c.name == "Button")
                    {
                        c.gameObject.collider.enabled = false;
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ButtonLeft()
    {
        if (currentLevel > 0)
        {
            TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, outRightVector);
            currentLevel--;
            TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, new Vector3(0, 0, 0));

            /*if(currentLevel > lastUnlockedLevel)
            {
                xMark.transform.position = outLeftVector;
                TweenPosition.Begin(xMark.gameObject, levelTransitionSpeed, new Vector3(0, 0, 0));
            }
            else
            {
                xMark.transform.position = xMarkUnusedPos;
            }*/
        }
    }

    void ButtonRight()
    {
        if (currentLevel != levels.Length - 1)
        {
            TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, outLeftVector);
            currentLevel++;
            TweenPosition.Begin(levels[currentLevel], levelTransitionSpeed, new Vector3(0, 0, 0));

            /*if(currentLevel > lastUnlockedLevel)
            {
                xMark.transform.position = outRightVector;
                TweenPosition.Begin(xMark.gameObject, levelTransitionSpeed, new Vector3(0, 0, 0));
            }
            else
            {
                xMark.transform.position = xMarkUnusedPos;
            }*/
        }
    }

    void LevelSelected()
    {
        PlayerPrefs.SetInt("LevelSelected", currentLevel);
        Application.LoadLevel("Story");
    }
}
