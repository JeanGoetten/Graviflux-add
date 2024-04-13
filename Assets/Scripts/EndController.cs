using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    public bool killEnemy = false; 
    public bool killedByEnemy = false;

    public string nextSceneName;

    public AudioSource au;
    public AudioClip[] clips;

    public void Reset()
    {
        Physics.gravity = new Vector3(0, -60, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            au.PlayOneShot(clips[0]);
            Reset();
            GameManager.level++;
            SceneManager.LoadScene(nextSceneName);
        }
        if(killEnemy)
        {
            if (other.gameObject.tag == "Enemy")
            {
                au.PlayOneShot(clips[2]);
                Destroy(other.gameObject);
            }
        }
        if (killedByEnemy)
        {
            if (other.gameObject.tag == "Enemy")
            {
                au.PlayOneShot(clips[1]);
                Reset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


    }
}
