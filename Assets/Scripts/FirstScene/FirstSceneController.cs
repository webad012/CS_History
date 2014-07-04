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
     * int SelectedLanguage
     * 
     */

    public UILabel statusLabel;

    private GameDataController gameDataControllerScript;

    private float currentVersion = 0.1f;
    private bool checkedVersion = false;
    private bool checkingSuccessful = false;
    private bool versionOk = false;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        statusLabel.text = "Game initializing...";

        if (Application.isWebPlayer)
        {
            checkedVersion = true;
            checkingSuccessful = true;
            versionOk = true;
        }
        else
        {
            StartCoroutine (CheckGameVersion());
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (checkedVersion)
        {
            if(!checkingSuccessful)
            {
                if (gameDataControllerScript.canContinue)
                {
                    statusLabel.text = "There was connection problem,\nbut you may play offline.\nPress any key to continue.";
                    if (Input.anyKeyDown)
                    {
                        Application.LoadLevel("MainMenu");
                    }
                }
            }
            else
            {
                if(!versionOk)
                {
                    if (gameDataControllerScript.canContinue)
                    {
                        statusLabel.text = "Version obsolete, please download latest version,\nbut you may play offline.\nPress any key to continue.";
                        if (Input.anyKeyDown)
                        {
                            Application.LoadLevel("MainMenu");
                        }
                    }
                }
                else
                {
                    if (gameDataControllerScript.canContinue)
                    {
                        statusLabel.text = "Game initialzied.\nPress any key to continue.";

                        if (Input.anyKeyDown)
                        {
                            Application.LoadLevel("MainMenu");
                        }
                    }
                }
            }
        }
	}

    IEnumerator CheckGameVersion () 
    {
        statusLabel.text = "Game initializing...Checking version";

        string url = StaticTexts.Instance.web_api_location + "?action=GetCurrentVersion";

        WWW hs_get = new WWW(url);
        yield return hs_get;
        
        if (hs_get.error != null)
        {
            checkedVersion = true;
            checkingSuccessful = false;
        }
        else
        {
            if(!hs_get.text.Contains("bad action"))
            {
                checkedVersion = true;
                checkingSuccessful = true;
                
                float serverVersion;
                float.TryParse(hs_get.text, out serverVersion);
                if(currentVersion != serverVersion)
                {
                    versionOk = false;
                }
                else
                {
                    versionOk = true;
                }
            }
        }
    }
}
