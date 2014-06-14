using UnityEngine;
using System.Collections;

public class TowerBase : MonoBehaviour 
{
    public bool hasEnemy = false;
    public Vector3 enemyLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right, out hit, 15))
        {
            if (hit.transform.tag == "Enemy")
            {
                hasEnemy = true;
                enemyLocation = hit.point;
            } 
            else if(hit.transform.tag == "Tower")
            {
                TowerBase towerBaseScript = hit.transform.gameObject.GetComponent<TowerBase>();
                if(towerBaseScript.hasEnemy)
                {
                    hasEnemy = true;
                    enemyLocation = towerBaseScript.enemyLocation;
                }
                else
                {
                    hasEnemy = false;
                }
            }
            else
            {
                hasEnemy = false;
            }
        } 
        else
        {
            hasEnemy = false;
        }
	}
}
