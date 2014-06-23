using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour 
{
    //public GameObject tile;
	private bool isTower = false;
	private Health hscr;
	private EnemyStats esscr;

	// Use this for initialization
	void Start () 
	{
		hscr = gameObject.GetComponent<Health> ();

		if (gameObject.tag == "Tower") 
		{
			isTower = true;
		} 
		else 
		{
			esscr = gameObject.GetComponent<EnemyStats> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hscr.health <= 0) 
		{
			if(!isTower)
			{
                int playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
                playerCoins += esscr.worth;
                PlayerPrefs.SetInt("PlayerCoins", playerCoins);
			}

			Destroy(gameObject);
		}
	}
}
