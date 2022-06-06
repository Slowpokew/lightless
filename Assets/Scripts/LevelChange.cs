using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int nextLevel;
    public Vector3 nextLevelCameraPosition, currentLevelCameraPosition;
    public Camera currentLevelCamera;
    public GameObject player, nextCollider;

    public static float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentLevelCameraPosition = currentLevelCamera.transform.position;
        Debug.DrawLine(currentLevelCameraPosition, nextLevelCameraPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (t != 0f && t <= 1f)
        {
            currentLevelCamera.transform.position = Vector3.Lerp(currentLevelCameraPosition, nextLevelCameraPosition, t);
            t += 0.007f;
        }
        if (t >= 1)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        NewLevel(nextLevel);
    }

    private void NewLevel(int level)
    {
        SceneManager.LoadScene("Level_" + Convert.ToInt32(level), LoadSceneMode.Additive);
        interLevelData.LevelNumberChange(level);

        try
        {
            //SceneManager.UnloadSceneAsync("Level_" + Convert.ToInt32(level - 2));
        }
        catch
        {
            
        }
        //gameObject.transform.Translate(0, -100,0);
        //player.SetActive(false);

        t = 0.01f;
    }
}
