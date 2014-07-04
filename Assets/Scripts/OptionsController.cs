using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour 
{
    // update gui
    public UILabel resetAllButton;
    public UILabel backButton;

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        resetAllButton.text = StaticTexts.Instance.language_ResetAll[PlayerPrefs.GetInt("SelectedLanguage", 0)];
        backButton.text = StaticTexts.Instance.language_Back[PlayerPrefs.GetInt("SelectedLanguage", 0)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectedBack()
    {
        Application.LoadLevel("MainMenu");
    }

    public void SelectedReset()
    {
        PlayerPrefs.DeleteAll();
        gameDataControllerScript.InitializeTowers();
    }
}
