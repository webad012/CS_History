using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour 
{
    //public Animation moving;
    //public Animator damaging;
    public string movingAnimation;
    public string damagingAnimation;

    //private bool movePlaying = false;
    public bool damagePlaying = false;

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
            //if(!movePlaying)
            //if(!animationScript.IsPlaying(movingAnimation) && !animationScript.IsPlaying(damagingAnimation))
            if (!animationScript.isPlaying)
            {
                if (damagePlaying)
                {
                    damagePlaying = false;
                }
                //movePlaying = true;
                //damagePlaying = false;
                animationScript.wrapMode = WrapMode.Loop;
                animationScript.Play(movingAnimation);
            }
        }
        else
        {
            if(animationScript.IsPlaying(movingAnimation))
            {
                animationScript.Stop(movingAnimation);
            }
        }
        /*else
        {
            if(!damagePlaying)
            {
                damagePlaying = true;
                movePlaying = false;
                animationScript.Play(damagingAnimation);
            }
        }*/
	}

    public void PlayDamageAnimation()
    {
        //if(!animationScript.IsPlaying(damagingAnimation))
        //{
        animationScript.wrapMode = WrapMode.Once;
        animationScript.Play(damagingAnimation);
        //}
    }
}
