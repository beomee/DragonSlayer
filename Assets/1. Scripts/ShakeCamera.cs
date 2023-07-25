using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    float shakeTime; // 카메라가 흔들리는 지속 시간
    float shakeIntensity; // 카메라가 흔들리는 세기(클 수록 세게 흔들리고 설정하지 않으면 기본값은 0.1f)


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
        this.shakeTime = shakeTime;   // 매개변수로 받아온 값을 클래스의 변수로 넣어준다.
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition"); // 현재 코루틴을 실행중일 수도 있기때문에, 중복 실행하지 않기 위해 Stop코루틴을 한 후 Start코루틴으로 코드 작성 
        StartCoroutine("ShakeByPosition");
    }

    IEnumerator ShakeByPosition()
    {
        //Vector3 startRotation = transform.eulerAngles; // 흔들리기 직전의 시작 위치 (흔들림 종료 후에 다시 돌아올 위치임) 
        Vector3 startPosition = transform.position;

        //float power = 5f;

        while (shakeTime > 0.0f) // 카메라가 흔들리는 지속시간이 0이 되기 전까지 계속 실행
        {

            //특정 축만 변경하길 원하면 아래 코드를 사용 (이동하지 않을 값은 0 대입) 
            //float x = 0; //Random.Range(-1f,1f);
            //float y = 0; //Random.Range(-1f,1f);
            //float z = Random.Range(-0.5f,0.5f);
            //transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            // 초당 1씩 감소
            shakeTime -= Time.deltaTime;

            yield return null; // 매 프레임마다 적용
        }
        // 화면 흔들기 재생이 완료되면, 흔들리기 전의 위치로 다시 되돌아감.
        //transform.rotation = Quaternion.Euler(startRotation);
        transform.position = startPosition;
    }

}
