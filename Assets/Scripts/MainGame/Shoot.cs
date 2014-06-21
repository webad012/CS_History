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

	// Use this for initialization
	void Start () 
    {
        cd = cooldown;

        towerBaseScript = gameObject.GetComponent<TowerBase>();
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
                cd = cooldown;

                GameObject projectile;

                if(catapult)
                {
                    projectile = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    projectile.GetComponent<CatapultProjectile>().target = towerBaseScript.enemyLocation;
                    projectile.GetComponent<CatapultProjectile>().damage = damage;
                }
                else
                {
                    projectile = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    projectile.GetComponent<Projectile>().damage = damage;
                }
            }
        }
	}
}
