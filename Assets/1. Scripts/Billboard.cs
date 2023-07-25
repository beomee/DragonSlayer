using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        //transform.forward = Camera.main.transform.forward;  // 나를 기준으로 하는 앞과, 카메라가 바라보는 방향을 똑같이 바라보기.
    }
}
