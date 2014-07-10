using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour 
{
    public string movingAnimation;
    public string damagingAnimation;

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
            if (!animationScript.isPlaying)
            {
                if (damagePlaying)
                {
                    damagePlaying = false;
                }
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
        animationScript.wrapMode = WrapMode.Once;
        animationScript.Play(damagingAnimation);
    }
}
