using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 6.0F;
    public float turnSpeed = 100.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical") * speed;
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        verticalMovement *= Time.deltaTime;
        horizontalMovement *= Time.deltaTime;

        transform.Translate(horizontalMovement, 0, verticalMovement);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);
    }
}

