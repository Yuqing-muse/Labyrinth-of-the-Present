using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class Inventory : MonoBehaviour {

    protected Slot[] slotList;//存放物品槽的数组
   //控制背包的显示和隐藏相关变量
    private float targetAlpha = 1;//显示目标值
    private float smoothing = 4;//渐变平滑速度
    private CanvasGroup canvasGroup;
    

    public virtual void Start ()
    {//声明为虚函数，方便子类重写
        slotList = GetComponentsInChildren<Slot>();//关于放物品的物品槽类
        canvasGroup = GetComponent<CanvasGroup>();
	}


    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < .01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    //根据Id存储物品
    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    //实际的存储物品
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物品的id不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)//说明已经没有空的物品槽了
            {
                Debug.LogWarning("没有空的物品槽");
                return false;//存储失败
            }
            else
            {
                slot.StoreItem(item);//把物品存储到这个空的物品槽里面
            }
        }
        else//这个物品可以放多个
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else //没有找到符合条件的物品槽，那就找一个没有存放物品的物品槽去存放物品
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }

    //寻找空的物品槽
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)//物品槽下面无子类，说明该物品槽为空，返回它即可
            {
                return slot;
            }
        }
        return null;
    }
    //寻找相同id的物品槽
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID &&slot.IsFilled()==false )
            {//物品槽不为空，且是相同类型的，并且没有放满
                return slot;
            }
        }
        return null;
    }

    //面板的显示方法
    public void Show()
    {
        canvasGroup.blocksRaycasts = true;//面板显示时为可交互状态
        targetAlpha = 1;
    }
    //面板的隐藏方法
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;//面板隐藏后为不可交互状态
        targetAlpha = 0;
    }

    //控制面板的显示及隐藏关系
    public void DisplaySwitch()
    {
        if (targetAlpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    #region save and load
    //控制物品信息的保存（ID，Amount数量）
    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();//用来保存物品信息的字符串
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.Item.ID + ","+itemUI.Amount+"-");//用逗号分隔一个物品中的ID和数量，用 - 分隔多个物品
            }
            else
            {
                sb.Append("0-");//如果物品槽里没有物品就是0
            }
        }
        PlayerPrefs.SetString(this.gameObject.name, sb.ToString());//保存字符串数据
    }

    //控制物品信息的加载（根据ID，Amount数量）
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;//判断保存的这个关键码Key是否存在,不存在就不做处理
        string str = PlayerPrefs.GetString(this.gameObject.name);//获取上面保存的字符串数据
        //按照  -  分隔多个物品
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length-1; i++)//长度减1是因为最后一个字符是 “-”，不需要取它
        {
            string itemStr = itemArray[i];
            if (itemStr != "0")
            {
                //print(itemStr);
                string[] temp = itemStr.Split(',');//按照逗号分隔这个物品的信息（ID和Amoun数量）
                int id = int.Parse(temp[0]);
                Item item = InventoryManager.Instance.GetItemById(id);//通过物品ID得到该物品
                int amount = int.Parse(temp[1]);
                for (int j = 0; j < amount; j++)//执行Amount次StoreItem方法，一个一个的存
                {
                    slotList[i].StoreItem(item);
                }
            }
        }
    }
    #endregion
}
