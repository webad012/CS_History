using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public string projectileName;
	public float movementSpeed;
	public float damage;
    public GameObject explosion;

    private Vector3 initPos;
    private bool isBinaryProjectile = false;

	void Start()
	{
		initPos = transform.position;

        if (projectileName == "Binary")
        {
            isBinaryProjectile = true;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.right * movementSpeed * Time.deltaTime);

        if(isBinaryProjectile)
        {
            transform.Find("One").gameObject.transform.Rotate (0,0,-200*Time.deltaTime);
            transform.Find("Zero").gameObject.transform.Rotate (0,0,-200*Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, initPos) > 20)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
