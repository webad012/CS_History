using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour 
{
    public UILabel knowledgeValueLabel;
    public GameObject[] options;
    public float optionTransitionSpeed = 0.25f;

    // update gui
    public UISprite languageFlag;
    public UILabel playButton;
    public UILabel upgradeButton;
    public UILabel optionsButton;
    public UILabel exitButton;

    private int knowledge_int;
    private string knowledge_string;

    private int selectedLanguage;

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        gameDataControllerScript.PlayBackgroundMusic(gameDataControllerScript.sounds.backgroundMenu);

        knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (knowledge_int == 0)
        {
            knowledge_string = knowledge_int.ToString();
        }
        else
        {
            knowledge_string = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        }

        knowledgeValueLabel.text = knowledge_string;

        UpdateGUI();
	}

    void Update()
    {
    }

    void PlaySelected()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("LevelSelector");

    }

    void UpgradeSelected()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("Upgrade");
    }
    
    void OptionsSelected()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("Options");
    }
    
    void ExitSelected()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("Credits");
    }

    void LanguageButton()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        if (selectedLanguage + 1 == gameDataControllerScript.languages.Length)
        {
            selectedLanguage = 0;
        }
        else
        {
            selectedLanguage++;
        }
        PlayerPrefs.SetInt("SelectedLanguage", selectedLanguage);

        UpdateGUI();
    }

    void UpdateGUI()
    {
        languageFlag.spriteName = gameDataControllerScript.languages[selectedLanguage].spritename;
        playButton.text = StaticTexts.Instance.language_Play[selectedLanguage];
        upgradeButton.text = StaticTexts.Instance.language_Upgrade[selectedLanguage];
        optionsButton.text = StaticTexts.Instance.language_Options[selectedLanguage];
        exitButton.text = StaticTexts.Instance.language_Exit[selectedLanguage];
    }
}
