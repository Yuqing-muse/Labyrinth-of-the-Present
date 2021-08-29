using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    internal object onClick;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NewGame()//Begin界面中的  进入游戏按键
    {
       Application.LoadLevel("NewGameLoading");
    }

    public void OpenBag()//背包组件中，控制背包打开的图标按钮
    {
        Knapsack.Instance.DisplaySwitch();
    }

    public void GoToChoice()//选择关卡城市
    {
        Application.LoadLevel("Level");
    }
    public void Exitchangetu()
    {
        Application.LoadLevel("Turkey_KST_alpha test");
    }
    public void Exitchangeai()
    {
        Application.LoadLevel("Ethiopia");
    }
}
