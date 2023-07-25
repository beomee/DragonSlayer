using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� ����.
public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float m_force = 0f; // ī�޶� ��鸱 ���� 

    [SerializeField]
    Vector3 m_offset = Vector3.zero;  // ī�޶� ��鸱 ����

    public Quaternion m_originRot; // ī�޶��� �ʱⰪ�� ������ ���� 


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
        Vector3 t_originEuler = transform.eulerAngles;  // ī�޶��� ���Ϸ� �ʱⰪ�� ���� ���� -> t_originEuler
        while (true)
        {
            //���� �� ���� ������ �ο�
            float t_rotX = Random.Range(-m_offset.x, m_offset.x); 
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);

            // ��鸲 �� = �ʱⰪ + ������ 
            Vector3 t_randomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);

            // ��鸲 ���� ���ʹϾ����� ��ȯ 
            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            // ���������� ������ ������ while �ݺ� 
            while (Quaternion.Angle(transform.rotation, t_rot) > 0.1f)
            {
                // �� �����Ӹ��� ��ǥ���� �ٻ�ġ�� �� ������ �ݺ� 
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator ShakeCoroutineReset() // ī�޶� �ʱⰪ���� �ǵ��� 
    {
        while (Quaternion.Angle(transform.rotation, m_originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }

}
