using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public float health;
    public GameObject bloodPrefab;

    public void TakeDamage(float damage)
    {
        //Debug.Log("asd1");
        health -= damage;
        //Debug.Log("asd2");
        Instantiate(bloodPrefab, gameObject.transform.position, Quaternion.identity);
    }
}