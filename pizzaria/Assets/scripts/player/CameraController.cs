using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //Keeps track of how much movement the mouse has made
    Vector2 mouseLook;
    //Helps smooth movement of mouse
    Vector2 smoothV;
    //These variables help the player find a comfortable camera control
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;


    // Use this for initialization
    void Start () {
        //Sets this camera to be the child of the gameobject it is attatched to.
        offset = target.transform.position - transform.position;
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
        mouseLook.y = Mathf.Clamp(mouseLook.y, -15f, 15f);


        //add mouselooky to local Rotation on right axis or x axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //add mouselookx to local Rotation on up axis or y axis
        target.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, target.transform.up);
    }
    

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        //transform.LookAt(target.transform);
    }
}

