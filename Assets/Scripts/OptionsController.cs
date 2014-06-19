using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
    }
}
