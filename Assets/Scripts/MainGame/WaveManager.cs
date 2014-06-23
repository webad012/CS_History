using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour 
{
    //public int numOut;
    public bool waveStarted = false;
    public int initialPouse;
    //public GameObject[] enemies;
    public EnemyData[] enemies;
    public float cooldown;
    private float cd;

    //private GameDataController gameDataControllerScript;
    private int levelSelected;

	// Use this for initialization
	void Start () 
    {
        levelSelected = PlayerPrefs.GetInt("LevelSelected", 0);
        //gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        enemies = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>().levels [levelSelected].enemies;

        cd = cooldown * initialPouse;
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

            cd = cooldown;
            Vector3 pos = new Vector3(4f, 0.8f, Random.Range(-2, 3));
            //Vector3 pos = new Vector3(4f, 1f, Random.Range(-2, 3));
            int ind = Random.Range(0, enemies.Length);

            GameObject enemyObject = (GameObject)Instantiate(enemies[ind].prefab, pos, Quaternion.identity);
            enemyObject.GetComponent<Health>().health = enemies[ind].health;
            enemyObject.GetComponent<EnemyMove>().movementSpeed = enemies[ind].movementSpeed;
            enemyObject.GetComponent<EnemyStats>().worth = enemies[ind].worth;
            enemyObject.GetComponent<EnemyDamage>().damage = enemies[ind].damage;
            enemyObject.GetComponent<EnemyDamage>().cooldown = enemies[ind].damageCooldown;
            //Instantiate(enemies[ind], pos, Quaternion.identity);
            //numOut++;
        }
	}

    public void resetCD()
    {
        cd = cooldown * initialPouse;
    }
}
