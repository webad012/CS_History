using UnityEngine;
using System.Collections;
using System;

/*[System.Serializable]
public class Pipe
{
    public GameObject ones_1;
    public GameObject ones_2;
    public GameObject ones_3;
    public GameObject ones_4;
    public GameObject ones_5;
    public GameObject fives_1;
    public GameObject fives_2;
}*/

public class MiniGameAbacusController : MonoBehaviour 
{
    public float tweenSpeed = 0.5f;

    private float onesTweenX = 0.13f;
    private float fivesTweenX = -0.12f;

    private StoryController sc;

	// Use this for initialization
	void Start () 
    {
        sc = GameObject.Find("StoryController").GetComponent<StoryController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BallSelected(GameObject go)
    {
        string pipe_num = go.transform.parent.transform.parent.name.Substring(5);
        string ball_num_string = go.transform.name.Substring(4);
        int ball_num_int = Convert.ToInt32(ball_num_string);

        BallController bc = go.GetComponent<BallController>();

        if (bc.isSelected)
        {
            foreach(Transform sibling in go.transform.parent.transform)
            {
                string sibling_num = sibling.name.Substring(4);
                if(Convert.ToInt32(sibling_num) > Convert.ToInt32(ball_num_int)
                   && sibling.gameObject.GetComponent<BallController>().isSelected)
                {
                    BallSelected(sibling.gameObject);
                }
            }
            float toAdd = Mathf.Pow(10, Convert.ToInt32(pipe_num));

            if(go.transform.parent.name.Equals("Ones"))
            {
                TweenPosition.Begin(go, tweenSpeed, new Vector3(go.transform.position.x + onesTweenX, go.transform.position.y, go.transform.position.z));
            }
            else
            {
                TweenPosition.Begin(go, tweenSpeed, new Vector3(go.transform.position.x + fivesTweenX, go.transform.position.y, go.transform.position.z));
                toAdd *= 5;
            }

            bc.isSelected = false;
            sc.AddToScore(-toAdd);
        } 
        else
        {
            foreach(Transform sibling in go.transform.parent.transform)
            {
                string sibling_num = sibling.name.Substring(4);
                if(Convert.ToInt32(sibling_num) < Convert.ToInt32(ball_num_int)
                   && !sibling.gameObject.GetComponent<BallController>().isSelected)
                {
                    BallSelected(sibling.gameObject);
                }
            }

            float toAdd = Mathf.Pow(10, Convert.ToInt32(pipe_num));

            if(go.transform.parent.name.Equals("Ones"))
            {
                TweenPosition.Begin(go, tweenSpeed, new Vector3(go.transform.position.x - onesTweenX, 
                                                          go.transform.position.y, go.transform.position.z));
            }
            else
            {
                TweenPosition.Begin(go, tweenSpeed, new Vector3(go.transform.position.x - fivesTweenX, 
                                                          go.transform.position.y, go.transform.position.z));

                toAdd *= 5;
            }
            
            bc.isSelected = true;
            sc.AddToScore(toAdd);
        }
    }
}
