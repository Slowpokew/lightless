using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public GameObject Light;

    private bool isLit = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLit)
        {
            FlickerAsync();
        }
    }

    async Task FlickerAsync()
    {
        Light.SetActive(!Light.activeSelf);
        isLit = !isLit;
        await Task.Delay(3000);
        isLit = !isLit;
    }
}
