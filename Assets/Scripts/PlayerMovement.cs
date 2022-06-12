using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    bool jumpKeyWasPressed;
    bool leftShiftKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbody;
    private int superJumps;
    private bool superJumpActive;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Transform groundCheckTransform;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        superJumpActive = false;
        //leftShiftKeyWasPressed = false;

        superJumps = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftShiftKeyWasPressed = true;
        }
        */
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, groundMask).Length != 0)
        {


            /*
            if (superJumps > 0)
            {

               
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    superJumpActive = true;
                    
                   
                }

              
            }

            if (jumpKeyWasPressed)
            {
                if(superJumpActive == true)
                {
                    superJumps--;
                    jumpPower *= 2;
                    superJumpActive = false;
                }

                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                jumpKeyWasPressed = false;
                jumpPower = 5f;
            }


            */

            if (jumpKeyWasPressed)
            {

                /*
                float jumpPower = 5f;

                if(superJumps > 0)
                {
                    jumpPower *= 2;
                    superJumps--;
                }
                    
                    */
                

                rigidbody.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
                jumpKeyWasPressed = false;
            }

            if(superJumps > 0)
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
              
                    rigidbody.AddForce(Vector3.up * 7f, ForceMode.VelocityChange);
                    jumpKeyWasPressed = false;
                    leftShiftKeyWasPressed = false;
                    superJumps--;
                
               
            }

        }

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, horizontalInput * 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            superJumps++;
        }
    }
}
