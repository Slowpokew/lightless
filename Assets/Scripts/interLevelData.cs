using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interLevelData : MonoBehaviour
{
    public static int currentLevel;
    public static Vector3[] startpoints = new Vector3[100];
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        startpoints[0] = new Vector3(-40, 0.5f, 10);
        startpoints[2] = new Vector3(37, 10, 7);
        startpoints[2] = new Vector3(50, 10, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LevelNumberChange(int newNumber)
    {
        currentLevel = newNumber;
    }
}
