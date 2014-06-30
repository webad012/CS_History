using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour 
{
    public bool waveStarted = false;
    public UILabel waveTimerLabel;
    private float waveCooldown;

    private int levelSelected;
    private WaveData wavesData;
    private int currentWave = 0;
    //private int enemiesOut = 0;

	// Use this for initialization
	void Start () 
    {
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);

        wavesData = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().levels [levelSelected].wavesData;

        waveCooldown = wavesData.initialCooldown;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (waveCooldown > 0)
        {
            waveCooldown -= Time.deltaTime;
        } 
        else
        {
            if(!waveStarted)
            {
                waveStarted = true;
            }

            //waveCooldown = wavesData.spawnCooldown;
            //Vector3 pos = new Vector3(4f, 0.8f, Random.Range(-2, 3));
            //int ind = Random.Range(0, wavesData.enemies.Length);

            float multiplicator = Mathf.Pow(wavesData.multiplicator, currentWave);

            Debug.Log(multiplicator);

            //GameObject enemyObject = (GameObject)Instantiate(wavesData.enemies[ind].prefab, pos, Quaternion.identity);
            //enemyObject.GetComponent<Health>().health = wavesData.enemies[ind].health * multiplicator;
            //enemyObject.GetComponent<EnemyMove>().movementSpeed = wavesData.enemies[ind].movementSpeed * multiplicator;
            //enemyObject.GetComponent<EnemyStats>().worth = wavesData.enemies[ind].worth;
            //enemyObject.GetComponent<EnemyDamage>().damage = wavesData.enemies[ind].damage * multiplicator;
            //enemyObject.GetComponent<EnemyDamage>().cooldown = wavesData.enemies[ind].damageCooldown * multiplicator;

            //float spawnCooldown = wavesData.spawnCooldown;
            for(int i=0; i<wavesData.numberOfEnemies + (currentWave * wavesData.enemyNumIncrease); i++)
            {
                Vector3 pos = new Vector3(4f, 0.8f, Random.Range(-2, 3));
                int ind = Random.Range(0, wavesData.enemies.Length);

                GameObject enemyObject = (GameObject)Instantiate(wavesData.enemies[ind].prefab, pos, Quaternion.identity);
                enemyObject.GetComponent<Health>().health = wavesData.enemies[ind].health * multiplicator;
                enemyObject.GetComponent<EnemyMove>().movementSpeed = wavesData.enemies[ind].movementSpeed * multiplicator;
                enemyObject.GetComponent<EnemyStats>().worth = wavesData.enemies[ind].worth;
                enemyObject.GetComponent<EnemyDamage>().damage = wavesData.enemies[ind].damage * multiplicator;
                enemyObject.GetComponent<EnemyDamage>().cooldown = wavesData.enemies[ind].damageCooldown * multiplicator;
            }

            //enemiesOut++;

            //if(enemiesOut == wavesData.numberOfEnemies + (currentWave * wavesData.enemyNumIncrease))
            //{
            //    enemiesOut = 0;
            waveCooldown = wavesData.newWaveCooldown;
            currentWave++;
            //}
        }

        waveTimerLabel.text = "New wave in: " + ((int)waveCooldown).ToString();
	}

    /*public void resetCD()
    {
        cd = cooldown * initialPouse;
    }*/
}
