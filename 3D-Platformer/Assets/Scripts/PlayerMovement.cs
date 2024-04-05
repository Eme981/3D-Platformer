using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;

    private float ySpeed;
    private CharacterController characterC;
    public bool isGrounded;

    private void Start()
    {
        characterC = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        moveDirection.Normalize();
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        //transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        characterC.SimpleMove(moveDirection * magnitude * speed);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
        {
            ySpeed = -0.5f;
            isGrounded = false;
        }

        Vector3 vel = moveDirection * magnitude;
        vel.y = ySpeed;
        //transform.Translate(vel * Time.deltaTime);
        characterC.Move(vel * Time.deltaTime);

        if(characterC.isGrounded)
        {
            ySpeed = -0.5f;
            isGrounded = true;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                isGrounded = false;
            }
        }

        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}
