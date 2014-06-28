using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
    private EnemyMove enemyMoveScript;
    public float damage;
    public float cooldown;
    private float cd;

    private bool triggeredTower = false;
    private GameObject towerObject;

	// Use this for initialization
	void Start () 
    {
        cd = cooldown;
        enemyMoveScript = gameObject.GetComponent<EnemyMove>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }

        
        RaycastHit hit;
        if (triggeredTower)
        {
            if(towerObject == null)
            {
                triggeredTower = false;
                enemyMoveScript.canMove = true;
            }
            else
            {
                if (cd <= 0)
                {
                    cd = cooldown;
                    Health healthScript = towerObject.GetComponent<Health>();
                    healthScript.health -= damage;
                }
                enemyMoveScript.canMove = false;
            }
        } 
        else if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.6f))
        {
            if (hit.transform.tag == "Tower")
            {
                if (cd <= 0)
                {
                    cd = cooldown;
                    Health healthScript = hit.transform.gameObject.GetComponent<Health>();
                    healthScript.health -= damage;
                }
                enemyMoveScript.canMove = false;
            } 
            else if (hit.transform.tag == "House")
            {
                //lose game
                GameObject.FindGameObjectWithTag("TowerDefenseController").GetComponent<TowerDefenseController>().gameLost = true;
                enemyMoveScript.canMove = false;
            } else
            {
                enemyMoveScript.canMove = true;
            }
        }
        else if(!enemyMoveScript.canMove)
        {
            enemyMoveScript.canMove = true;
        }
	}

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Tower")
        {
            triggeredTower = true;
            towerObject = other.gameObject;
            //Debug.Log(other.tag);
        }
    }
}
