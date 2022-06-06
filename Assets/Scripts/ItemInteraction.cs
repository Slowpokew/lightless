using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    private Vector3 playerPos, targetPos, playerDestination;
    private float playerRotationY;
    private Ray ray;
    private bool isCarrying = false;

    public Rigidbody objectRB;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = MoveController.playerPos;
        playerRotationY = MoveController.playerRotation;
        playerDestination = MoveController.playerDestination;

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (isCarrying)
                {
                    isCarrying = false;
                }
                else 
                    if (hit.collider.gameObject == gameObject)
                    {
                        TryPickingUp(playerPos, hit.collider);
                    }
            }
        }

        if (isCarrying)
        {
            targetPos = new Vector3(playerDestination.x, playerPos.y + 1.1f, playerDestination.z);
            objectRB.velocity = (targetPos - objectRB.transform.position)*3/(objectRB.mass/100f);
        }
    }

    void TryPickingUp(Vector3 playerPos, Collider objectCollider)
    {
        float distance;

        
        distance = Convert.ToSingle(Math.Sqrt(
            (playerPos.x - objectCollider.gameObject.transform.position.x) * (playerPos.x - objectCollider.gameObject.transform.position.x) +
            (playerPos.y - objectCollider.gameObject.transform.position.y) * (playerPos.y - objectCollider.gameObject.transform.position.y) +
            (playerPos.z - objectCollider.gameObject.transform.position.z) * (playerPos.z - objectCollider.gameObject.transform.position.z)));

        if (distance <= 2)
        {
            isCarrying = true;
        }
    }
}


        
    

