using UnityEngine;
using System.Collections;

public class MoneyLabel : MonoBehaviour 
{
    public Money moneyScript;
    public UILabel label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        label.text = "Money: " + moneyScript.money;
	}
}
