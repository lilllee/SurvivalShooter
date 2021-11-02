using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public float speed = 15.0f;

    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            AudioManager.Instance.PlaySound("Explosion", transform.position);
            Destroy(gameObject);
        }
    }
}