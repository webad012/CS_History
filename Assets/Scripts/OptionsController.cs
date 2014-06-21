using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour 
{
    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();
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
