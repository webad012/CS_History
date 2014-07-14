using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour 
{
    public float cooldown;
    public float damage;
    public GameObject projectilePrefab;
    public bool catapult = false;

    public AudioClip shootSound;
    public int timesPlayed;
    public float pauseBetweenShoots;
    public float startSoundPosition;
    private AudioSource shootSource;

    private float cd;
    private TowerBase towerBaseScript;
    private Transform shootSpawn;

	// Use this for initialization
	void Start () 
    {
        shootSource = gameObject.AddComponent<AudioSource>();
        shootSource.volume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        shootSource.time = startSoundPosition;
        shootSource.clip = shootSound;

        RestartCooldown();

        towerBaseScript = gameObject.GetComponent<TowerBase>();
        shootSpawn = gameObject.transform.Find("ShootSpawn").gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (cd > 0)
        {
            cd -= Time.deltaTime;
        }

        if (towerBaseScript.hasEnemy)
        {
            if (cd <= 0)
            {
                RestartCooldown();

                GameObject projectile = (GameObject)Instantiate(projectilePrefab, shootSpawn.position, Quaternion.identity);
  
                if(projectile.GetComponent<Projectile>() != null)
                {
                    projectile.GetComponent<Projectile>().damage = damage;
                }
                else if(projectile.GetComponent<ProjectileBinaryController>() != null)
                {
                    projectile.GetComponent<ProjectileBinaryController>().damage = damage;
                }
                else if(projectile.GetComponent<CatapultProjectile>() != null)
                {
                    projectile.GetComponent<CatapultProjectile>().target = towerBaseScript.enemyLocation;
                    projectile.GetComponent<CatapultProjectile>().damage = damage;
                }

                if(shootSound)
                {
                    StartCoroutine(PlayShoot());
                }
            }
        }
	}

    IEnumerator PlayShoot()
    {
        for(int i=0; i<timesPlayed; i++)
        {
            shootSource.Play();
            shootSource.time = startSoundPosition;
            yield return new WaitForSeconds(pauseBetweenShoots);
        }
    }

    void RestartCooldown()
    {
        cd = cooldown + Random.Range(-0.2f, 0.2f);
    }
}
