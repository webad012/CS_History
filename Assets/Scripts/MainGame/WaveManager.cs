using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour 
{
    //public int numOut;
    public int initialPouse;
    public GameObject[] enemies;
    public float cooldown;
    private float cd;

	// Use this for initialization
	void Start () 
    {
        cd = cooldown * initialPouse;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (cd > 0)
        {
            cd -= Time.deltaTime;
        } 
        else
        {
            cd = cooldown;
            Vector3 pos = new Vector3(4f, 1f, Random.Range(-2, 3));
            int ind = Random.Range(0, enemies.Length);
            Instantiate(enemies[ind], pos, Quaternion.identity);
            //numOut++;
        }
	}

    public void resetCD()
    {
        cd = cooldown * initialPouse;
    }
}
