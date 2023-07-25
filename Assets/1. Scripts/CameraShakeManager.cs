using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{


    public static CameraShakeManager instance;

    [SerializeField] float shakeForce = 0.1f; // Ä«¸Þ¶ó¸¦ Èçµå´Â Èû.

    private void Awake()
    {
        if (instance == null)  // ½Ì±ÛÅæ
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)  // Ä«¸Þ¶ó Èçµé¸®´Â ÇÔ¼ö 
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
