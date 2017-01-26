using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollerController : MonoBehaviour {

    public GameObject rollerInventory;
    private Vector3 replacement;
    private float rollTimer = 0f;
    public float timeToRoll = 3.0f;
    public bool canInteract = false;
    public GameObject thick;
    public GameObject thin;
    public GameObject handTossed;
    public Collision other;

    private void Update()
    {
        if (canInteract)
        {
            roll();
            
        }
        pickup();
    }
    public void roll()
    {
        if(Input.GetKey(KeyCode.R))
        {
            rollTimer += Time.deltaTime;
            Debug.Log(rollTimer);
            if (timeToRoll < rollTimer)
            {
                Debug.Log("Rolled!");
                GameObject newTossed = Instantiate(handTossed,rollerInventory.transform.position,rollerInventory.transform.rotation);
                Destroy(rollerInventory);
                rollerInventory = newTossed;
                canInteract = false;
            }
        }
    }
    public void pickup()
    {
        if(rollerInventory != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(other.gameObject.tag == "Player")
                {
                    other.gameObject.GetComponent<playerInventory>().pickup();
                    rollerInventory = null;
                }
                
            }
        }
        
    }
    public void OnCollisionEnter(Collision col)
    {
        other = col;
        Debug.Log("Colliding!");
        if (rollerInventory == null)
        {
            
        }else if (col.gameObject.tag == "Player" && rollerInventory.tag == "rollableIngredient")
        {
            canInteract = true;
            Debug.Log("Ready to Roll!");
            //transform the rollable into its second form. could make changes based on input.
            //ex: push X for 1 sec, make thick crust, but press for 3 sec to make thin.
        }
        //insert other stations here, seems like this would be the solution.
    }

    public void OnCollisionExit(Collision col)
    {
        canInteract = false;
    }
}
