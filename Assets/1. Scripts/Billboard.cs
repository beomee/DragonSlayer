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
        //transform.forward = Camera.main.transform.forward;  // ���� �������� �ϴ� �հ�, ī�޶� �ٶ󺸴� ������ �Ȱ��� �ٶ󺸱�.
    }
}
