using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListStudy : MonoBehaviour
{

    //List : �������� ũ�Ⱑ ���ϱ⶧���� �޸𸮰� ����ϴ¸�ŭ ũ�Ⱑ ���Ѵ�.
    class Skill
    {
        static void SkillStart(string[] args)
        { 
          List<int> intList = new List<int>();
          intList.Add(1);
          intList.Add(2);
          intList.Add(3);
          intList.Add(4);
          intList.Add(5);

            intList.Insert(2, 5); // ù���� ���ڿ��� �Ű������� �ְ�, �ι�°���� �� �����͸� �ֱ�.

            
            // �� ��ȣ�� ���ǹ��� ��꿡���� ���� �����ؼ� �۵��ؾ� �Ѵٸ�, for�� ��� + ��ȸ�ϴ� ���� List�� �����͸� �����ϰų� �����ҋ��� for���� ����ϴ°� ����
            for (int i = 0; i < intList.Count; i++)
            {
                print(intList[i]);
            }

            // �� ��ȣ�� ���ǹ����� �ʰ�, ��� ���� ���������� ���ƾ��Ѵٸ� foreach�� ��� 
            foreach (var i in intList)
            {
                print(intList[i]);
            }

            // ����Ʈ���� �����͸� ã������ Contains �Լ��� ����ϸ� for���� ������ �ʾƵ� �ٷ� ��밡��.
            print(string.Format("{0} is Exist? = {1}", 1, intList.Contains(1))); 
            print(string.Format("{0} is Exist? = {1}", 1, intList.Contains(10)));

            // ���° �׸� �ִ��� Ȯ���Ϸ��� IndexOf�� ����ϸ� ��
            print(string.Format("Value {0} index = {1}", 5, intList.IndexOf(5)));
            print(string.Format("Value {0} index = {1}", 5, intList.IndexOf(15))); // List�ȿ� ������� ���� ���� ������ -1�� ����.


            // �ʿ信 ���� �ʿ���� ���� �����ϱ�
            intList.Remove(1);
            // ���δ� �����ϱ� 
            intList.RemoveAll(delegate (int i) { return i == 1; });
            // ���ϴ� ��ġ�� �� �����ϱ� 
            intList.RemoveAt(1);
            // Ư�� ���� �ȿ� �� �����ϱ� 
            intList.RemoveRange(1, 3);
            // ��� ������ �����ϱ� 
            intList.Clear();
        }


    }

}
