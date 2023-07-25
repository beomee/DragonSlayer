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
        //// 다른 물체와 부딪혔을때 일어나는 물리작용 제어
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
