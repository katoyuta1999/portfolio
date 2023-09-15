using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SupportEfect : MonoBehaviour

{
public static SupportEfect instance;
public static int enemyShield;
[SerializeField] Transform buffarea;
[SerializeField] Transform limitbuffarea;
[SerializeField] Transform playerHand;
public bool model =true;
public static int buffpow;
public static int[] buffEfect;
public static int bufflimit;

public static int playershield; 

    internal readonly string childname;

    public void Awake()
  {
    if(instance == null)
    {
        instance = this;
    }
  }
  
//被対象
public void TargetedMe(int cardID,List<int> category)
    {
      if(category.Contains(1))//shield
      {
        int shield = GetShield(cardID);
        if (PlayerParameter.buffDebuff.ContainsKey(2))
        {
          shield += PlayerParameter.buffDebuff[2]; 
        }
        if (PlayerParameter.buffDebuff.ContainsKey(4))
        {
          shield -= PlayerParameter.buffDebuff[4]; 
        }
        if (PlayerParameter.limitBuffDebuff.ContainsKey(2))
        {
          shield =(int)(shield*1.5); 
        }
        if (PlayerParameter.limitBuffDebuff.ContainsKey(4))
        {
          shield =(int)(shield*0.75);  
        }
        PlayerParameter.playerShield +=shield;

        ShieldWatcher.instance.SetShieldValue(PlayerParameter.playerShield);        
        GameManeger.instance.ShieldOnOff();
      }


      if(category.Contains(2))//buff
      {
        //atk =効果量
        buffEfect = GetBuffEfect(cardID);//1=自分の力を上げる。
        buffpow = GetBuff(cardID);//5pow
        
        for (int i = 0; i < buffEfect.Length; i++)
        {
          if (PlayerParameter.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += PlayerParameter.buffDebuff[buffEfect[i]];
              PlayerParameter.buffDebuff.Remove(buffEfect[i]);
          }
          
          PlayerParameter.buffDebuff.Add(buffEfect[i],buffpow);
        Transform thisfield = GetComponent<Transform>();

        GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,thisfield);
        
        var AAA = buffarea.GetComponentsInChildren<BuffDebuffController>().ToList();//自分のバフをリスト化
        var BBB = buffarea.GetComponentsInChildren<Text>().ToList();
        for (int j = 0; j < AAA.Count; j++)
        {
        if((buffpow>int.Parse(BBB[j].text))&&(AAA[j].model.icon==Geticon(buffEfect[i])))
        {
          BuffDebuffController.instance.DestroyBuffDebuff(AAA[j]);
        }
        }

        }
        
        //need watcher


      }
      if(category.Contains(3))
      {
        //Atk＝持続
        buffEfect = GetBuffEfect(cardID);//1=自分の力を上げる。
        bufflimit = GetBuff(cardID);//5turn
        for (int i = 0; i < buffEfect.Length; i++)
        {
          if (PlayerParameter.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              bufflimit  += PlayerParameter.limitBuffDebuff[buffEfect[i]];
              PlayerParameter.limitBuffDebuff.Remove(buffEfect[i]);
          }
          
          PlayerParameter.limitBuffDebuff.Add(buffEfect[i],bufflimit);
          Transform thisfield = GetComponent<Transform>();

          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,limitbuffarea,thisfield);


        var AAA = limitbuffarea.GetComponentsInChildren<BuffDebuffController>().ToList();//自分のバフをリスト化
        var BBB = limitbuffarea.GetComponentsInChildren<Text>().ToList();
        for (int j = 0; j < AAA.Count; j++)
        {
        if((buffpow>int.Parse(BBB[j].text))&&(AAA[j].model.icon==Geticon(buffEfect[i])))
        {
          BuffDebuffController.instance.DestroyBuffDebuff(AAA[j]);
        }
        }

        }
        

      }
      if(category.Contains(8))
      {
        int drawCard =GetDraw(cardID);
        for (int i = 0; i <drawCard; i++)
        {
          GameManeger.instance.DrawCard(playerHand);
        }
      }
    }

    private int GetBuff(int cardID)
    {
      CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
      int buff=CardEntity.recost;
      return buff;
    }

        private int[] GetBuffEfect(int cardID)
    {
      CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
      int[] buff=CardEntity.buffdebuff;
      return buff;
    }

    public int GetShield(int cardID)
  { 
    CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
    int shield=CardEntity.atk;
    return shield;
  }
  private UnityEngine.Sprite Geticon(int buffID)
    {
      BuffDebuffEntity BuffDebuffEntity = Resources.Load<BuffDebuffEntity>("BuffDebuffEntity/BuffDebuff"+buffID);

      UnityEngine.Sprite icon=BuffDebuffEntity.icon;
      return icon;
    }
public void SetenemyHP(int enemyHP)
  {
    var texts= gameObject.GetComponentsInChildren<Text>();
    texts[1].text = enemyHP.ToString();

  }
    public int GetDraw(int cardID)
  { 
  CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
    int shield=CardEntity.recost;
    return shield;
  }

}
