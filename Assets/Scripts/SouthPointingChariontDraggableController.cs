using UnityEngine;
using System.Collections;

public class SouthPointingChariontDraggableController : MonoBehaviour 
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float minY;
    private float maxY;

    private SouthPointingChariotMinigameController spcmc;

	// Use this for initialization
	void Start () 
    {
        spcmc = GameObject.FindGameObjectWithTag("MiniGameMainObject").GetComponent<SouthPointingChariotMinigameController>();

        minY = spcmc.dragMinMaxY.x;
        maxY = spcmc.dragMinMaxY.y;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        Vector3 tmp = new Vector3(screenPoint.x,
                                  Input.mousePosition.y, 
                                  screenPoint.z);
        
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(tmp);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(screenPoint.x, 
                                             Input.mousePosition.y, 
                                             screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        transform.position = curPosition;

        Vector3 locPos = transform.localPosition;
        locPos.y = Mathf.Clamp(locPos.y, minY, maxY);
        transform.localPosition = locPos;

        if (spcmc == null)
        {
            spcmc = GameObject.FindGameObjectWithTag("MiniGameMainObject").GetComponent<SouthPointingChariotMinigameController>();
        }
        spcmc.Dragged(transform.parent.name, locPos.y);
    }
}
