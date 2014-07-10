using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour 
{
    // update gui
    public UILabel resetAllButton;
    public UILabel backButton;
    public GameObject volumeObject;

    public GameObject optionsWindow;
    public GameObject resetAllWindow;

    private GameDataController gameDataControllerScript;

    private int selectedLanguage;
    private float soundsVolume;

	// Use this for initialization
	void Start () 
    {
        optionsWindow.SetActive(true);
        resetAllWindow.SetActive(false);

        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);
        soundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);

        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        resetAllButton.text = StaticTexts.Instance.language_ResetAll[selectedLanguage];
        backButton.text = StaticTexts.Instance.language_Back[selectedLanguage];

        UpdateGUI();
	}

    void UpdateGUI()
    {
        resetAllButton.text = StaticTexts.Instance.language_ResetAll [selectedLanguage];
        backButton.text = StaticTexts.Instance.language_Back [selectedLanguage];

        volumeObject.transform.Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Volume [selectedLanguage];
        volumeObject.transform.Find("Value").GetComponent<UILabel>().text = (soundsVolume*10).ToString();

        resetAllWindow.transform.Find("Label_Title").GetComponent<UILabel>().text = StaticTexts.Instance.language_ResetAll [selectedLanguage]
        + "\n" + StaticTexts.Instance.language_AreYouSure [selectedLanguage];
        resetAllWindow.transform.Find("Button_No").Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_No [selectedLanguage];
        resetAllWindow.transform.Find("Button_Yes").Find("Label").GetComponent<UILabel>().text = StaticTexts.Instance.language_Yes [selectedLanguage];
    }

    public void SelectedBack()
    {
        Application.LoadLevel("MainMenu");
    }

    public void SelectedReset()
    {
        optionsWindow.SetActive(false);
        resetAllWindow.SetActive(true);
    }

    void RestartYesButton()
    {
        PlayerPrefs.DeleteAll();
        gameDataControllerScript.InitializeTowers();
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        optionsWindow.SetActive(true);
        resetAllWindow.SetActive(false);

        UpdateGUI();
    }

    void RestartNoButton()
    {
        optionsWindow.SetActive(true);
        resetAllWindow.SetActive(false);
    }

    void VolumeUp()
    {
        if (soundsVolume < 1f)
        {
            soundsVolume += 0.1f;
            soundsVolume = (float)System.Math.Round(soundsVolume, 1);
            PlayerPrefs.SetFloat("SoundsVolume", soundsVolume);
            UpdateGUI();
        }
    }

    void VolumeDown()
    {
        if (soundsVolume > 0f)
        {
            soundsVolume -= 0.1f;
            soundsVolume = (float)System.Math.Round(soundsVolume, 1);
            PlayerPrefs.SetFloat("SoundsVolume", soundsVolume);
            UpdateGUI();
        }
    }
}
