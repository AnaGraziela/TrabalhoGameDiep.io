using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    float Delay = 3f;
    void Start()
    {
        Destroy(gameObject, Delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
