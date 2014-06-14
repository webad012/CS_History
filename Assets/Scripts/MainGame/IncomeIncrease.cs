using UnityEngine;
using System.Collections;

public class IncomeIncrease : MonoBehaviour 
{
    public float cooldown;
    private float cd;
    public int income;
    private Money moneyScript;

	// Use this for initialization
	void Start () 
    {
        moneyScript = GameObject.Find("GameLogic").GetComponent<Money>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (cd > 0)
        {
            cd -= Time.deltaTime;
        } 
        else
        {
            cd = cooldown;
            moneyScript.money += income;
        }
	}
}
