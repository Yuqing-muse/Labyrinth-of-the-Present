using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//提示框类  实现提示框的各种功能
public class ToolTip : MonoBehaviour {

    private Text toolTipText;//提示框的父物体，用来控制提示框的大小
    private Text contentText;//提示框的子Text,用来控制文字的显示
    private CanvasGroup canvasGroup;//组件，用来控制提示框的隐藏和显示

    private float targetAlpha = 0;//设置透明度，0表示隐藏，1显示

    public float smoothing = 1;//使透明度变化的更加平滑的参数值


    void Start()
    {
        toolTipText = GetComponent<Text>();
        contentText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha,smoothing*Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {//如果当前提示框的Alpha值与目标Alpha值相差很小，那就设置为目标值
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    //提示框的显示方法
    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }

    //提示框的隐藏方法
    public void Hide()
    {
        targetAlpha = 0;
    }

    //设置提示框自身的位置
    public void SetLocalPotion(Vector3 position)
    {
        transform.localPosition = position;
    }
	
}
