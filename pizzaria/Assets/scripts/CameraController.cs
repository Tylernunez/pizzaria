using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    //Keeps track of how much movement the mouse has made
    Vector2 mouseLook;
    //Helps smooth movement of mouse
    Vector2 smoothV;
    //These variables help the player find a comfortable camera control
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f; 


    // Use this for initialization
    void Start () {
        //Sets this camera to be the child of the gameobject it is attatched to.
        player = this.transform.parent.gameObject;
	}
    void Update()
    {
        //Takes Mouse Input or Mouse Delta
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //Mouse delta is mulitplied by smoothing and sensitivity values
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        //Linear interpretaion of movement 
        //forces camera to move smoothly between two points rather than having the camera move instantly to the new point
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        //add mouselooky to local Rotation on right axis or x axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //add mouselookx to local Rotation on up axis or y axis
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
	
}
