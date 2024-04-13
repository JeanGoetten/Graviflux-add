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

    public AudioSource au;
    public AudioSource au2;
    public AudioClip[] clips; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravity = -60;
        horizontalInput = 0;

        au2.PlayOneShot(clips[2]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

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
    }
}
