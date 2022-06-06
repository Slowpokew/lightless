using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class IllumonationCheck : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.UI.Image fadePanel;
    public static int lightSourcesNumberChange;

    private float timer = 0f;
    private bool isLitLocal = false, isLitLocal2 = false, isHitOnEdge = true, mirrorHit = false;
    private GameObject[] mirrors;
    private GameObject fadeImageObject;

    public static List<Vector3> edges = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        fadeImageObject = GameObject.Find("FadePanel");
        lightSourcesNumberChange = 0;
        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        fadePanel = fadeImageObject.GetComponent<UnityEngine.UI.Image>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit, hit2;

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            if (hit.collider.name == "Player")
            {
                if (!isLitLocal)
                    lightSourcesNumberChange++;
                isLitLocal = true;
            }
            else
            {
                if (isLitLocal)
                    lightSourcesNumberChange--;
                isLitLocal = false;
            }
            if (hit.collider.tag == "Mirror")
                mirrorHit = true;
        }

        isHitOnEdge = false;
        foreach (GameObject mirror in mirrors)
        {
            Collider mirrorCollider = mirror.GetComponent<Collider>();
            Vector3 projectionLocation = 
                new Vector3 (
                    player.transform.position.x - (2 * (player.transform.position.x - mirrorCollider.transform.position.x)), 
                    player.transform.position.y, 
                    player.transform.position.z - (2 * (player.transform.position.z - mirrorCollider.transform.position.z))
                    );

            foreach (Vector3 edge in edges)
            {
                if (mirrorCollider.ClosestPoint(player.transform.position) == edge)
                {
                    isHitOnEdge = true;
                }
            }

            if (Physics.Raycast(transform.position, projectionLocation - transform.position, out hit2))
            {
                if (hit2.collider.tag == "Mirror" && !isHitOnEdge && !mirrorHit)
                {
                    if (!isLitLocal2)
                        lightSourcesNumberChange++;
                    isLitLocal2 = true;
                }
                else
                {
                    if (isLitLocal2)
                        lightSourcesNumberChange--;
                    isLitLocal2 = false;
                }
            }
        }

        if (lightSourcesNumberChange > 0)
            timer += 1f;
        else
            if (timer > 0)
            timer -= 0.5f;

        if (timer >= 350)
        {
            GameOver(interLevelData.currentLevel);
            timer = 0;
        }
        fadePanel.color = new Color(1, 1, 1, timer / 350);
    }

    void GameOver(int level)
    {
        if (level != 0)
        {
            SceneManager.UnloadSceneAsync("Level_" + Convert.ToInt32(level));
            SceneManager.LoadScene("Level_" + Convert.ToInt32(level), LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("Level_");
            SceneManager.LoadScene("Level_");
            player.transform.position = new Vector3(interLevelData.startpoints[level].x, interLevelData.startpoints[level].y, interLevelData.startpoints[level].z);
            Debug.Log("aaaaaaaaaaaaaaaaaa");
        }
    }
}
