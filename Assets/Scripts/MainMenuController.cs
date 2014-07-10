using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour 
{
    public UILabel knowledgeValueLabel;
    public GameObject[] options;
    public float outLeftX;
    public float outRightX;
    public float optionTransitionSpeed = 0.25f;

    // update gui
    public UISprite languageFlag;
    public UILabel playButton;
    public UILabel upgradeButton;
    public UILabel optionsButton;
    public UILabel exitButton;

    private int knowledge_int;
    private string knowledge_string;

    private int currentOption = 0;
    private Vector3 outLeftVector;
    private Vector3 outRightVector;

    private int selectedLanguage;

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);

        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonRight();
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {

        }
    }

    void ButtonLeft()
    {
        if (currentOption > 0)
        {
            TweenPosition.Begin(options[currentOption], optionTransitionSpeed, outRightVector);
            currentOption--;
            TweenPosition.Begin(options[currentOption], optionTransitionSpeed, new Vector3(0, 0, 0));
        }
    }
    
    void ButtonRight()
    {
        if (currentOption != options.Length - 1)
        {
            TweenPosition.Begin(options[currentOption], optionTransitionSpeed, outLeftVector);
            currentOption++;
            TweenPosition.Begin(options[currentOption], optionTransitionSpeed, new Vector3(0, 0, 0));
        }
    }

    void PlaySelected()
    {
        Application.LoadLevel("LevelSelector");
    }

    void UpgradeSelected()
    {
        Application.LoadLevel("Upgrade");
    }
    
    void OptionsSelected()
    {
        Application.LoadLevel("Options");
    }
    
    void ExitSelected()
    {
        Application.Quit();
    }

    void LanguageButton()
    {
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
