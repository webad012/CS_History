/*using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour 
{
	private bool isTower = false;
	private Health hscr;
	private EnemyStats esscr;
    private TowerDefenseController tdcscr;

	// Use this for initialization
	void Start () 
	{
		hscr = gameObject.GetComponent<Health> ();
        tdcscr = GameObject.Find("TowerDefenseController").GetComponent<TowerDefenseController>();

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
                StartCoroutine(tdcscr.SprinkleCoins(esscr.worth, 
                                                    10, 
                                                    gameObject.transform.position));
                PlayerPrefs.SetInt("PlayerCoins", playerCoins);
			}

			Destroy(gameObject);
		}
	}
}*/
