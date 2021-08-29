using UnityEngine;
using System.Collections;
using UnityEngine.UI;//便于引用相关的UI组件

public class ItemUI : MonoBehaviour
{

    //处理UI变化
    public Item Item { get; private set; }//获取UI上的物品
    public int Amount { get; private set; }//获取UI图片中的物品数目


    #region UI Component
    private Image itemImage;//UI物品的图片，组件
    private Text amountText;//在UI图片上显示当前数目的Text组件

    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();//初始化组件
            }
            return amountText;
        }
    }
    #endregion

    private float targetScale = 1f;//目标缩放大小

    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);

    private float smoothing = 4;//动画平滑过渡时间

    void Update()
    {
        if (transform.localScale.x != targetScale)
        {
            //设置物品动画
            float scale = Mathf.Lerp(transform.localScale.x, targetScale,smoothing*Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < .02f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }
    //更新UI的图标显示
    public void SetItem(Item item,int amount = 1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        // update ui 
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
    //添加UI数目
    public void AddAmount(int amount=1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        //update ui 
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //减少物品数量
    public void ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;//物品更新时放大UI，用于动画
        this.Amount -= amount;
        //update ui 
        if (Item.Capacity > 1)//更新UI
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //设置物品个数
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        //update ui 
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //当前物品 跟 另一个物品 交换显示
    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.Item;
        int amountTemp = itemUI.Amount;
        itemUI.SetItem(this.Item, this.Amount);
        this.SetItem(itemTemp, amountTemp);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }


}
