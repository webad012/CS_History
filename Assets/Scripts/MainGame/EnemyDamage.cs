using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
    private EnemyMove enemyMoveScript;
    public float damage;
    public float cooldown;
    private float cd;

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
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.6f))
        {
            if (hit.transform.tag == "Tower")
            {
                if(cd <= 0)
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
                GameObject.Find("GameLogic").GetComponent<LooseGame>().lost = true;
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
        }
	}
}
