using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 6.0F;
    public bool sprint = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical") * speed;
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        verticalMovement *= Time.deltaTime;
        horizontalMovement *= Time.deltaTime;

        transform.Translate(horizontalMovement, 0, verticalMovement);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (sprint == true)
        {
            speed = 12.0F;
        }
        else
        {
            speed = 6.0F;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }

            
    }
}

