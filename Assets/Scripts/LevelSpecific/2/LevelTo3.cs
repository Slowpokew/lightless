using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTo3 : MonoBehaviour
{
    public int nextLevel;
    public Vector3 nextLevelCameraPosition, currentLevelCameraPosition;
    public Camera currentLevelCamera;
    public GameObject player, prevCollider;
    public static float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentLevelCameraPosition = currentLevelCamera.transform.position;
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
        SceneManager.LoadScene("Level_3", LoadSceneMode.Additive);
        interLevelData.LevelNumberChange(level);

        t = 0.01f;
    }
}
