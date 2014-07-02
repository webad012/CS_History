using UnityEngine;
using System.Collections;

public class SouthPointingChariotMinigameController : MonoBehaviour 
{
    public Rect[] targetAreas;
    
    public Vector2 dragMinMaxY;

    private int valueLeft = 50;
    private int valueRight = 50;
    private float facingError = 10;

    private Transform body;
    private Transform target;
    private bool targetPositioned = false;

    private StoryController sc;
    
    // Use this for initialization
	void Start () 
    {
        sc = GameObject.FindGameObjectWithTag("StoryController").GetComponent<StoryController>();
            
        body = transform.Find("Body").gameObject.transform;
        target = transform.Find("Target").gameObject.transform;

        PositionTarget();
    }
    
	// Update is called once per frame
	void Update () 
    {
        if (targetPositioned)
        {
            if (Vector3.Angle(body.right, target.position - body.position) < facingError)
            {
                sc.AddToScore(1);
            }
        }
    }
    
    void OnGUI()
    {
    }

    void PositionTarget()
    {
        targetPositioned = false;
        while (!targetPositioned)
        {
            int rndInd = (int)Random.Range(0, targetAreas.Length);
            float rndX = Random.Range(targetAreas [rndInd].x, targetAreas [rndInd].x + targetAreas [rndInd].height);
            float rndZ = Random.Range(targetAreas [rndInd].y, targetAreas [rndInd].y + targetAreas [rndInd].width);

            Vector3 posVec = new Vector3(rndX, 0f, rndZ);
            target.localPosition = posVec;

            if (Vector3.Angle(body.right, target.position - body.position) > facingError)
            {
                targetPositioned = true;
            }
        }
    }
        
    public void Dragged(string left_right, float value)
    {
        float maxVal = dragMinMaxY.y - dragMinMaxY.x;

        float percent = (((value - maxVal) * 100)/maxVal) + 150;

        if (left_right == "Left")
        {
            valueLeft = (int)percent;
        }
        else if (left_right == "Right")
        {
            valueRight = (int)percent;
        }

        float tmp = (valueLeft - valueRight) * 180 / 100;

        if (body == null)
        {
            body = transform.Find("Body").gameObject.transform;
        }
        body.rotation = Quaternion.AngleAxis(tmp, Vector3.up);
    }
}
