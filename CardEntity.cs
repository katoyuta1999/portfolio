using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="CardEntity",menuName ="Create Card Entity")]
public class CardEntity : ScriptableObject
{
    public new string name;
    public int id;
    public int hp;
    public int atk;
    public int cost;
    public int recost;
    public Sprite icon;
    public int[] area;
    public int[] limitArea;
    public int[] category;//0=atk 1=sup 2=buff 3=limitbuff
    public int[] buffdebuff;
    public int rarity ;// 0＝初期カード　1　2　3
    [SerializeField, TextArea(1,3)] public string text;
    
}
