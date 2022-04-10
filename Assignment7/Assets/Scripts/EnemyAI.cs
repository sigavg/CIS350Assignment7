/* Luke Lesimple
 * Assignment 7
 * enemy movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody erb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        erb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 lookdir = (player.transform.position - transform.position).normalized;
        erb.AddForce(lookdir * speed);  
    }
    
    
}
