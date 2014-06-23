using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour 
{
    //public Animation moving;
    //public Animator damaging;
    public string movingAnimation;
    public string damagingAnimation;

    private bool movePlaying = false;
    private bool damagePlaying = false;

    private EnemyMove enemyMoveScript;
    private Animation animationScript;

	// Use this for initialization
	void Start () 
    {
        enemyMoveScript = gameObject.GetComponent<EnemyMove>();
        animationScript = gameObject.transform.Find("Body").GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enemyMoveScript.canMove)
        {
            if(!movePlaying)
            {
                movePlaying = true;
                damagePlaying = false;
                animationScript.Play(movingAnimation);
            }
        } 
        else
        {
            if(!damagePlaying)
            {
                damagePlaying = true;
                movePlaying = false;
                animationScript.Play(damagingAnimation);
            }
        }
	}
}
