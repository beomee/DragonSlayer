using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    float shakeTime; // ī�޶� ��鸮�� ���� �ð�
    float shakeIntensity; // ī�޶� ��鸮�� ����(Ŭ ���� ���� ��鸮�� �������� ������ �⺻���� 0.1f)


    // Start is called before the first frame update
    void Start()
    {
   


    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void OnShakeCamera(float shakeTime, float shakeIntensity)
    {
        this.shakeTime = shakeTime;   // �Ű������� �޾ƿ� ���� Ŭ������ ������ �־��ش�.
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition"); // ���� �ڷ�ƾ�� �������� ���� �ֱ⶧����, �ߺ� �������� �ʱ� ���� Stop�ڷ�ƾ�� �� �� Start�ڷ�ƾ���� �ڵ� �ۼ� 
        StartCoroutine("ShakeByPosition");
    }

    IEnumerator ShakeByPosition()
    {
        //Vector3 startRotation = transform.eulerAngles; // ��鸮�� ������ ���� ��ġ (��鸲 ���� �Ŀ� �ٽ� ���ƿ� ��ġ��) 
        Vector3 startPosition = transform.position;

        //float power = 5f;

        while (shakeTime > 0.0f) // ī�޶� ��鸮�� ���ӽð��� 0�� �Ǳ� ������ ��� ����
        {

            //Ư�� �ุ �����ϱ� ���ϸ� �Ʒ� �ڵ带 ��� (�̵����� ���� ���� 0 ����) 
            //float x = 0; //Random.Range(-1f,1f);
            //float y = 0; //Random.Range(-1f,1f);
            //float z = Random.Range(-0.5f,0.5f);
            //transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            // �ʴ� 1�� ����
            shakeTime -= Time.deltaTime;

            yield return null; // �� �����Ӹ��� ����
        }
        // ȭ�� ���� ����� �Ϸ�Ǹ�, ��鸮�� ���� ��ġ�� �ٽ� �ǵ��ư�.
        //transform.rotation = Quaternion.Euler(startRotation);
        transform.position = startPosition;
    }

}
