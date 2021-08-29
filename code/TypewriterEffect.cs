using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//打字机效果的脚本
[RequireComponent(typeof(Text))]
[AddComponentMenu("Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
    public UnityEvent myEvent;
    public int charsPerSecond = 0;
    public AudioClip mAudioClip;             // 打字的声音，不是没打一个字播放一下，开始的时候播放结束就停止播放  
    private bool isActive = false;

    private float timer;
    private string words;
    private Text mText;

    void Start()
    {
        if (myEvent == null)
            myEvent = new UnityEvent();//新建一个事件

        words = GetComponent<Text>().text;//读入文本内容
        GetComponent<Text>().text = string.Empty;
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(1, charsPerSecond);//对打字速度的平滑处理
        mText = GetComponent<Text>();//获取一个组件
    }

    void ReloadText()
    {//提前载入需要的内容
        words = GetComponent<Text>().text;
        mText = GetComponent<Text>();
    }

    public void OnStart()
    {
        ReloadText();
        isActive = true;
    }

    void OnStartWriter()
    {
        if (isActive)
        {
            try
            {
                mText.text = words.Substring(0, (int)(charsPerSecond * timer));//控制组件上输出文本的内容
                timer += Time.deltaTime;
            }
            catch (Exception)
            {
                OnFinish();
            }
        }
    }

    void OnFinish()
    {//文本完全输出完成后的操作
        isActive = false;
        timer = 0;
        GetComponent<Text>().text = words;
        try
        {
            myEvent.Invoke();
        }
        catch (Exception)
        {
            Debug.Log("问题");
        }
    }

    void Update()
    {
        OnStartWriter();
    }
}