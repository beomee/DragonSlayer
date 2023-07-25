using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListStudy : MonoBehaviour
{

    //List : 동적으로 크기가 변하기때문에 메모리가 허용하는만큼 크기가 변한다.
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

            intList.Insert(2, 5); // 첫번쨰 숫자에는 매개변수를 넣고, 두번째에는 들어갈 데이터를 넣기.

            
            // 방 번호가 유의미한 계산에따라 방을 선택해서 작동해야 한다면, for를 사용 + 순회하는 도중 List의 데이터를 삭제하거나 변동할떄도 for문을 사용하는게 좋음
            for (int i = 0; i < intList.Count; i++)
            {
                print(intList[i]);
            }

            // 방 번호가 유의미하지 않고, 모든 방을 순차적으로 돌아야한다면 foreach를 사용 
            foreach (var i in intList)
            {
                print(intList[i]);
            }

            // 리스트안의 데이터를 찾으려면 Contains 함수를 사용하면 for문을 돌리지 않아도 바로 사용가능.
            print(string.Format("{0} is Exist? = {1}", 1, intList.Contains(1))); 
            print(string.Format("{0} is Exist? = {1}", 1, intList.Contains(10)));

            // 몇번째 항목에 있는지 확인하려면 IndexOf를 사용하면 됨
            print(string.Format("Value {0} index = {1}", 5, intList.IndexOf(5)));
            print(string.Format("Value {0} index = {1}", 5, intList.IndexOf(15))); // List안에 들어있지 않은 값을 넣으면 -1이 나옴.


            // 필요에 따라서 필요없는 것은 삭제하기
            intList.Remove(1);
            // 전부다 삭제하기 
            intList.RemoveAll(delegate (int i) { return i == 1; });
            // 원하는 위치의 값 삭제하기 
            intList.RemoveAt(1);
            // 특정 범위 안에 값 삭제하기 
            intList.RemoveRange(1, 3);
            // 모든 데이터 삭제하기 
            intList.Clear();
        }


    }

}
