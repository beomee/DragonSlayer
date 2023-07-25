using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Test : MonoBehaviour
{

    CinemachineImpulseSource impulse;
    // Start is called before the first frame update
    void Start()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();

       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Shake()
    {
        impulse.GenerateImpulse(100f);
    }
}
