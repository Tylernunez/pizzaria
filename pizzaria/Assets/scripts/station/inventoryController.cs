using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryController : MonoBehaviour {

    public GameObject inventory;
    public Transform inventoryParent;
    public bool canInteract = false;

    public Collider other;


    private void Update()
    {
            if (inventory != null && Input.GetKey(KeyCode.E))
            {
                pickup();
            }
    }
   
    public void pickup()
    {
                if(other.gameObject.tag == "Player")
                {
                    other.gameObject.GetComponent<playerInventory>().pickup();
                    inventory = null;
                }   
    }
    public void drop()
    {            
        inventory = other.gameObject.GetComponent<playerInventory>().inventory;
        inventory.transform.parent = this.transform;
        inventoryParent = this.transform;
        inventory.transform.position = new Vector3(inventoryParent.position.x, inventoryParent.position.y + 1, inventoryParent.position.z + 1);
        inventory.transform.rotation = inventoryParent.rotation;
        inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

    }
    public void OnTriggerEnter(Collider col)
    {
        other = col;
    }

    public void OnTriggerExit(Collider col)
    {
        other = null;
    }
}
