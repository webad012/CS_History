using UnityEngine;
using System.Collections;

public class TileTaken : MonoBehaviour 
{
    public bool isTaken = false;
    public GameObject tower;

    void Update()
    {
        if(isTaken = true && tower == null)
        {
            isTaken = false;
        }
    }
}
