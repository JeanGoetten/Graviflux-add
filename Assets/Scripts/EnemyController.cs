using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    public bool mobility;
    public bool follow;
    public bool getTheFlag;
    public bool dieByFlag;
    public bool portalCross; 

    public float speed; 

    private Rigidbody rb;

    private int directionX;

    public float cdPortal = 1f;
    private float timeRec;

    private bool onTravel;

    private float portalForceIncrease;

    private float aceleration;

    public GameObject portal_1;
    public GameObject portal_2;

    public GameObject goPwned;

    void Awake()
    {
        target = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody>();

        directionX = -1;

        aceleration = 0;

        timeRec = 0;

        onTravel = false;

        portalForceIncrease = 0f;

        portal_1 = GameObject.FindWithTag("Portal_1");
        portal_2 = GameObject.FindWithTag("Portal_2");

        goPwned.SetActive(false);
    }

    void Update()
    {
        timeRec += Time.deltaTime;
        float distance = (transform.position.x - target.transform.position.x);

        // patrol
        if (mobility)
        {
            rb.velocity = new Vector2(directionX * speed, rb.velocity.y);
        }
        else
        { // look at player
            if (distance < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); // look at

                if (follow)
                {
                    rb.velocity = new Vector2(1 * speed, rb.velocity.y);
                }
            }
            else if (distance > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); // look at

                if (follow)
                {
                    rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                }
            }
        }
        // gravitty
        if (Physics.gravity.y < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, 2, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, -2, transform.localScale.z);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall")
        {
            directionX = directionX * -1;
        }
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall")
        {
            aceleration = 0f;
            onTravel = false;
            portalForceIncrease = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (portalCross)
        {
            if (other.gameObject.tag == "Portal_1")
            {
                if (timeRec > cdPortal)
                {
                    onTravel = true;
                    timeRec = 0;
                    portalForceIncrease++;
                    aceleration += (rb.velocity.magnitude / 5) * portalForceIncrease;
                    transform.position = new Vector2(portal_2.transform.position.x + 1.5f, portal_2.transform.position.y + 1.5f);
                    rb.velocity = new Vector2(aceleration, aceleration);
                }
            }
            if (other.gameObject.tag == "Portal_2")
            {
                if (timeRec > cdPortal)
                {
                    onTravel = true;
                    timeRec = 0;
                    portalForceIncrease++;
                    aceleration += (rb.velocity.magnitude / 5) * portalForceIncrease;
                    transform.position = new Vector2(portal_1.transform.position.x + 1.5f, portal_1.transform.position.y + 1.5f);
                    rb.velocity = new Vector2(aceleration, aceleration);
                }
            }
        }
        
        if (other.gameObject.tag == "PwnedByPlayer")
        {
            goPwned.SetActive(true);
        }
    }
}
