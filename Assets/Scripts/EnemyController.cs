using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject target;

    void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        float distance = (transform.position.x - target.transform.position.x);
        //Debug.Log(distance);
        if(distance < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }else if(distance > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (Physics.gravity.y < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, 2, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, -2, transform.localScale.z);
        }
    }
}
