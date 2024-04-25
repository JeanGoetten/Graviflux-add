using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnd : MonoBehaviour
{
    public GameObject go_explosion;
    public GameObject go_realEnd;
    public Transform exp_position; 

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Instantiate(go_explosion);
            Instantiate(go_realEnd);
            Destroy(this.gameObject);
            
        }
    }

    IEnumerator CD()
    {
        yield return new WaitForSeconds(1);
        
    }
}
