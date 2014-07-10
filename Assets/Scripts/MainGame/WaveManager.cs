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
    private float spawnCooldown = 0.5f;
    private int numOfEnemies = 0;

	// Use this for initialization
	void Start () 
    {
        numOfEnemies = 0;
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

            if(spawnCooldown > 0)
            {
                spawnCooldown -= Time.deltaTime;
            }
            else
            {
                float multiplicator = Mathf.Pow(wavesData.multiplicator, currentWave);
                Vector3 pos = new Vector3(4f, 0.8f, Random.Range(-2, 3));
                int ind = Random.Range(0, wavesData.enemies.Length);

                GameObject enemyObject = (GameObject)Instantiate(wavesData.enemies[ind].prefab, pos, Quaternion.identity);
                enemyObject.GetComponent<Health>().startingHealth = wavesData.enemies[ind].health * multiplicator;
                enemyObject.GetComponent<Health>().deathSound = wavesData.enemies[ind].deathSound;
                enemyObject.GetComponent<Health>().takeDamageSound = wavesData.enemies[ind].takeDamageSound;
                enemyObject.GetComponent<EnemyMove>().movementSpeed = wavesData.enemies[ind].movementSpeed * multiplicator;
                enemyObject.GetComponent<EnemyStats>().worth = wavesData.enemies[ind].worth;
                enemyObject.GetComponent<EnemyDamage>().damage = wavesData.enemies[ind].damage * multiplicator;
                enemyObject.GetComponent<EnemyDamage>().cooldown = wavesData.enemies[ind].damageCooldown * multiplicator;
                enemyObject.GetComponent<EnemyDamage>().dealDamageSound = wavesData.enemies[ind].dealDamageSound;

                numOfEnemies++;
                spawnCooldown = 0.5f;
            }

            if(numOfEnemies == wavesData.numberOfEnemies + (currentWave * wavesData.enemyNumIncrease))
            {
                waveCooldown = wavesData.newWaveCooldown;
                currentWave++;
                numOfEnemies = 0;
            }
        }

        //waveTimerLabel.text = "New wave in: " + ((int)waveCooldown).ToString();
        waveTimerLabel.text = StaticTexts.Instance.language_newWaveIn[PlayerPrefs.GetInt("SelectedLanguage", 0)] 
        + ((int)waveCooldown).ToString();
	}
}
