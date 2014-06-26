using UnityEngine;
using System.Collections;

public class ProjectileBinaryController : Projectile 
{
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Find("One").gameObject.transform.Rotate (0,0,-100*Time.deltaTime);
        transform.Find("Zero").gameObject.transform.Rotate (0,0,-100*Time.deltaTime);
	}
}
