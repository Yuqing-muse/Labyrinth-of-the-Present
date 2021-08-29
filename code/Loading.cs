using UnityEngine;

using System.Collections;

public class Loading : MonoBehaviour
{

    public Texture2D load_write;    //进度条底纹  

    public Texture2D load_yellow;   //进度条  

    public float loading = 0f;        //进度条数字显示  

    public Texture2D img;

    public GUISkin GUIskin;

    //在这里记录当前切换场景的名称  

    // public static string loadName;  



    void Update()
    {

        if (loading >= 100)
        {

            Application.LoadLevel("Store");

        }

        else
        {

            loading += Time.deltaTime * 20;

        }

    }

    void OnGUI()
    {

        //加载背景图  

        GUIStyle backGround = new GUIStyle();

        backGround.normal.background = img;

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", backGround);

        //进度条加载  

        if (loading <= 100)
        {

            float blood_width = load_yellow.width * loading / 100;

            //进度条底纹  

            GUI.DrawTexture(new Rect(Screen.width * 0.05f, Screen.height * 0.9f, load_write.width, load_write.height), load_write);

            //进度条  

            GUI.DrawTexture(new Rect(Screen.width * 0.13f, Screen.height * 0.92f, blood_width, load_yellow.height), load_yellow);

            //Loading 字体  

            GUIStyle go = new GUIStyle();

            go.fontSize = 30;

            go.normal.textColor = new Color(255, 255, 255);

            GUI.skin = GUIskin;
            GUI.Label(new Rect(Screen.width * 0.15f, Screen.height * 0.9f, 100, 100), "Loading.....", go);

            //加载数字的显示  

            GUIStyle go1 = new GUIStyle();
            go1.fontSize = 30;

            //go1.normal.textColor = Color.blue;  

            //进度显示的数字的位置和内容
            GUI.skin = GUIskin;
            GUI.Label(new Rect(Screen.width * 0.6f, Screen.height * 0.85f, 100, 100), (int)loading + "%", go);

        }

    }

}
