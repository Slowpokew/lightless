using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 focusPoint;
    private List<Vector3> settings = new List<Vector3>();
    public static int levelNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        settings.Add(new Vector3(-5, 0, 0));
        settings.Add(new Vector3(-5, 0, 0));
        settings.Add(new Vector3(-25, 0, 0));
        settings.Add(new Vector3(-25, 0, 100));

        focusPoint = new Vector3(
            player.position.x / 3,
            player.position.y / 5,
            player.position.z / 10)
            + settings[levelNumber];
    }

    // Update is called once per frame
    void Update()
    {
        focusPoint = Vector3.Lerp(
            new Vector3(player.position.x / 3, player.position.y / 5, player.position.z / 10),
            new Vector3(player.position.x / 3, player.position.y / 5, player.position.z / 10) + settings[levelNumber] + gameObject.transform.position - new Vector3(-9f, 17, -20),
            LevelChange.t);

        transform.LookAt(focusPoint);
        levelNumber = interLevelData.currentLevel;
    }
}
