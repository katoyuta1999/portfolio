using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyEntity",menuName ="Create Enemy Entity")]
public class EnemyEntity : ScriptableObject
{
    public new string name;
    public int hp;
    public Sprite icon;
    public int shield;
    public  Dictionary<int,int> buffDebuff = new Dictionary<int,int>()
        {  };
    public  Dictionary<int,int> limitBuffDebuff = new Dictionary<int,int>()
        {  }; 
    public List<int> moveList1;//enemyCardIDを入力することで上から行動をパターン化できる。
    public List<int> moveList2;//Hpが下がったときなどのパターン変化用。
     public List<int> moveList3;

}
