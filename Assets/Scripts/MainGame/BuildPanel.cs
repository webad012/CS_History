using UnityEngine;
using System.Collections;

public class BuildPanel : MonoBehaviour 
{
    public bool buildPanelOpen = false;
    public TweenPosition buildPanelTweener;
    public TweenRotation buildPanelArrowTweener;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ToggleBuildPanel()
    {
        bool param;
        if (buildPanelOpen) 
        {
            param = false;
        } 
        else 
        {
            param = true;
        }
        
        /*foreach (Transform thePlane in placementPlanesRoot) 
        {
            thePlane.gameObject.renderer.enabled = param;
        }*/
        
        buildPanelTweener.Play(param);
        
        buildPanelArrowTweener.Play(param);
        
        buildPanelOpen = param;
    }
}
