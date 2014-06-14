using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour 
{
    public float cooldown;
    private float cd;
    public GameObject projectile;
    public bool catapult = false;
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

                if(catapult)
                {
                    //Vector3 direction = towerBaseScript.enemyLocation - transform.position;
                    //Vector3 direction = towerBaseScript.enemyLocation;
                    //throwThis = originalThrow - ((originalDist - Vector3.Distance(placeHolder.transform.position, target.transform.position)) *35);
                    //var clone = Instantiate(grenade, placeHolder.transform.position, placeHolder.transform.rotation);
                    //clone.rigidbody.AddRelativeForce(Vector3.forward * (throwThis));
                    GameObject catapultProjectile = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
                    //catapultProjectile.rigidbody.AddRelativeForce(Vector3.forward * 100);
                    //catapultProjectile.rigidbody.velocity = (Vector3.up + direction.normalized) * 2;
                    //catapultProjectile.target = towerBaseScript.enemyLocation;
                    catapultProjectile.GetComponent<CatapultProjectile>().target = towerBaseScript.enemyLocation;
                }
                else
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
            }
        }
	}
}
