using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class essential : MonoBehaviour {

    // Use this for initialization
    //进入AR界面
    public void EnterAR()
    {
        Application.LoadLevel("AR");
    }
    //离开AR界面
    public void OutAR()
    {
        Application.LoadLevel("main");

    }

    public  void IntoMain()
    {
        Application.LoadLevel("main");
    }

    public void An()
    {
        Application.LoadLevel("Store");
    }
    public  void Enter()
    {
        Application.LoadLevel("loading");
    }
    public  void Out()
    {
        Application.Quit();
    }
}
