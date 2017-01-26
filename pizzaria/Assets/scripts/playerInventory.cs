using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour {

    public GameObject inventory;
    public bool isHolding = false;
    public float grabRange = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isHolding)
        {
            pickup();
        }

        if (isHolding)
        {
            drop();
        }
         
	}
    public void pickup()
    {
        GameObject[] closeItem = GameObject.FindGameObjectsWithTag("pickup");
        GameObject closestObject = null;
        foreach(GameObject g in closeItem)
        {
            if (!closestObject)
            {
                closestObject = g;
            }
            //compare distances
            if (Vector3.Distance(transform.position, g.transform.position) <= Vector3.Distance(transform.position, closestObject.transform.position))
            {
                closestObject = g;
            }

        }
        if(Vector3.Distance(transform.position, closestObject.transform.position) < grabRange)
        {
            Debug.Log("The closest item is: " + closestObject);
            Debug.Log("Ready to Pickup!");
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Pickup!");
                inventory = closestObject;
                inventory.transform.parent = this.transform;
                inventory.transform.position = this.transform.position + this.transform.TransformDirection(new Vector3(0, 0, 1));
                isHolding = true;
                //Rigidbody pickup = inventory.GetComponent<Rigidbody>();
                //pickup.freezeRotation = true;
                RigidbodyConstraints pickup = inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            }
        }
        
    }
    public void drop()
    {
        Debug.Log("Ready to Drop!");
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Drop!");
            inventory.transform.parent = null;
            RigidbodyConstraints pickup = inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            inventory = null;
            isHolding = false;
            
        }
    }
}
