using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カードデータとその処理
public class BuffDebuffModel
{
    public string name;
    public int ID;
    public int buffEfect;
    public Sprite icon;


    public BuffDebuffModel(int buffID)
    {
        BuffDebuffEntity BuffDebuffEntity = Resources.Load<BuffDebuffEntity>("BuffDebuffEntity/BuffDebuff"+buffID);
        
        name = BuffDebuffEntity.name;
        ID = BuffDebuffEntity.ID;
        buffEfect = BuffDebuffEntity.buffEfect;
        icon = BuffDebuffEntity.icon;
        


        
    }
    
    
}