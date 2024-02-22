using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtomoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
