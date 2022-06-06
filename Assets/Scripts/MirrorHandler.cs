using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MirrorHandler : MonoBehaviour
{
    public GameObject player;

    public static Vector3 right, left;
    List<Light> mirrorLights = new List<Light>();
    List<Light> lights = new List<Light>();
    private Ray ray;

    [SerializeField] private LayerMask mirrorCullingMask;

    // Start is called before the first frame update
    void Start()
    {
        Light[] lightsTemp = FindObjectsOfType(typeof(Light)) as Light[];

        for (int i = 0; i < lightsTemp.Length; i++)
            lights.Add(lightsTemp[i]);

        for (int i = 0; i < lightsTemp.Length; i++)
        {
            if (lightsTemp[i].CompareTag("Light"))
            {
                mirrorLights.Add(Instantiate(lights[i]));
                mirrorLights[i].transform.position =
                    new Vector3
                    (
                        mirrorLights[i].transform.position.x - (2 * (mirrorLights[i].transform.position.x - gameObject.transform.position.x)),
                        mirrorLights[i].transform.position.y,
                        mirrorLights[i].transform.position.z - (2 * (mirrorLights[i].transform.position.z - gameObject.transform.position.z))
                    );
                mirrorLights[i].transform.rotation = lights[i].transform.rotation;
                mirrorLights[i].transform.LookAt(gameObject.transform.position + new Vector3(0, -2.5f, 0));
                mirrorLights[i].innerSpotAngle = gameObject.transform.localScale.x;
                //Debug.Log(mirrorLights[i].spotAngle);
                mirrorLights[i].spotAngle = 10;
                mirrorLights[i].cullingMask = mirrorCullingMask;
                mirrorLights[i].GetComponent<IllumonationCheck>().enabled = false;
                
            }
            else
            {
                mirrorLights.Add(new Light());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        foreach (Light light in mirrorLights)
        {
            try
            {
                if (Physics.Raycast(light.transform.localPosition, (light.transform.localPosition - gameObject.transform.position) * -1, out hit))
                {
                    if (hit.collider.name != "MirrorBody")
                    {
                        light.enabled = false;
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch { }
        }

        left = new Vector3
            (
                gameObject.transform.position.x - (gameObject.transform.localScale.x / 2 * Convert.ToSingle(Math.Cos(Convert.ToDouble((Math.PI / 180) * (180 - gameObject.transform.localRotation.eulerAngles.y))))),
                player.transform.position.y,
                gameObject.transform.position.z - (gameObject.transform.localScale.x / 2 * Convert.ToSingle(Math.Sin(Convert.ToDouble((Math.PI / 180) * (180 - gameObject.transform.localRotation.eulerAngles.y)))))
            );

        right = new Vector3
            (
                gameObject.transform.position.x + (gameObject.transform.localScale.x / 2 * Convert.ToSingle(Math.Cos(Convert.ToDouble((Math.PI / 180) * (180 - gameObject.transform.localRotation.eulerAngles.y))))),
                player.transform.position.y,
                gameObject.transform.position.z + (gameObject.transform.localScale.x / 2 * Convert.ToSingle(Math.Sin(Convert.ToDouble((Math.PI / 180) * (180 - gameObject.transform.localRotation.eulerAngles.y)))))
            );
        IllumonationCheck.edges.Add(right);
        IllumonationCheck.edges.Add(left);
    }
}
