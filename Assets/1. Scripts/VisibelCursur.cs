using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibelCursur : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = true; // 마우스 커서 보이게하기 
        Cursor.lockState = CursorLockMode.Confined; // 마우스 다시 움직이도록 하기

    }      

    // Update is called once per frame
    void Update()
    {
        

    }


}

