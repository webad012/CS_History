using UnityEngine;
using System.Collections;

public class BinaryNumberController : MonoBehaviour 
{
    public int orderNum;

    private bool isSelected = false;
    private int numValue;
    private GUIText numTextMain;
    private GUIText numTextShade;

    private bool showTooltip = false;
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;
    private string toolTipText;
        
    private StoryController sc;
    
	// Use this for initialization
	void Start ()
    {
        numTextMain = gameObject.GetComponent<GUIText>();
        numTextShade = gameObject.transform.Find("Shade").GetComponent<GUIText>();

        sc = GameObject.Find("StoryController").GetComponent<StoryController>();
        
        gameObject.name = "Number_" + orderNum.ToString();
        numValue = (int)Mathf.Pow(2, orderNum);
        SetTooltip();
    }

    void OnGUI()
    //void Update()
    {
        if (showTooltip)
        {
            //Debug.Log("asd");
            float x = Event.current.mousePosition.x;
            float y = Event.current.mousePosition.y;
            GUI.Label (new Rect (x-149,y+40,300,60), toolTipText, guiStyleBack);
            GUI.Label (new Rect (x-150,y+40,300,60), toolTipText, guiStyleFore);
        }
    }

    void OnMouseEnter ()
    {
        showTooltip = true;
    }
    
    void OnMouseExit ()
    {
        showTooltip = false;
    }
    
    void OnMouseDown()
    {
        string displayNumString;
        if (isSelected)
        {
            displayNumString = "0";
            isSelected = false;
            sc.AddToScore(-numValue);
        }
        else
        {
            displayNumString = "1";
            isSelected = true;
            sc.AddToScore(numValue);
        }

        numTextMain.text = displayNumString;
        numTextShade.text = displayNumString;
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
    
        toolTipText = numValue.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
    }
}
