using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour 
{
	private bool isTower = false;
	private Health hscr;
	private Money mscr;
	private EnemyStats esscr;

	// Use this for initialization
	void Start () 
	{
		hscr = gameObject.GetComponent<Health> ();
		mscr = GameObject.Find ("GameLogic").GetComponent<Money> ();

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
				mscr.money += esscr.worth;
			}

			Destroy(gameObject);
		}
	}
}
