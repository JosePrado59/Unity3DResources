using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public float moveSpeed = 5;
    private float speed;

    private Vector3 direction;
    private Vector3 movement;

    private void Awake()
    {
        speed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {  
        ControlPlayer();
    }
    
    void ControlPlayer()
    {
        Vector3 direction = new Vector3(InputManager.GetSharedInstance().GetInput().XAxis, 0.0f, 
            InputManager.GetSharedInstance().GetInput().YAxis);

        Vector3 movement = Camera.main.transform.TransformDirection(direction);
        movement.y = 0.0f;
        movement.Normalize();

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(movement),Time.deltaTime*7.3f);
        }
        transform.Translate(movement*moveSpeed*Time.deltaTime, Space.World);

        animator.SetBool("IsWalking", (direction.x != 0 ||direction.z != 0) && IsGrounded());
    }

    bool IsGrounded()
    {
        Vector3 dir = new Vector3(0, -1);
        bool grounded;
        
        if(Physics.Raycast(transform.position, dir, 0.4f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        return grounded;
    }
    
}
