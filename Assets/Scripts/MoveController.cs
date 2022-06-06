using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Ray ray, floorCheck;
    private Vector3 mousePos;
    public GameObject player, skin;
    public Rigidbody playerRB;
    private bool mouseDown = false;
    private float pointer = 0f, turn, speed = 1f;
    private GameObject[] lights;
    private IllumonationCheck illumonationCheck;
    private int floorLayerMask;

    public static Vector3 playerPos, playerDestination;
    public static float playerRotation;
    public static int isLit;

    // Start is called before the first frame update 
    void Start()
    {
        isLit = 0;
        lights = GameObject.FindGameObjectsWithTag("Light");
        floorLayerMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        int isLitChange = 0;
        RaycastHit hit, hitFloor;

        foreach (GameObject light in lights)
        {
            isLitChange += IllumonationCheck.lightSourcesNumberChange;
        }

        isLit = isLitChange;

        //Debug.Log(isLit);

        if (isLit > 0)
            speed = 0.5f;
        else
            speed = 1f;

        playerPos = player.transform.position;
        skin.transform.position = player.transform.position;

        if (Input.GetMouseButtonDown(0))
            mouseDown = true;
        if (Input.GetMouseButtonUp(0))
            mouseDown = false;

        if (mouseDown)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && Physics.Raycast(player.transform.position, transform.TransformDirection(Vector3.down), out hitFloor, 1.5f))
            {
                mousePos = hit.point;
                //Debug.Log(mousePos);
                Move(player, hit.point);
            }
        }

        if (Math.Abs(playerRB.velocity.x) > 0)
        {
            if (Math.Abs(playerRB.velocity.x) < 0.25f)
            {
                playerRB.velocity = new Vector3(0, 0, playerRB.velocity.z) * speed;
            }

            if (playerRB.velocity.x < 0)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x + 0.25f, 0, playerRB.velocity.z) * speed;
            }

            if (playerRB.velocity.x > 0)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x - 0.25f, 0, playerRB.velocity.z) * speed;
            }
        }

        if (Math.Abs(playerRB.velocity.z) > 0)
        {
            if (Math.Abs(playerRB.velocity.z) < 0.25f)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.z, 0, 0) * speed;
            }

            if (playerRB.velocity.z < 0)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z + 0.25f) * speed;
            }

            if (playerRB.velocity.z > 0)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z - 0.25f) * speed;
            }
        }

        playerDestination = playerPos + (playerRB.velocity) * 1f;
        Vector3 vel = playerRB.velocity;

        if (playerDestination.magnitude <= 2f)
        {
            vel /= 2;
            playerDestination = playerPos + vel;
        }
        if (playerDestination.magnitude > 2f)
        {
            vel *= 2;
            playerDestination = playerPos + vel;
        }
    }
    void Move(GameObject player, Vector3 destination)
    {
        Vector3 playerPosition = player.transform.position;
        float distance,
            xDist = Math.Abs(playerPosition.x - destination.x),
            zDist = Math.Abs(playerPosition.z - destination.z);
        int xMove = 0, zMove = 0;

        distance = Convert.ToSingle(Math.Sqrt(
            xDist * xDist +
            zDist * zDist));

        if (playerPosition.x < destination.x)
            xMove = 1;
        else
            xMove = -1;

        if (playerPosition.z < destination.z)
            zMove = 1;
        else
            zMove = -1;

        if (xDist < 1)
        {
            xDist = zDist;
            xMove = 0;
        }

        if (zDist < 1)
        {
            zDist = xDist;
            zMove = 0;
        }

        if (distance > 0.1f)
        {
            playerRB.velocity = new Vector3(
                Convert.ToSingle(xMove * xDist / distance * 10), 
                0f, 
                Convert.ToSingle(zMove * zDist / distance * 10));
        }

        xDist = Math.Abs(playerPosition.x - destination.x);
        zDist = Math.Abs(playerPosition.z - destination.z);
        float angle = 0;

        if (zDist != 0)
            angle = Convert.ToSingle(Math.Atan(xDist / zDist) * 180 / Math.PI);

        if (destination.x - playerPosition.x > 0 && destination.z - playerPosition.z > 0)
            angle = angle + 0;

        if (destination.x - playerPosition.x > 0 && destination.z - playerPosition.z < 0)
            angle = 180 - angle;

        if (destination.x - playerPosition.x < 0 && destination.z - playerPosition.z < 0)
            angle = angle + 180;

        if (destination.x - playerPosition.x < 0 && destination.z - playerPosition.z > 0)
            angle = 360 - angle;

        if (pointer >= 360)
            pointer -= 360;

        turn = angle - pointer;


        if (turn >= 180)
        {            
            turn = -360 + (turn);
        }
        else
            if (turn <= -180)
            {
                turn = 360 + (turn);
            }

        if (pointer < 0)
        {
            pointer += 360;
        }

        skin.transform.Rotate(0f, turn/2, 0f);
        pointer += turn/2;
        playerRotation = pointer;   

    }
}
