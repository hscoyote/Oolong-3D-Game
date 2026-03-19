using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitCamera : MonoBehaviour
{

    //Serialized reference to the object to orbit around
    [SerializeField] private Transform target;
    public float rotSpeed = 1.5f;

    //Private variable with reference to camera
    private Camera cam;

    //for camera zoom
    [SerializeField] private float zoomFOV;
    [SerializeField] private float normalFOV;
    [SerializeField] private float zoomSpeed;

    private float _rotY;
    private float _rotX;
    private Vector3 _offset;

    ///limits vertical rotation of camera
    public float minimumVert = 45f;

    public float maximumVert = 45f;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.x;

        //store the starting position offset between the camera and target
        _offset = target.position - transform.position;
        
    }

    void Update()
    {
        float targetFOV;

        ///changes FOV if right mouse button is pressed and camera speed
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targetFOV = zoomFOV;
            //_rotY += Input.GetAxis("Mouse X") - 3;
            
        } else
        {
            targetFOV = normalFOV;

        }

        cam.fieldOfView = targetFOV;
    }

    //Late update
    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        //either rotates cmaera slowly using arrow keys, or rotate quickly with the mouse
        if (horInput != 0)
        {
            _rotY += horInput * rotSpeed;
        } else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        if (verInput != 0)
        {
            _rotX -= Input.GetAxis("Mouse Y") * 3;
            _rotX = Mathf.Clamp(_rotX,minimumVert, maximumVert);
            
        } else{
            _rotX -= Input.GetAxis("Mouse Y") * rotSpeed * 3;
            _rotX = Mathf.Clamp(_rotX,minimumVert, maximumVert);

        }

        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
        //Maintain the starting offset, shifted according to the camera's position
        transform.position = target.position - (rotation * _offset);
        //no matter where the camera is relative to the target, always face the target
        transform.LookAt(target);
        
    }
}
