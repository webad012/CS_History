using UnityEngine;
using System.Collections;

public class UpgradeButtonController : MonoBehaviour 
{
    public GameObject target;
    public string onHoverFunction;
    public string offHoverFunction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*if (!gameObject.GetComponent<UIButton>().isEnabled)
        {
            gameObject.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade";
        }*/
	}

    void OnHover(bool isOver)
    {
        if (isOver)
        {
            target.SendMessage(onHoverFunction, gameObject);
        } 
        else
        {
            target.SendMessage(offHoverFunction, gameObject);
            //gameObject.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade";
        }
    }
}
