using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrinFixed : MonoBehaviour
{

    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //// �ٸ� ��ü�� �ε������� �Ͼ�� �����ۿ� ����
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
