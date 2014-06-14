using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour 
{
	public float movementSpeed;
    public bool canMove = true;
	
	// Update is called once per frame
	void Update () 
	{
        if(canMove)
        {
            transform.Translate (Vector3.left * movementSpeed * Time.deltaTime);
        }
	}
}
