using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTxt : MonoBehaviour
{
    float dir = 1f;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, dir * 50f, 0) * Time.deltaTime;


    }


}
