using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{//重写父类Slot的鼠标点击方法，但是什么都不用做，商贩只管卖物品
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right &&InventoryManager.Instance.IsPickedItem==false )
        {//在商贩那里点击右键直接购买物品
            if (transform.childCount > 0)//首先商贩得有物品,其次鼠标上没有物品
            {
                Item currentItem = transform.GetChild(0).GetComponent<ItemUI>().Item;//取得当前点击的要买的物品
                transform.parent.parent.SendMessage("BuyItem", currentItem);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left && InventoryManager.Instance.IsPickedItem == true)
        {//在背包鼠标左键拖动售卖物品
            transform.parent.parent.SendMessage("SellItem");
        }
        
    }
}
