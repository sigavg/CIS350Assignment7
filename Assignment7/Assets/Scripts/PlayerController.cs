/* Luke Lesimple
 * Assignment 7
 * Controls player movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody prb;
    public float speed = 5.0f;
    private float forwardInput;

    private GameObject focalpt;

    public bool hasPowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        prb = GetComponent<Rigidbody>();
        focalpt = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prb.AddForce(focalpt.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Debug.Log("Player collided with: " + collision.gameObject.name +
                " with powerup set to " + hasPowerUp);
        }
    }

}
