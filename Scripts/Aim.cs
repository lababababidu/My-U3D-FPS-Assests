using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    bool isaim = false;
    void Update()
    {
        if (Input.GetMouseButton(1)){
            aim();
        }else{
            cancelaim();
        }
    }

    private void aim(){
        if(isaim == true){
            return;
        }
        isaim = true;
        transform.localPosition  = new Vector3(-0.7f,0f,0f);
    }

    private void cancelaim(){
        if(isaim == false){
            return;
        }
        isaim = false;
        transform.localPosition  = new Vector3(0f,0f,0f);
    }
}
