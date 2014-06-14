using UnityEngine;
using System.Collections;

public class CatapultProjectile : MonoBehaviour 
{
    public float movementSpeed;
    public float damage;
    public Vector3 initPos;
    //public GameObject explosion;

    public Vector3 target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    //private Transform myTransform;
    
    void Start()
    {
        //Debug.Log(target);
        initPos = transform.position;

        //StartCoroutine(SimulateProjectile());
        rigidbody.velocity = BallisticVel(target, 45);

        //rigidbody.AddForce(Vector3.up * movementSpeed, ForceMode.Impulse);
        //rigidbody.AddForce(Vector3.up * movementSpeed);
        //rigidbody.velocity = new Vector3(-1, 1, 0) * movementSpeed;
    }
    
    // Update is called once per frame
    void Update () 
    {
        //transform.Translate (Vector3.right * movementSpeed * Time.deltaTime);
        
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

    Vector3 BallisticVel(Vector3 target, float angle) 
    {
        var dir = target - transform.position;  // get target direction
        var h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        var dist = dir.magnitude ;  // get horizontal distance
        var a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
        // calculate the velocity magnitude
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    /*IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);
        
        // Move projectile to the position of throwing object + add some offset if needed.
        transform.position = initPos + new Vector3(0, 0.0f, 0);
        
        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, target);
        
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        
        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        
        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
        
        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(target - transform.position);
        
        float elapse_time = 0;
        
        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            
            elapse_time += Time.deltaTime;
            
            yield return null;
        }
    }*/
}
