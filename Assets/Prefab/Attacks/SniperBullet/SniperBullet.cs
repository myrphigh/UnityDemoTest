using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float Damage = 3f;
    public Rigidbody Rigidbody;
    Vector3 target;
    private void Start()
    {
        
        Transform t = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 temp = t.GetChild(1).transform.position;
        temp.y += 1f;
        target = (temp - transform.position).normalized;
        Debug.Log("x: " + target.x + "y: " + target.y + "z: " + target.z);
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = target * 13f;
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        //Rigidbody.MovePosition(transform.position + target * Time.deltaTime * 13f);
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.playerHealth -= Damage;
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
