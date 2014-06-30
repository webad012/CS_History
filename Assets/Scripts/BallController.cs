using UnityEngine;
using System.Collections;
using System;

public class BallController : MonoBehaviour 
{
    public bool isSelected = false;
    private int ball_value;

    private Color defaultColor = new Color32(40, 115, 120, 255);
    //private Color mouseOverColor = new Color32(130, 130, 40, 255);

    private float tweenSpeed = 0.25f;
    private float onesTweenX = 0.13f;
    private float fivesTweenX = -0.12f;

    private StoryController sc;

    //private string currentToolTipText = "";
   // private GUIStyle guiStyleFore;
    //private GUIStyle guiStyleBack;
    //private bool showTooltip = false;

	// Use this for initialization
	void Start () 
    {
        //SetTooltip();

        string pipe_num = transform.parent.transform.parent.name.Substring(5);
        
        ball_value = (int)Mathf.Pow(10, Convert.ToInt32(pipe_num));
        
        if(transform.parent.name.Equals("Fives"))
        {
            ball_value *= 5;
        }

        renderer.material.color = defaultColor;
        sc = GameObject.Find("StoryController").GetComponent<StoryController>();
	}

    /*void OnGUI()
    {
        if (showTooltip)
        {
            float x = Event.current.mousePosition.x;
            float y = Event.current.mousePosition.y;
            GUI.Label (new Rect (x-149,y+40,300,60), currentToolTipText, guiStyleBack);
            GUI.Label (new Rect (x-150,y+40,300,60), currentToolTipText, guiStyleFore);
        }
    }*/

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

    /*void OnMouseEnter ()
    {
        renderer.material.color = mouseOverColor;
        showTooltip = true;
    }
    
    void OnMouseExit ()
    {
        renderer.material.color = defaultColor;
        showTooltip = false;
    }

    void SetTooltip()
    {
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;  
        guiStyleFore.alignment = TextAnchor.UpperCenter ;
        guiStyleFore.wordWrap = true;
        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;  
        guiStyleBack.alignment = TextAnchor.UpperCenter ;
        guiStyleBack.wordWrap = true;

        string pipe_num = transform.parent.transform.parent.name.Substring(5);

        ball_value = (int)Mathf.Pow(10, Convert.ToInt32(pipe_num));
        
        if(transform.parent.name.Equals("Fives"))
        {
            ball_value *= 5;
        }
        
        currentToolTipText = ball_value.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
    }*/
}
