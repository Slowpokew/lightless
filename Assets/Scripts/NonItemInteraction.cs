using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonItemInteraction : MonoBehaviour
{
    private Vector3 playerPos;
    private Ray ray;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = MoveController.playerPos;

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (
                Physics.Raycast(ray, out hit) && 
                hit.collider.gameObject == gameObject && 
                (gameObject.transform.position - playerPos).magnitude < 3f
                )
            {
                if (gameObject.tag == "Switch")
                {
                    if (target.activeSelf)
                        target.SetActive(false);
                    else
                        target.SetActive(true);
                }
            }
        }        
    }
}
