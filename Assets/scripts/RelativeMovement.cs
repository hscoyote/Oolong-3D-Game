using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private AudioClip jumpsound;

    public float moveSpeed = 6.0f;
    public float runSpeed = 16.0f;

    public float jumpspeed = 20.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;

    private CharacterController _charController;

    ///
    //
    private bool allowedToMove;
    public void SetMovement(bool b)
    {
        allowedToMove = b;
    }


    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        //initialize the vertical speed to the minimum falling speed at the start of the existign function
        _vertSpeed = minFall;
        allowedToMove = true;
    }

    
    //this script needs a reference to the object to move relative to
    [SerializeField] private Transform target;

    //initialize rot speed
    public float rotSpeed = 15.0f;

    // Update is called once per frame
    void Update()
    {
        if (allowedToMove)
        {
             ///start with vector (0,0,0) and asdd movement components progressively 
        Vector3 movement = Vector3.zero;


        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

       

        //only handle movement while arrow keys are pressed
        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;

            //limit diagonal movement to the same speed as movement along an axis
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            
            //Keep the initial rotation to restore after finishing with the target object
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            //transform movement direction from Local to global coordinates 
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            //LookRotation() calculates a quaternion facing in that direction 
            //transform.rotation = Quaternion.LookRotation(movement);

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);


        }

          //character controller has an isGrounded property to check if the controller is on the ground
            if (_charController.isGrounded)
            {
                //react to the jump button while on the ground
                if (Input.GetButton("Jump"))
                {
                    _vertSpeed=jumpspeed;

                    // play sound 
                    SoundManager.instance.PlaySoundClip(jumpsound, transform, 1f);
                } else
                {
                    _vertSpeed = minFall;
                } 
                  //movement speed changes if Left Shift is pressed when player is grounded
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = runSpeed;
            
                } else
                {
                    moveSpeed = 6.0f;

                }
                
            } else
            {
                //if not on the ground, then apply gravity until terminal velocity is reached
                _vertSpeed += gravity * 5 * Time.deltaTime;
                if (_vertSpeed < terminalVelocity)
                {
                    _vertSpeed = terminalVelocity;
                }
            }
            movement.y = _vertSpeed;

            movement *= Time.deltaTime;
            _charController.Move(movement);
            
        }
       
    }
}
