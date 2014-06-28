using UnityEngine;
using System.Collections;

public class FirstSceneController : MonoBehaviour 
{
    /*
     *  Used player prefs:
     * 
     * int TowerUnlocked{towernum}
     * int HealthLevel_{towernum}
     * int DamageLevel_{towernum}
     * int CooldownLevel_{towernum}
     * int PlayerKnowledge
     * int LevelSelected
     * int PlayerCoins
     * int LevelUnlocked
     * int lastUnlockedStory
     * 
     */

    public UILabel statusLabel;

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        //Debug.Log(PlayerPrefs.GetInt("TowerUnlocked0"));
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        statusLabel.text = "Game initializing...";
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gameDataControllerScript.canContinue)
        {
            statusLabel.text = "Game initialzied.\nPress any key to continue.";

            if(Input.anyKeyDown)
            {
                Application.LoadLevel("MainMenu");
            }
        }
	}
}
