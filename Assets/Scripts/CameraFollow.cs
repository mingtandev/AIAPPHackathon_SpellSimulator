using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float mouseSensity = 100f;
    public Transform playerBody;


    float xRotation = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        xRotation -= mouseY;

        transform.localRotation = Quaternion.Euler(xRotation , 0f , 0f);

        playerBody.Rotate(Vector3.up, mouseX);




    }
}
