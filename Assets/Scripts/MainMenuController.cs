using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour 
{
    public UILabel knowledgeValueLabel;
    public GameObject[] options;
    public float outLeftX;
    public float outRightX;
    public float optionTransitionSpeed = 0.25f;

    private int knowledge_int;
    private string knowledge_string;

    private int currentOption = 0;
    private Vector3 outLeftVector;
    private Vector3 outRightVector;

	// Use this for initialization
	void Start () 
    {
        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);

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
	}
	
	// Update is called once per frame
	void Update () {
	
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
    }
}
