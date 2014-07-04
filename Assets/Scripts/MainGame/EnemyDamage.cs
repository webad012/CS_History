using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
    public AudioClip dealDamageSound;

    private EnemyMove enemyMoveScript;
    public float damage;
    public float cooldown;
    private float cd;

    private EnemyAnimation enemyAnimationScript;

    //private bool triggeredTower = false;
    private GameObject towerObject;

	// Use this for initialization
	void Start () 
    {
        //cd = cooldown;
        cd = 0;
        enemyMoveScript = gameObject.GetComponent<EnemyMove>();
        enemyAnimationScript = gameObject.GetComponent<EnemyAnimation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*if (cd > 0)
        {
            cd -= Time.deltaTime;
        }*/

        if (towerObject)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }

            if (cd <= 0)
            {
                RestartCooldown(1f);
                AudioSource.PlayClipAtPoint(dealDamageSound, gameObject.transform.position);
                enemyAnimationScript.PlayDamageAnimation();
                towerObject.GetComponent<Health>().TakeDamage(damage);
            }

            if(enemyMoveScript.canMove)
            {
                enemyMoveScript.canMove = false;
            }
        }
        else
        {
            if (cd > 0)
            {
                RestartCooldown(0.5f);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.6f))
            {
                if (hit.transform.tag == "Tower"
                    && hit.transform.gameObject.GetComponent<Health>().ready == true)
                {
                    //cd = cooldown/2;
                    towerObject = hit.transform.gameObject;
                } 
                else if (hit.transform.tag == "House")
                {
                    //lose game
                    GameObject.FindGameObjectWithTag("TowerDefenseController").GetComponent<TowerDefenseController>().gameLost = true;
                    //enemyMoveScript.canMove = false;
                } 
                else if(!enemyMoveScript.canMove)
                {
                    enemyMoveScript.canMove = true;
                }
            }
            else if(!enemyMoveScript.canMove)
            {
                enemyMoveScript.canMove = true;
            }
        }
        /*RaycastHit hit;
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
                    towerObject.GetComponent<Health>().TakeDamage(damage);
                    //Health healthScript = towerObject.GetComponent<Health>();
                    //healthScript.health -= damage;
                }
                enemyMoveScript.canMove = false;
            }
        } 
        else if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.6f))
             //|| Physics.Raycast(transform.position, Vector3.right, out hit, 0.5f))
        {
            if (hit.transform.tag == "Tower"
                && hit.transform.gameObject.GetComponent<Health>().ready == true)
            {
                if (cd <= 0)
                {
                    cd = cooldown;
                    hit.transform.gameObject.GetComponent<Health>().TakeDamage(damage);
                    //Health healthScript = hit.transform.gameObject.GetComponent<Health>();
                    //healthScript.health -= damage;
                }
                enemyMoveScript.canMove = false;
            } 
            else if (hit.transform.tag == "House")
            {
                //lose game
                GameObject.FindGameObjectWithTag("TowerDefenseController").GetComponent<TowerDefenseController>().gameLost = true;
                enemyMoveScript.canMove = false;
            } 
            else
            {
                enemyMoveScript.canMove = true;
            }
        }
        else if(!enemyMoveScript.canMove)
        {
            enemyMoveScript.canMove = true;
        }*/
	}

    void OnTriggerStay(Collider other)
    {
        if (towerObject == null
            && other.tag == "Tower"
            && other.gameObject.GetComponent<Health>().ready == true)
        {
            //cd = cooldown/2;
            //triggeredTower = true;
            towerObject = other.gameObject;
        }
    }

    void RestartCooldown(float multiplier)
    {
        cd = (cooldown * multiplier) + Random.Range(-0.1f, 0.1f);
    }
}
