using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour 
{
    public float cooldown;
    public float damage;
    public GameObject projectilePrefab;
    public bool catapult = false;

    private float cd;
    private TowerBase towerBaseScript;
    private Transform shootSpawn;

	// Use this for initialization
	void Start () 
    {
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

                GameObject projectile;

                if(catapult)
                {
                    projectile = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    projectile.GetComponent<CatapultProjectile>().target = towerBaseScript.enemyLocation;
                    projectile.GetComponent<CatapultProjectile>().damage = damage;
                }
                else
                {
                    projectile = (GameObject)Instantiate(projectilePrefab, shootSpawn.position, Quaternion.identity);

                    if(projectile.GetComponent<Projectile>() != null)
                    {
                        projectile.GetComponent<Projectile>().damage = damage;
                    }
                    else if(projectile.GetComponent<ProjectileBinaryController>() != null)
                    {
                        projectile.GetComponent<ProjectileBinaryController>().damage = damage;
                    }
                }
            }
        }
	}

    void RestartCooldown()
    {
        cd = cooldown + Random.Range(-0.2f, 0.2f);
    }
}
