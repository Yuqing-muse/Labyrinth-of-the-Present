using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


// 物品槽

public class Slot : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler{

    public GameObject itemPrefab;
    //需要存储的物品预设
    //存放物品的时候，如果槽里已经有了，就下标（物体个数）++；如果没有，就实例化一个图片加入
    
    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)//如果这个物品槽下没有物品，那就实例化一个物品
        {
            GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
            itemGameObject.transform.SetParent(this.transform);//设置物品为物品槽的子物体
            itemGameObject.transform.localScale = Vector3.one;//正确保存物品的缩放比例
            itemGameObject.transform.localPosition = Vector3.zero;//设置物品的局部坐标，为了与其父亲物品槽相对应
            itemGameObject.GetComponent<ItemUI>().SetItem(item);//更新ItemUI
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();//默认添加一个
        }
    }


 
    // 得到当前物品槽存储的物品类型
   
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }

  
    // 得到物品的id
  
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    //判断物品槽是否为空，true表示当前数量大于等于容量，false表示当前数量小于容量
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;//当前的数量大于等于容量
    }

  
    //接口实现的方法，鼠标离开时触发
    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.childCount>0)
            InventoryManager.Instance.HideToolTip();
    }

    //鼠标覆盖时触发，在槽中有物品的前提下，显示信息框
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
        
    }
    //定义鼠标点击后的事件，左键点击拖拽【按下Ctrl键，放置当前鼠标上的物品的一个；没有按下Ctrl键，放置当前鼠标上物品的所有】
    //右键点击直接交易（出售或者买）
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)//右键直接交易
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();//当前UI图标
                if (currentItemUI.Item.Type.Equals("luxury") || currentItemUI.Item.Type.Equals("necessity"))//只有装备和物品才可以穿戴
                {
                    Item currentItem = currentItemUI.Item;//临时存储物品信息，防止物品UI销毁导致物品空指针
                    currentItemUI.ReduceAmount(1);//当前物品槽中的物品减少1个
                    if (currentItemUI.Amount <= 0)//物品槽中的物品没有了
                    {
                        DestroyImmediate(currentItemUI.gameObject);//立即销毁物品槽中的物品
                        InventoryManager.Instance.HideToolTip();
                    }
                   
                } //金钱兑换操作
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;
        // 自身是空 1,IsPickedItem ==true  pickedItem放在这个位置
                            // 按下ctrl      放置当前鼠标上的物品的一个
                            // 没有按下ctrl   放置当前鼠标上的物品的所有
                 //2,IsPickedItem==false  不做任何处理
        // 自身不是空 
                 //1,IsPickedItem==true
                        //自身的id==pickedItem.id  
                                    // 按下ctrl      放置当前鼠标上的物品的一个
                                    // 没有按下ctrl   放置当前鼠标上的物品的所有
                                                    //可以完全放下
                                                    //只能放下其中一部分
                        //自身的id!=pickedItem.id   pickedItem跟当前物品交换          
                 //2,IsPickedItem==false
                        //ctrl按下 取得当前物品槽中物品的一半
                        //ctrl没有按下 把当前物品槽里面的物品放到鼠标上
        if (transform.childCount > 0)//自身不是空
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();//取得当前的物品

            if (InventoryManager.Instance.IsPickedItem == false)//当前没有选中任何物品( 当前手上没有任何物品)当前鼠标上没有任何物品
            {
                if (Input.GetKey(KeyCode.LeftControl))//按下左边的ctrl键的时候，拾取物品栏中的一半物品
                {
                    int amountPicked = (currentItem.Amount + 1) / 2;//如果物品为偶数就拾取刚好一般，如果为奇数就拾取一半多一个
                    InventoryManager.Instance.PickupItem(currentItem.Item, amountPicked);
                    int amountRemained = currentItem.Amount - amountPicked;
                    if (amountRemained <= 0)
                    {//如果没有剩余的物品，就销毁对应的物品图标图片
                        Destroy(currentItem.gameObject);//销毁当前物品
                    }
                    else//还有剩余的物品的话，就对图标的下标数字进行修改
                    {
                        currentItem.SetAmount(amountRemained);
                    }
                }
                else
                {//没有按下左边ctrl
                    InventoryManager.Instance.PickupItem(currentItem.Item,currentItem.Amount);
                    Destroy(currentItem.gameObject);//复制完之后销毁当前物品槽中的物品
                }
            }else
            {//此时，鼠标上有未放置的图标  也就是说，现在是放置物品状态
             //1,IsPickedItem==true
             //自身的id==pickedItem.id  
             // 按下ctrl      放置当前鼠标上的物品的一个
             // 没有按下ctrl   放置当前鼠标上的物品的所有
             //可以完全放下
             //只能放下其中一部分
             //自身的id!=pickedItem.id   pickedItem跟当前物品交换          
                if (currentItem.Item.ID == InventoryManager.Instance.PickedItem.Item.ID)
                { //说明当前鼠标移动的物体和物体栏中的物体是同一类 是可以合并的
                    if (Input.GetKey(KeyCode.LeftControl))
                    {//按下左ctrl的时候，只可以放置一个
                        if (currentItem.Item.Capacity > currentItem.Amount)//当前物品槽还有容量
                        {//未满，还可以再放
                            currentItem.AddAmount();//增加物品栏中物品数量
                            InventoryManager.Instance.RemoveItem();//鼠标上物体个数减少一个
                        }
                        else
                        {//已经满了，不能再放了
                            return;
                        }
                    }
                    else//没有按下左ctrl的时候，全部放置
                    {
                        if (currentItem.Item.Capacity > currentItem.Amount)
                        {
                            int amountRemain = currentItem.Item.Capacity - currentItem.Amount;//物品槽还可以再放的数量
                            //因为是全部放置，所以还要判断在全部放置后，物品栏容量是否会超出预期容量
                            if (amountRemain >= InventoryManager.Instance.PickedItem.Amount)
                            {//可以放得下
                                currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickedItem.Amount);
                                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);//把鼠标上的所有物体都撤销（拿下）
                            }
                            else//只能放下一部分
                            {
                                currentItem.SetAmount(currentItem.Amount + amountRemain);//只能把空位塞满了
                                InventoryManager.Instance.RemoveItem(amountRemain);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else  //当前物品与物品栏中的不是同一类物品  pickedItem跟当前物品交换
                {   //保存当前鼠标捡起物品，用于和物品槽中的物品交换
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    //二者进行交换
                    //把当前鼠标上的物品放入物品槽
                    currentItem.SetItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                    InventoryManager.Instance.PickedItem.SetItem(item, amount); //把当前物品槽中的物品放在鼠标上
                }

            }
        }//离开放置物品状态

        else
        {
            //自身是空的情况，也就是是当前到达的物品栏中没有物体，所以只能进行放置操作
            if (InventoryManager.Instance.IsPickedItem == true)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    InventoryManager.Instance.RemoveItem();
                }
                else//放置当前鼠标上的所有物品
                 //需要依次实例化出相应的图标
                    {
                    for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    }
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                }
            }
            else//当前鼠标上没有物品
            {
                return;
            }

        }
    }
}
