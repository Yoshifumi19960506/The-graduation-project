using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
//↑注意***********************************************

public class ControllerController : MonoBehaviour
{

    //viveコントローラの入力を制御するスクリプト
    //ぶっちゃけよくわかってない

    //使う手
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean grabAction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //とりまviveの横ボタンで入力できる
        if (grabAction.GetState(hand))
        {
            Debug.Log("Grab");
        }
    }
}
