using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyCardEntity",menuName ="Create EnemyCard Entity")]
public class EnemyCardEntity : ScriptableObject
{
    public new string name;
    public int id;
    public int atk;
    public int multipul;
    public int icon1;//Atk
    public int icon2;//Def
    public int icon3;//Buff
    public int icon4;//Debuff
    public int[] category;//0=atk 1=sup 2=buff 3=limitbuff
    public int[] buffdebuff;

}
