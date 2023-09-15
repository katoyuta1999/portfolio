using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemyデータとその処理
public class EnemyModel
{
    public string name;
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
    
    public EnemyModel(int EnemyID)
    {
        EnemyEntity EnemyEntity
         = 
        Resources.Load<EnemyEntity>("EnemyEntityList/Enemy"+EnemyID);
        
        name = EnemyEntity.name;
        hp = EnemyEntity.hp;
        icon = EnemyEntity.icon;
        shield = EnemyEntity.shield;
        moveList1 = EnemyEntity.moveList1;
        moveList2 = EnemyEntity.moveList2;
        moveList3 = EnemyEntity.moveList3;
        buffDebuff = EnemyEntity.buffDebuff;
        limitBuffDebuff =EnemyEntity.limitBuffDebuff;


        
    }
    
    
}