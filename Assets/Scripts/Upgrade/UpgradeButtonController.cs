using UnityEngine;
using System.Collections;

public class UpgradeButtonController : MonoBehaviour 
{
    public GameObject target;
    public string functionName;

    private bool isMouseOver = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (isMouseOver && collider.enabled)
        if (isMouseOver && gameObject.GetComponent<UIButton>().isEnabled)
        {
            target.SendMessage(functionName, gameObject);
        } 
        else
        {
            gameObject.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade";

        }
	}

    void OnHover(bool isOver)
    {
        //string labelText;
        if (isOver)
        {
            isMouseOver = true;
        } 
        else
        {
            isMouseOver = false;
        }
    }
}
