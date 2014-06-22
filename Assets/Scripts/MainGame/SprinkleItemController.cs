using UnityEngine;
using System.Collections;

public class SprinkleItemController : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        gameObject.GetComponent<TweenPosition>().eventReceiver = gameObject;
        gameObject.GetComponent<TweenPosition>().callWhenFinished = "SprinkleFinished";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SprinkleFinished()
    {
        Destroy(gameObject);
    }
}
