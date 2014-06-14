using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float movementSpeed;
	public float damage;
	public Vector3 initPos;
    public GameObject explosion;

	void Start()
	{
		initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.right * movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initPos) > 20)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().health -= damage;
            //create particles
            Destroy(gameObject);
        }
    }
}
