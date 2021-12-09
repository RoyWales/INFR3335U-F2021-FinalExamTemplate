using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    public static int playerSpeed = 3;
    private float gravityValue = -1.0f;

    public Transform cam;

    public Joystick movestick;

    public PhotonView view;

    void Update()
    {
        if (view.IsMine)
        {
            PlayerMovement();
            //CameraControls();
        }
    }

    public void PlayerMovement()
    {
        //joystick.Horizontal;


        //This is for moving the character
        Vector3 move = new Vector3(movestick.Horizontal * -1, 0, movestick.Vertical * -1); //had to flip it (* -1)
                                                                                           // Vector3 move = new Vector3(Input.GetAxis("Horizontal") * -1, 0, Input.GetAxis("Vertical") * -1); //had to flip it (* -1)
        controller.Move(move * Time.deltaTime * playerSpeed); //multiply move vector by time and speed

        // This turns the player to face the direction they are moving in if not zero
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime; //adds the gravity(y) value
        controller.Move(playerVelocity * Time.deltaTime);  //applies gravity
    }

    public void SetJoysticks(GameObject camera)
    {
        Joystick[] tempJoystickList = camera.GetComponentsInChildren<Joystick>();
        foreach (Joystick temp in tempJoystickList)
        {
            if (temp.tag == "MovementJoystick")
            {
                movestick = temp;
            }

        }

        //cam = camera.transform;

        CinemachineVirtualCamera vcam = camera.GetComponentInChildren<CinemachineVirtualCamera>();

        vcam.LookAt = transform;
        vcam.Follow = vcam.LookAt;

    }
}
