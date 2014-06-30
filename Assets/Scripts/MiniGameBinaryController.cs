using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniGameBinaryController : MonoBehaviour 
{
    public GameObject numberPrefab;

    //private List<GameObject> numbers = new List<GameObject>();

	// Use this for initialization
	void Start () 
    {
        float posX = 0.8f;
        for (int i=0; i<10; i++)
        {
            GameObject numberObject = (GameObject)Instantiate(numberPrefab, new Vector3(posX, 0.5f, 0f), Quaternion.identity);
            numberObject.transform.parent = gameObject.transform;
            numberObject.GetComponent<BinaryNumberController>().orderNum = i;
            posX -= 0.07f;
        }
	}
}
