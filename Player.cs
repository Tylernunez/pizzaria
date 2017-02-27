using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Public variables that will show up in the Editor
	public float Acceleration = 50f;
	public float MaxSpeed = 20f;
	public float JumpStrength = 500f;

	// Private variables.  These will not be accessible from any other class.
	private bool _onGround = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Get the player's input axes
		float xSpeed = Input.GetAxis("Horizontal");
		float zSpeed = Input.GetAxis("Vertical");
		// Get the movement vector
		Vector3 velocityAxis = new Vector3(xSpeed, 0, zSpeed);
		// Rotate the movement vector based on the camera
		velocityAxis = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y,Vector3.up) * velocityAxis;

		// Rotate the player's model to show direction
		if (velocityAxis.magnitude > 0) {
			transform.rotation = Quaternion.LookRotation(velocityAxis);
		}

		// Move the player
		GetComponent<Rigidbody>().AddForce(velocityAxis.normalized * Acceleration);

		// Check to see if the player is on the ground.
		_onGround = CheckGroundCollision();


		// Check the player's input
		if (Input.GetButtonDown("Jump")){
			Jump();
		}

		LimitVelocity();
	}

	/// <summary>
	/// Keeps the player's velocity limited so it will not go too fast.
	/// </summary>
	private void LimitVelocity() {
		Vector2 xzVel = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z);
		if (xzVel.magnitude > MaxSpeed) {
			xzVel = xzVel.normalized * MaxSpeed;
			GetComponent<Rigidbody>().velocity = new Vector3(xzVel.x, GetComponent<Rigidbody>().velocity.y, xzVel.y);
		}
	}

	/// <summary>
	/// Checks to see if the player is on the ground.
	/// </summary>
	/// <returns><c>true</c>, if the player is touching the ground, <c>false</c> otherwise.</returns>
	private bool CheckGroundCollision(){
		// We can use a layer mask to tell the Physics Raycast which layers we are trying to hit.
		// This will allow us to restrict which objects this applies to.
		int layerMask = 1 << LayerMask.NameToLayer("Collision");

		// We will get the bounds of the MeshFilter (our player's sphere) so we can
		// get the coordinates of where the bottom is.
		Bounds meshBounds = GetComponent<MeshFilter>().mesh.bounds;

		// We will use a Physics.Raycast to see if there is anything on the ground below the player.
		// We can limit the distance to make sure that we are touching the bottom of the collider.
		if (Physics.Raycast(transform.position+meshBounds.center,Vector3.down,meshBounds.extents.y,layerMask)){
			return true;
		}
		return false;
	}

	/// <summary>
	/// Applies force to the player's rigidbody to make him jump.
	/// </summary>
	private void Jump(){
		if (_onGround){
			GetComponent<Rigidbody>().AddForce(new Vector3(0,JumpStrength,0));
		}
	}

}
