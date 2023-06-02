using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{


    public float TimeLeft = 2;


    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0)
        {
            Destroy(gameObject);
        }
    }
}
