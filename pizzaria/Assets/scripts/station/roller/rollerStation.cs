using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollerStation : MonoBehaviour {

    inventoryController inventoryController;
    public GameObject thick;
    public GameObject thin;
    public GameObject handTossed;
    public float rollTimer = 0f;
    public float timeToRoll = 3.0f;

    private void Awake()
    {
        inventoryController = this.GetComponent<inventoryController>();

    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R))
        {
            roll();
        }

    }
    public void roll()
    {
        foreach (Transform child in inventoryController.inventory.transform) if (child.CompareTag("rollableIngredient")) 
        {
            rollTimer += Time.deltaTime;
            Debug.Log(rollTimer);
            if (timeToRoll < rollTimer)
            {
                Debug.Log("Rolled!");
                GameObject newTossed = Instantiate(handTossed, inventoryController.inventory.transform.position, inventoryController.inventory.transform.rotation);
                Destroy(inventoryController.inventory);
                inventoryController.inventory = newTossed;
                inventoryController.canInteract = false;
                inventoryController.inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

    }
}
