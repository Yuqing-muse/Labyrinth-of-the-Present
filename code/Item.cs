using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemQuality Quality { get; set; }
    public ItemType Type { get; set; }
    public int Capacity { get; set; }//每一个格子中可存放物体的最大容量
    public string Description { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }//查找精灵路径

    //物品类的构造函数
    public Item()
    {
        this.ID = -1;//表示这是一个空的物品类
    }
    public Item(int id, string name, ItemType type, ItemQuality quality, string description, int capaticy, int buyPrice, int sellPrice, string sprite)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Quality = quality;
        this.Description = description;
        this.Capacity = capaticy;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
    }

    //关于物品各个类型的详细分类
    public enum ItemType
    {
        luxury,//奢饰品，包括的是各个地方的特产
        necessity//必需品，包括食物、木柴等生活用品
    }
    public enum ItemQuality
    {//同一类型物品之间按照质量指标来规定价钱
        Common,
        Rare,
        Legendary//传奇的
    }

    //把类中的数据转换为字符串，在提示框中显示
    public virtual string GetToolTipText()
    {

        string strItemType = "";
        switch (Type)
        {
            case ItemType.luxury:
                strItemType = "奢侈品";
                break;
            case ItemType.necessity:
                strItemType = "必需品";
                break;
        }
        string strItemQuality = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                strItemQuality = "一般材质";
                break;
            case ItemQuality.Rare:
                strItemQuality = "稀有材质";
                break;
            case ItemQuality.Legendary:
                strItemQuality = "传奇材质";
                break;
        }
        string color = ""; //用于设置提示框各个不同内容的颜色
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.Rare:
                color = "yellow";
                break;
            case ItemQuality.Legendary:
                color = "blue";
                break;
        }
        string text = string.Format("<color={0}>{1}</color>\n<color=yellow><size=10>介绍：{2}</size></color>\n<color=red><size=12>容量：{3}</size></color>\n<color=green><size=12>物品类型：{4}</size></color>\n<color=blue><size=12>物品质量：{5}</size></color>\n<color=orange>购买价格$：{6}</color>\n<color=red>出售价格$：{7}</color>", color, Name, Description, Capacity, strItemType, strItemQuality, BuyPrice, SellPrice);
        return text;
    }


}

