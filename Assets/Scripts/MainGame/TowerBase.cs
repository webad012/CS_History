using UnityEngine;
using System.Collections;

public class TowerBase : MonoBehaviour 
{
    public bool hasEnemy = false;
    public Vector3 enemyLocation;

    private GameObject enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enemy)
        {
            //Debug.Log(enemy.name);
            hasEnemy = true;
            enemyLocation = enemy.transform.position;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 15))
            /*if (Physics.Raycast(new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z), 
                                    Vector3.right, 
                                    out hit, 
                                    15))*/
            {
                if (hit.transform.tag == "Enemy")
                {
                    hasEnemy = true;
                    enemyLocation = hit.point;
                } else if (hit.transform.tag == "Tower")
                {
                    TowerBase towerBaseScript = hit.transform.gameObject.GetComponent<TowerBase>();
                    if (towerBaseScript.hasEnemy)
                    {
                        hasEnemy = true;
                        enemyLocation = towerBaseScript.enemyLocation;
                    } else
                    {
                        hasEnemy = false;
                    }
                } else
                {
                    hasEnemy = false;
                }
            } else
            {
                hasEnemy = false;
            }
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (hasEnemy == false
            && other.tag == "Enemy")
        {
            //hasEnemy = true;
            enemy = other.gameObject;
        }
    }
}
