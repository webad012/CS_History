using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour 
{
    public bool waveStarted = false;
    //public int initialPouse;
    //public EnemyData[] enemies;
    //public float cooldown;
    private float cd;

    private int levelSelected;
    private WaveData wavesData;
    private int currentWave = 0;
    private int enemiesOut = 0;

	// Use this for initialization
	void Start () 
    {
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);

        //enemies = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().levels [levelSelected].enemies;
        wavesData = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().levels [levelSelected].wavesData;

        //cd = cooldown * initialPouse;
        cd = wavesData.startCooldown;
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
            if(!waveStarted)
            {
                waveStarted = true;
            }

            //cd = cooldown;
            cd = wavesData.spawnCooldown;
            Vector3 pos = new Vector3(4f, 0.8f, Random.Range(-2, 3));
            //int ind = Random.Range(0, enemies.Length);
            int ind = Random.Range(0, wavesData.enemies.Length);

            int multiplicator = (int)Mathf.Pow(wavesData.multiplicator, currentWave);

            //GameObject enemyObject = (GameObject)Instantiate(enemies[ind].prefab, pos, Quaternion.identity);
            GameObject enemyObject = (GameObject)Instantiate(wavesData.enemies[ind].prefab, pos, Quaternion.identity);
            enemyObject.GetComponent<Health>().health = wavesData.enemies[ind].health * multiplicator;
            enemyObject.GetComponent<EnemyMove>().movementSpeed = wavesData.enemies[ind].movementSpeed * multiplicator;
            enemyObject.GetComponent<EnemyStats>().worth = wavesData.enemies[ind].worth;
            enemyObject.GetComponent<EnemyDamage>().damage = wavesData.enemies[ind].damage * multiplicator;
            enemyObject.GetComponent<EnemyDamage>().cooldown = wavesData.enemies[ind].damageCooldown * multiplicator;

            enemiesOut++;

            if(enemiesOut == wavesData.numberOfEnemies)
            {
                enemiesOut = 0;
                cd = wavesData.startCooldown;
                currentWave++;
            }
        }
	}

    /*public void resetCD()
    {
        cd = cooldown * initialPouse;
    }*/
}
