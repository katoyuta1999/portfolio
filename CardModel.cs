using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//カードデータとその処理
public class CardModel
{
    public string names;
    public int hp;
    public int atk;
    public int cost;
    public int id;
    public int[] area;
    public int[] limitArea;
    public Sprite icon;
    public bool canAttack = false;
    public static CardModel instance;
    public bool canUse = false;
    public bool PlayerCard = false;
    public int[] category;
    public int[] buffdebuff;
    public string text;


    public CardModel(int cardID, bool playerCard)
    {
        CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
        
        names = CardEntity.name;
        id = CardEntity.id;
        hp = CardEntity.hp;
        atk = CardEntity.atk;
        cost = CardEntity.cost;
        icon = CardEntity.icon;
        area = CardEntity.area;
        limitArea =CardEntity.limitArea;
        category = CardEntity.category;
        buffdebuff = CardEntity.buffdebuff;
        text = CardEntity.text;
        PlayerCard = playerCard;


        
    }
    
    
}