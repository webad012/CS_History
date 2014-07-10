using UnityEngine;
using System.Collections;
using System;

public class BallController : MonoBehaviour 
{
    public bool isSelected = false;
    private int ball_value;

    private Color defaultColor = new Color32(40, 115, 120, 255);

    private float tweenSpeed = 0.25f;
    private float onesTweenX = 0.13f;
    private float fivesTweenX = -0.12f;

    private StoryController sc;

	// Use this for initialization
	void Start () 
    {
        string pipe_num = transform.parent.transform.parent.name.Substring(5);
        
        ball_value = (int)Mathf.Pow(10, Convert.ToInt32(pipe_num));
        
        if(transform.parent.name.Equals("Fives"))
        {
            ball_value *= 5;
        }

        renderer.material.color = defaultColor;
        sc = GameObject.Find("StoryController").GetComponent<StoryController>();
	}

    public void SelectBall()
    {
        string ball_num_string = transform.name.Substring(4);
        int ball_num_int = Convert.ToInt32(ball_num_string);

        if (isSelected)
        {
            foreach(Transform sibling in transform.parent.transform)
            {
                string sibling_num = sibling.name.Substring(4);
                if(Convert.ToInt32(sibling_num) > Convert.ToInt32(ball_num_int)
                   && sibling.gameObject.GetComponent<BallController>().isSelected)
                {
                    sibling.gameObject.GetComponent<BallController>().SelectBall();
                }
            }

            Vector3 tween_vec;
            
            if(transform.parent.name.Equals("Ones"))
            {
                tween_vec = new Vector3(transform.position.x + onesTweenX, 
                                        transform.position.y, 
                                        transform.position.z);
            }
            else
            {
                tween_vec = new Vector3(transform.position.x + fivesTweenX, 
                                           transform.position.y, 
                                           transform.position.z);
            }

            TweenPosition.Begin(gameObject, tweenSpeed, tween_vec);

            
            isSelected = false;
            sc.AddToScore(-ball_value);
        } 
        else
        {
            foreach(Transform sibling in transform.parent.transform)
            {
                string sibling_num = sibling.name.Substring(4);
                if(Convert.ToInt32(sibling_num) < Convert.ToInt32(ball_num_int)
                   && !sibling.gameObject.GetComponent<BallController>().isSelected)
                {
                    sibling.gameObject.GetComponent<BallController>().SelectBall();
                }
            }

            Vector3 tween_vec;

            if(transform.parent.name.Equals("Ones"))
            {
                tween_vec = new Vector3(transform.position.x - onesTweenX, transform.position.y, transform.position.z);
            }
            else
            {
                tween_vec = new Vector3(transform.position.x - fivesTweenX, transform.position.y, transform.position.z);
            }

            TweenPosition.Begin(gameObject, tweenSpeed, tween_vec);
            
            isSelected = true;
            sc.AddToScore(ball_value);
        }
    }
    
    void OnMouseDown()
    {
        SelectBall();
    }
}
