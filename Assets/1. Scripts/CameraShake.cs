using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용하지 않음.
public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float m_force = 0f; // 카메라가 흔들릴 강도 

    [SerializeField]
    Vector3 m_offset = Vector3.zero;  // 카메라가 흔들릴 방향

    public Quaternion m_originRot; // 카메라의 초기값을 저장할 변수 


    // Start is called before the first frame update
    void Start()
    {

        m_originRot = transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public IEnumerator DamagedEffect()
    {
        StartCoroutine(ShakeCoroutine());

        yield return new WaitForSeconds(0.4f);

        StartCoroutine(ShakeCoroutineReset());

    }


    IEnumerator ShakeCoroutine()
    {
        Vector3 t_originEuler = transform.eulerAngles;  // 카메라의 오일러 초기값을 담을 변수 -> t_originEuler
        while (true)
        {
            //벡터 축 마다 랜덤값 부여
            float t_rotX = Random.Range(-m_offset.x, m_offset.x); 
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);

            // 흔들림 값 = 초기값 + 랜덤값 
            Vector3 t_randomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);

            // 흔들림 값을 쿼터니언으로 변환 
            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            // 목적값까지 움직일 때까지 while 반복 
            while (Quaternion.Angle(transform.rotation, t_rot) > 0.1f)
            {
                // 매 프레임마다 목표값의 근사치가 될 때까지 반복 
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator ShakeCoroutineReset() // 카메라를 초기값으로 되돌림 
    {
        while (Quaternion.Angle(transform.rotation, m_originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }

}
