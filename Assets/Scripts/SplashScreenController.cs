using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour 
{
    public float maxSplashTime;
    public float scaleSpeed;
    public Vector3 minScale;
    public Vector3 maxScale;

	// Use this for initialization
	void Start () 
    {
        transform.localScale = minScale;
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.localScale = Vector3.Lerp (transform.localScale, maxScale, scaleSpeed * Time.deltaTime);

        if (maxSplashTime > 0)
        {
            maxSplashTime -= Time.deltaTime;
        }

        if (Input.anyKeyDown || maxSplashTime <= 0)
        {
            Application.LoadLevel("FirstScene");
        }
	}
}
