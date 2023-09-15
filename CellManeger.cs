using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CellManeger : MonoBehaviour
{
public static CellManeger instance;
          public void Awake()
  {
    if(instance == null)
    {
        instance = this;
    }
  }
    //対象とされているセルを覚える
    //public string targetcell =AttackCard.instance.cell;
    
    //カードID,範囲を読み取る


    //string area = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);

    //     area = CardEntity.area;
    //     limitArea =CardEntity.limitArea;


        
    
    // public int area ;
    // area = CardEntity.area;CellEfect


    //対象セルを確認
    public void TellTarget(int cardID,int[] cardArea,string targetCell,List<int> category)
    {
        var attackArea = new List<int>();
        //基準のセルを確定させる
        int basecell = int.Parse(targetCell);
        if(basecell<=3)
        {
            basecell -=7 ;
        }
        else if((basecell>=4) && (basecell<=6))
        {
            basecell -=5 ;
        }
        else if(basecell<=9)
        {
            basecell -=3 ;
        }

        //攻撃範囲の確定
        for (int i = 0; i < cardArea.Length; i++)
        {
            attackArea.Add(cardArea[i]+basecell);
        }
       
    // for (int i = 0; i < AAA.Length; i++)
    //     {
    //         Array.Resize(ref targetCells, AAA.Length);
    //         targetCells[i] = "EnemyCell"+(AAA[i]);
    //     }


        //各セルへ連絡する。カードIDを添えて。
        CellEfect[] child = GetComponentsInChildren<CellEfect>();
        
        for (int i = 0; i < cardArea.Length; i++)
        {
            if(!((attackArea[i]%5==0)||(attackArea[i]%5==4)))
            {   
                if((attackArea[i]>0)&&(attackArea[i]<6))
                {
                    Debug.Log(child[attackArea[i]-1]+"マス目を攻撃します。");
                    child[attackArea[i]-1].Targeted(cardID,category);
                    
                }
                else if((attackArea[i]>5)&&(attackArea[i]<11))
                {
                   Debug.Log(child[attackArea[i]-3]+"マス目を攻撃します。");
                   child[attackArea[i]-3].Targeted(cardID,category);
                }
                else if(attackArea[i]>10&&(attackArea[i]<15))
                {
                   Debug.Log(child[attackArea[i]-5]+"マス目を攻撃します。");
                   child[attackArea[i]-5].Targeted(cardID,category);
                }
            }
        }
    }
}
