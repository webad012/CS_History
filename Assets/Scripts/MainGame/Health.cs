using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public float startingHealth = 50;
    public GameObject bloodPrefab;
    public bool ready = false;
    public AudioClip deathSound;
    public AudioClip takeDamageSound;

    private float actualHealth;

    private bool isEnemy = false;

    private GameDataController gameDataControllerScript;

    void Start()
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        actualHealth = startingHealth;

        if (gameObject.tag == "Tower") 
        {
            isEnemy = false;
        } 
        else 
        {
            isEnemy = true;
        }

        ready = true;
    }

    public void TakeDamage(float damage)
    {
        if (actualHealth > 0)
        {
            bool destroyed = false;

            actualHealth -= damage;

            if (bloodPrefab)
            {
                Instantiate(bloodPrefab, gameObject.transform.position, Quaternion.identity);
            }

            if (actualHealth <= 0)
            {
                ready = false;

                if (isEnemy)
                {
                    EnemyStats esscr = gameObject.GetComponent<EnemyStats>();
                    TowerDefenseController tdcscr = GameObject.Find("TowerDefenseController").GetComponent<TowerDefenseController>();

                    int playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
                    playerCoins += esscr.worth;
                    StartCoroutine(tdcscr.SprinkleCoins(esscr.worth, 
                                                    10, 
                                                    gameObject.transform.position));
                    PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                }

                
                if(deathSound)
                {
                    gameDataControllerScript.PlayAudioClip(deathSound);
                }

                destroyed = true;
                Destroy(gameObject);
            }

            if(!destroyed && takeDamageSound)
            {
                gameDataControllerScript.PlayAudioClip(takeDamageSound);
            }
        }
    }
}