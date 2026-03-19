using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    
    [SerializeField] private AudioClip gunshot;
    
    //private variable that has a reference to the camera
    //public Vector3 startPosition;
    private Camera cam;

    private bool allowedToShoot;

    public void SetShooting(bool b)
    {
        allowedToShoot = b;
    }

    //for camera zoom
    /*[SerializeField] private float zoomFOV;
    [SerializeField] private float normalFOV;
    [SerializeField] private float zoomSpeed; */


    // Start is called before the first frame update
    void Start()
    {
        allowedToShoot = true;
        cam = GetComponent<Camera>();

        //Hide the cursor at the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float targetFOV;


        if (Input.GetKey(KeyCode.Mouse1))
        {
            targetFOV = zoomFOV;
            
        } else
        {
            targetFOV = normalFOV;

        }

        cam.fieldOfView = targetFOV; */



        //run the following code of the player clicks the left mouse button
        if (Input.GetMouseButtonDown(0) && allowedToShoot){
            ///use a Vector3 to store the location of the middle of the screen
            ///  divide the width and height by 2 to get the midpoint; these become 
            /// the x and y values of the vector, with the z value being zero
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);

            //create a ray by calling ScreenPointToRay
            //pass in the pint as this is used as the origin for the ray
            Ray ray = cam.ScreenPointToRay(point);

            ///create a RaycastHit object to figure out where the ray 
            /// hit 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                //for now, print the coords of where the ray hit
                //Debug.Log("Hit: " + hit.point);
                //StartCoroutine(SphereIndicator(hit.point));

                ///Get a reference to the object that was hit, then
                /// get a reference to that object's ReactiveTarget script,
                /// if there is one
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                ///If the ray had hit an enemy, (that is, "target
                ///  isn't null),
                /// indicate that an enemy was hit
                /// otherwise, place a sphere 
                if (target != null)
                {
                    Debug.Log("Target hit!");
                    target.ReactToHit();
                } else
                {
                    //StartCoroutine(SphereIndicator(hit.point));
                }

                //plays sound 
                SoundManager.instance.PlaySoundClip(gunshot, transform, 1f);

            }

        }

        
    }

    ///OneGUI method
    /// for drawing crosshairs on the screen
    private void OnGUI()
    {
        //Font Size
        int size = 12;

        //coords at which the crosshairs are drawn
        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;

        //draw the crosshairs as text
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

            ///coroutine 
            /// this places a sphere at a set of coords, then 
            /// removes the sphere after 1 second
            /*private IEnumerator SphereIndicator(Vector3 pos) {
                //create a new game object that's a sphere
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                //then place it at the given position
                sphere.transform.position = pos;

                //then wait one second
                yield return new WaitForSeconds(1);

                //then destroy the sphere
                Destroy(sphere);
            }*/
}
