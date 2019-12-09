using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifetime = 5;
    public GameObject explosion;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward, Space.World);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")&& other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            if (other.gameObject.GetComponent<EnemyAI>()!=null && other.gameObject.GetComponent<EnemyAI>().GetType().BaseType.ToString() == "EnemyAI")
            {
                other.gameObject.GetComponent<Health>().TakeDamage(25);
            }
        }

    }
}
