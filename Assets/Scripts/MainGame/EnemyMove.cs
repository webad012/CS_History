using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour 
{
	public float movementSpeed;
    public bool canMove = true;

    private EnemyAnimation enemyAnimationScript;

    void Start()
    {
        transform.rotation = Quaternion.identity;

        enemyAnimationScript = gameObject.GetComponent<EnemyAnimation>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        if(canMove && !enemyAnimationScript.damagePlaying)
        {
            transform.Translate (Vector3.left * movementSpeed * Time.deltaTime);
        }
	}
}
