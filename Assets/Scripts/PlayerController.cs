using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float gravity; 
    private Rigidbody rb;
    private float horizontalInput;

    private float aceleration; 

    public AudioSource au;
    public AudioSource au2;
    public AudioClip[] clips;

    public GameObject portal_1;
    public GameObject portal_2;

    public float cdPortal = 1f;
    private float timeRec;

    private bool onTravel;

    private float portalForceIncrease; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravity = -60;
        horizontalInput = 0;

        au2.PlayOneShot(clips[2]);

        aceleration = 0;

        timeRec = 0;

        onTravel = false;

        portalForceIncrease = 1;
    }

    private void Update()
    {
        timeRec += Time.deltaTime; 

        if (Input.GetKeyDown(KeyCode.Space) && !onTravel)
        {
            au.PlayOneShot(clips[0]);
            gravity *= -1;
            Physics.gravity = new Vector3(0, gravity, 0);
            GameManager.spacePressedCounter++;
        }

        if (gravity > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -2f, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 2f, transform.localScale.z);
        }

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        if (transform.position.y > 18.5)
        {
            transform.position = new Vector3(transform.position.x, -8.5f, transform.position.z); 
        }else if(transform.position.y < -8.5)
        {
            transform.position = new Vector3(transform.position.x, 18.5f, transform.position.z);
        }

        Debug.Log(aceleration); 
    }

    void FixedUpdate()
    {
        if(onTravel)
        {
            
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        

    }

    public void Reset()
    {
        gravity = -60;
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            au.PlayOneShot(clips[1]);
            Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.gameObject.tag == "Ground")
        {
            aceleration = 0f;
        onTravel = false;
        portalForceIncrease = 0f; 
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal_1")
        {
            if (timeRec > cdPortal)
            {
                onTravel = true;
                timeRec = 0;
                portalForceIncrease++;
                aceleration += (rb.velocity.magnitude / 5) * portalForceIncrease;
                transform.position = new Vector2(portal_2.transform.position.x + 1.5f, portal_2.transform.position.y + 1);
                rb.velocity = new Vector2(aceleration, aceleration);
                portalForceIncrease += 0.5f; 
            }
            
            
        }
        if (other.gameObject.tag == "Portal_2")
        {
            if (timeRec > cdPortal)
            {
                onTravel = true;
                timeRec = 0;
                portalForceIncrease++;
                aceleration += (rb.velocity.magnitude/5) * portalForceIncrease;
                transform.position = new Vector2(portal_1.transform.position.x + 1.5f, portal_1.transform.position.y + 1);
                rb.velocity = new Vector2(aceleration, aceleration);
                
            }
        }
    }
}
