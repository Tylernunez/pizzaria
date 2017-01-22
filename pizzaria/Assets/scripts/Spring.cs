using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

    private Rigidbody rb;
    public float force = 6.0f;
    

    void Start()
    {
        GameObject player = GameObject.Find("player");
        rb = player.GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            rb.AddForce(Vector3.up * force);
        }
    }

}
