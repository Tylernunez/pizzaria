﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    private Rigidbody rb;
    private float distToGround;
    public float acceleration = 50f;
    public float maxSpeed = 20f;
    public float jumpStrength = 500f;
    private bool OnGround = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //input
        float xspeed = Input.GetAxis("Horizontal");
        float zspeed = Input.GetAxis("Vertical");
        Vector3 velocityAxis = new Vector3(xspeed, 0, zspeed);
        //move
        rb.AddRelativeForce(velocityAxis.normalized * acceleration);


        OnGround = IsGrounded();
        //float verticalMovement = Input.GetAxis("Vertical") * speed;
        //float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        //verticalMovement *= Time.deltaTime;
        //horizontalMovement *= Time.deltaTime;

        //transform.Translate(horizontalMovement, 0, verticalMovement);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //jump
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        if (velocityAxis.magnitude != 0)
        {
            if (IsGrounded())
            {
                rb.drag = 2; // reduce velocity faster with myDrag rigidbody.angularVelocity = myDrag;
            }
        }
        if (!IsGrounded())
        {
            rb.drag = 1; // reduce velocity faster with myDrag rigidbody.angularVelocity = myDrag;
        }

        LimitVelocity();

    }

    private void LimitVelocity()
    {
        Vector2 xzVel = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z);
        if (xzVel.magnitude > maxSpeed)
        {
            xzVel = xzVel.normalized * maxSpeed;
            rb.velocity = new Vector3(xzVel.x, rb.velocity.y, xzVel.y);
        }
    }

    public bool IsGrounded()
    {
        // We can use a layer mask to tell the Physics Raycast which layers we are trying to hit.
        // This will allow us to restrict which objects this applies to.
        int layerMask = 1 << LayerMask.NameToLayer("Collision");

        // We will get the bounds of the MeshFilter (our player's sphere) so we can
        // get the coordinates of where the bottom is.
        Bounds meshBounds = GetComponent<MeshFilter>().mesh.bounds;

        // We will use a Physics.Raycast to see if there is anything on the ground below the player.
        // We can limit the distance to make sure that we are touching the bottom of the collider.
        if (Physics.Raycast(transform.position + meshBounds.center, Vector3.down, meshBounds.extents.y, layerMask))
        {
            return true;
        }
        return false;
    }

    public void Jump()
    {
        if (OnGround)
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0));
        }
    }
}
