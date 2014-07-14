using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour 
{
    public UILabel creditsLabel;
    public float creditsTime;

    private float countdown;

	// Use this for initialization
	void Start () 
    {
        countdown = creditsTime;

        creditsLabel.text = StaticTexts.Instance.language_Credits;
        creditsLabel.transform.localPosition = new Vector3(0, -(Screen.height/2), 0);

        float newY = (Screen.height * 2/3) + (creditsLabel.font.CalculatePrintedSize(creditsLabel.text, false, UIFont.SymbolStyle.None).y * creditsLabel.font.size * 2);

        TweenPosition.Begin(creditsLabel.gameObject, creditsTime, new Vector3(0, newY, 0));
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            Application.Quit();
        }

        if (Input.anyKeyDown)
        {
            Application.Quit();
        }
	}
}
