using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float speed = 5f;
    public Transform groundDetect;
    public LayerMask GroundLayer;
    float gravity = -9.8f;
    Vector3 velocity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ApplyVelocity();    
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void ApplyVelocity()
    {
        //CHECK IS GROUND , velocity  impact by gravity little bit
        bool isGround = Physics.CheckSphere(groundDetect.position, 0.4f, GroundLayer);
        if (isGround)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime * Time.deltaTime;
        }

        controller.Move(velocity);
    }
}
