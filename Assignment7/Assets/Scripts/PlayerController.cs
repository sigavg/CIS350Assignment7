/* Luke Lesimple
 * Assignment 7
 * Controls player movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody prb;
    public float speed = 5.0f;
    private float forwardInput;

    private GameObject focalpt;

    public bool hasPowerUp = false;
    private float pwrupstr = 15.0f;

    public GameObject powerupIndicator;
    public Text losescreen;

    // Start is called before the first frame update
    void Start()
    {
        losescreen.enabled = false;
        prb = GetComponent<Rigidbody>();
        focalpt = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -10 && !SpawnManager.win)
        {
            losescreen.enabled = true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Prototype 4");
            }
        }

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
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Debug.Log("Player collided with: " + collision.gameObject.name +
                " with powerup set to " + hasPowerUp);

            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 away = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRigidBody.AddForce(away * pwrupstr, ForceMode.Impulse);
        }
    }

}
