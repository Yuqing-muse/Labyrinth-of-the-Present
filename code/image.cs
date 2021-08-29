using UnityEngine;
using UnityEngine.UI;//犹如我们要获取一个text并操作，所以需要这个头文件  
using System.Collections;

public class image : MonoBehaviour
{

   // GameObject text;//获取notice提示文本，存在这个变量里面进行操作  
   // GameObject plane;
    public static Vector3 vec3, pos;//用于存放坐标，其变量类型也是Vector3，从官方文档抄来的，我也不知道为什么  
    private changestring c;
    //初始化函数  
    void Start()
    {
        
       
        
        c = this.GetComponent<changestring>();
       // gameObject.SetActive(false);//让自己隐藏，也就是这个image  
    }

    // Update is called once per frame  
    void Update()
    {

    }

    //按下鼠标将会被触发的事件  
    public void PointerDown()
    {
        vec3 = Input.mousePosition;//获取当前鼠标位置  
        pos = transform.GetComponent<RectTransform>().position;//获取自己所在的位置  
    }

    //鼠标拖拽时候会被触发的事件  
    public void Drag()
    {
        Vector3 off = Input.mousePosition - vec3;
        //此处Input.mousePosition指鼠标拖拽结束的新位置  
        //减去刚才在按下时的位置，刚好就是鼠标拖拽的偏移量  
        vec3 = Input.mousePosition;//刷新下鼠标拖拽结束的新位置，用于下次拖拽的计算  
        pos = pos + off;//原来image所在的位置自然是要被偏移的  
        transform.GetComponent<RectTransform>().position = pos;//直接将自己刷新到新坐标  
    }

    //此函数接口将赋予给“弹出对话框”按钮的onClick事件  
    public void onShow()
    {
        gameObject.SetActive(true);//显示自己  
    }

    //此函数接口将赋予给“确认”按钮的onClick事件  
    public void onOK()
    {
        
                             //gameObject.Find("notice").GetComponent<Text>().text = "你点击了确定！";//并更改内容 
       // plane.SetActive(true);

        gameObject.SetActive(false);//让自己隐藏 
        switch (c.s)
        {
            case 1:
                {
Application.LoadLevel("Initial");
                    break;
                }
            case 2:
                Application.LoadLevel("Turkey_KST_alpha test");
                break;
            case 3:
                Application.LoadLevel("Ethiopia");
                break;
        }
    }
   
    //此函数接口将赋予给“取消”按钮的onClick事件  
    public void onCancel()
    {
        //text.SetActive(true);//让提示文本显示  
        //GameObject.Find("notice").GetComponent<Text>().text = "";//并更改内容  
        gameObject.SetActive(false);//让自己隐藏  
    }
    public  void Enterchange()
    {
        Application.LoadLevel("Enter");
    }
    public  void Enterai()
    {
        Application.LoadLevel("Storetu");
    }
}