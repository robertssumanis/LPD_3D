using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotation : MonoBehaviour
{

    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Vector3[] angles;
    float rollbackTime = 7;

    int angleIndex;

    float t = 0f;

    void Start()
    {

    }


    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(angles[angleIndex]), lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .01f)
        {
            t = 0f;
            angleIndex = 1;
        }

    }
}
