using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour 
{
    public bool isSelected = false;

    private Color defaultColor = new Color32(40, 115, 120, 255);
    private Color mouseOverColor = new Color32(130, 130, 40, 255);

    private MiniGameAbacusController mgac;

	// Use this for initialization
	void Start () 
    {
        renderer.material.color = defaultColor;
        mgac = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<MiniGameAbacusController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
    }
    
    void OnMouseDown()
    {
        mgac.BallSelected(gameObject);
    }

    void OnMouseEnter ()
    {
        renderer.material.color = mouseOverColor;
    }
    
    void OnMouseExit ()
    {
        renderer.material.color = defaultColor;
    }
}
