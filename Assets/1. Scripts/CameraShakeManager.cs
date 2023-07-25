using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{


    public static CameraShakeManager instance;

    [SerializeField] float shakeForce = 0.1f; // ī�޶� ���� ��.

    private void Awake()
    {
        if (instance == null)  // �̱���
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)  // ī�޶� ��鸮�� �Լ� 
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
