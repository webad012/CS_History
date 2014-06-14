using UnityEngine;
using System.Collections;

public class LooseGame : MonoBehaviour 
{
    public bool lost = false;
    private int initMoney;
    private Money moneyScript;
    private WaveManager waveManagerScript;

	// Use this for initialization
	void Start () 
    {
        moneyScript = gameObject.GetComponent<Money>();
        waveManagerScript = gameObject.GetComponent<WaveManager>();
        initMoney = moneyScript.money;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (lost)
        {
            lost = false;
            waveManagerScript.resetCD();

            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            for(int i=0; i<towers.Length; i++)
            {
                Destroy(towers[i]);
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for(int j=0; j<enemies.Length; j++)
            {
                Destroy(enemies[j]);
            }
            
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            for(int k=0; k<tiles.Length; k++)
            {
                tiles[k].GetComponent<TileTaken>().isTaken = false;
            }

            moneyScript.money = initMoney;
        }
	}
}
