using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
//using Common;

public class UIMessage : MonoBehaviour
{
    public Text Title;
    public Text Content;//这个是Content下的text
    public Button Sure;
    public Button Cancel;
   ///*private GameObject sure;
    ///private GameObject cancel;
    void Start()
    {
        ///sure = GameObject.FindGameObjectWithTag("Sure");
        ///cancel = GameObject.FindGameObjectWithTag("Cancel");
        Button s = Sure.GetComponent<Button>();
        Button c = Cancel.GetComponent<Button>();
        //s.onClick.AddListener(MessageBox.Sure);

        

        Title.text = MessageBox.TitleStr;
        Content.text = MessageBox.ContentStr;
    }
   
    }


public class MessageBox
{
    static GameObject Messagebox;
    static int Result = -1;
    public static Confim confim;
    public static string TitleStr;
    public static string ContentStr;
    public static void Show(string content)
    {
        ContentStr = "    " + content;
        Messagebox = (GameObject)Resources.Load("Prefab/Background");
        Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
        Messagebox.transform.localScale = new Vector3(1, 1, 1);
        Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        Time.timeScale = 1;
    }
    public static void Show(string title, string content)
    {
        TitleStr = title;
        ContentStr = "    " + content;
        Messagebox = (GameObject)Resources.Load("Prefab/Background");
        Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
        Messagebox.transform.localScale = new Vector3(1, 1, 1);
        Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        Time.timeScale = 1;
    }
    public static void Sure()
    {
        if (confim != null)
        {
            confim();
            GameObject.Destroy(Messagebox);
            TitleStr = "标题";
            ContentStr = null;
            Time.timeScale = 0;
        }
    }
    public static void Cancle()
    {
        Result = 2;
        GameObject.Destroy(Messagebox);
        TitleStr = "标题";
        ContentStr = null;
        Time.timeScale = 0;
    }
   /* public void test()
    {
        MessageBox.Show("XXX窗口", "输出了XXXX");
        MessageBox.confim = () =>
        {
            //这里写你自己点击确定按钮后的操作
            Debug.Log("shabile");
        };
    }*/

}
