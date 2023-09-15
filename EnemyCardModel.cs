using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カードデータとその処理
public class EnemyCardModel
{
    public string name;
    public int id;
    public int atk;
    public int multipul;
    public int icon1;//Atk
    public int icon2;//Def
    public int icon3;//Buff
    public int icon4;//Debuff
    public int[] category;//0=atk 1=sup 2=buff 3=limitbuff
    public int[] buffdebuff;


    public EnemyCardModel(int EnemyCardID)
    {
        EnemyCardEntity EnemyCardEntity = Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+EnemyCardID);
        
        name = EnemyCardEntity.name;
        id = EnemyCardEntity.id;
        atk = EnemyCardEntity.atk;
        icon1 = EnemyCardEntity.icon1;
        icon2 = EnemyCardEntity.icon2;
        icon3 = EnemyCardEntity.icon3;
        icon4 = EnemyCardEntity.icon4;
        category = EnemyCardEntity.category;
        buffdebuff = EnemyCardEntity.buffdebuff;
        multipul = EnemyCardEntity.multipul;


        
    }
    
    
}