using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCamera3 : MonoBehaviour
{
    public float rotSpeed;  // ȸ�� �ӵ�
    public Transform player;

    bool isDownMax; // ���콺�� ���� �� �ִ� �ִ�ġ�� �����ߴ��� �Ǵ��ϴ� �ڵ�
    bool isUpMax;  // ���콺�� �ø� �� �ִ� �ִ�ġ�� �����ߴ��� �Ǵ��ϴ� �ڵ� 

    private void Awake()
    {
        transform.position = player.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        // < 3��Ī ī�޶��� ���콺�� �����̴� ��� > 

        // ���콺�� ���� ������ ����
        float rotX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

        // ���콺�� �����ٰ� ������ 30�̻��� �Ǹ� �ִ��� �����ϱ�
        if (transform.eulerAngles.x >= 30 && transform.eulerAngles.x <= 180)  // 30�� �̻�� 350�� ���϶�� ������ ��ġ�� ��츦 ���ֱ� ���� 180�̶�� ������ �߰��� ����.
        {
            isDownMax = true;  // �ִ�ġ�� ���������� ����
        }

        // ���콺�� �ø��ٰ� ������ -10(350)���ϰ� �Ǹ� �ִ��� �����Ű�� 
        else if (transform.eulerAngles.x <= 350 && transform.eulerAngles.x >= 180)
        {
            isUpMax = true;
        }

        // ���콺�� ���� �÷ȴٸ� �ִ�ġ ���� ������Ű�� (������ Ǫ�� �ڵ�� �� �߰��� �ۼ��ϱ�)
        if (rotX > 0)
        {
            isDownMax = false;
        }

        // ���콺�� �Ʒ��� ���ȴٸ� �ִ�ġ ���� ������Ű�� (������ Ǫ�� �ڵ�� �� �߰��� �ۼ��ϱ�)
        else if (rotX < 0)
        {
            isUpMax = false;
        }

        // �ִ�ġ�� �������� �ʾ������� ȸ���ϰԲ� 
        if (!isDownMax && !isUpMax)
        {
            transform.RotateAround(player.position, transform.right, -rotX); // ������ ��������, � ������, �󸶸�ŭ 
        }
    }



}
