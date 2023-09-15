using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CellEfect : MonoBehaviour

{
[SerializeField] Transform playerHand;
public static CellEfect instance;
public static int enemyShield;
public static int buffpow;
public static int[] buffEfect;
public static int bufflimit;
public bool model =true;
public Transform buffarea;

    internal readonly string childname;

    public void Awake()
  {
    if(instance == null)
    {
        instance = this;
    }
  }
  
//被対象
public void Targeted(int cardID ,List<int> category)
    {
        var enemy = GetComponentInChildren<EnemyBuff1>();
        var enemy2 = GetComponentInChildren<EnemyBuff2>();
        var enemy3 = GetComponentInChildren<EnemyBuff3>();
        buffEfect = GetBuffEfect(cardID);
        buffpow = GetBuff(cardID);
        
      if(category.Contains(0))//攻撃カード0
      {
        //攻撃する
        int damage=Attack(cardID);
        if (PlayerParameter.buffDebuff.ContainsKey(1))
        {
          damage += PlayerParameter.buffDebuff[1]; 
        }
        if (PlayerParameter.buffDebuff.ContainsKey(3))
        {
          damage -= PlayerParameter.buffDebuff[3]; 
        }
        if (PlayerParameter.limitBuffDebuff.ContainsKey(1))
        {
          damage = (int)(damage*1.5); 
        }
        if (PlayerParameter.limitBuffDebuff.ContainsKey(3))
        {
          damage = (int)(damage*0.75); 
        }
        if(enemy!=null)
          {
            if (enemy.limitBuffDebuff.ContainsKey(5))
            damage = (int)(damage*1.5);

          }
          if (enemy==null&&enemy2!=null)
          {
            if (enemy2.limitBuffDebuff.ContainsKey(5))
            damage = (int)(damage*1.5);
          }
          if(enemy3!=null)
          {
            if (enemy3.limitBuffDebuff.ContainsKey(5))
            damage = (int)(damage*1.5);
          }

        EnemySpawn enemys = GetComponentInChildren<EnemySpawn>();
        if(enemys==null)
        {
          return;//対象がない場合攻撃は無に消える。
        }




        var texts= gameObject.GetComponentsInChildren<Text>();
        int enemyHP = int.Parse(texts[1].text);

        enemyShield = int.Parse(texts[2].text);


        if (enemyShield>=0)
        {
          enemyShield -=damage;
          SetEnemyShield(enemyShield);
          if (enemyShield<=0)
          {
            enemyHP +=enemyShield;
            SetEnemyShield(enemyShield);
          }

        }else
        {
          enemyHP -=damage;
        }
        

        SetenemyHP(enemyHP);//敵のHPを更新。
        GameManeger.instance.EnemyShieldOnOff(enemys.transform);

        if (enemyHP < 1)
        {
          enemys.DestroyEnemy(enemys);
        }

      }
      if(category.Contains(1))//debuff
      {
        Debug.Log("自分が敵にshieldを与える事はないのでパスと思ったが自分には付与するので要る");
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
        
        for (int i = 0; i < buffEfect.Length; i++)
        {
          if(enemy!=null)
          {
          if (enemy.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy.buffDebuff[buffEfect[i]];
              enemy.buffDebuff.Remove(buffEfect[i]);
          }
          enemy.buffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemy.transform.GetChild(7);
          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,enemy.transform);

          }else if (enemy==null&&enemy2!=null)
          {
          if (enemy2.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy2.buffDebuff[buffEfect[i]];
              enemy2.buffDebuff.Remove(buffEfect[i]);
          }
          enemy2.buffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemy2.transform.GetChild(7);
          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,enemy2.transform);
          }else if(enemy2==null)
          {
            if (enemy3.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy3.buffDebuff[buffEfect[i]];
              enemy3.buffDebuff.Remove(buffEfect[i]);
          }
          enemy3.buffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemy3.transform.GetChild(7);
          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,enemy3.transform);

          }


        
        
        

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
      }


      if(category.Contains(3))//limitbuff
      {
        
         for (int i = 0; i < buffEfect.Length; i++)
        {
          if(enemy!=null)
          {
          if (enemy.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy.limitBuffDebuff[buffEfect[i]];
              enemy.limitBuffDebuff.Remove(buffEfect[i]);
          }
          enemy.limitBuffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemy.transform.GetChild(7);
          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,enemy.transform);

          }else if (enemy==null&&enemy2!=null)
          {
          if (enemy2.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy2.limitBuffDebuff[buffEfect[i]];
              enemy2.limitBuffDebuff.Remove(buffEfect[i]);
          }
          enemy2.limitBuffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemy2.transform.GetChild(7);
          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,enemy2.transform);

          }else if(enemy2==null)
          {
            if (enemy3.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy3.limitBuffDebuff[buffEfect[i]];
              enemy3.limitBuffDebuff.Remove(buffEfect[i]);
          }
          enemy3.limitBuffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemy3.transform.GetChild(7);
          GameManeger.instance.CreateBuffDebuff(buffEfect[i],cardID,category,buffarea,enemy3.transform);

          }


        
        
        

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
      }         
      if(category.Contains(4))//up
      {
        var cell = GetComponent<Transform>();
        var enemycell = Regex.Replace(cell.ToString(), @"[^0-9]", "");
        var i = int.Parse(enemycell)-3 ;
        
        var movecell = GameObject.Find("EnemyCell"+i.ToString());
        var movecellchild =movecell.GetComponentInChildren<EnemySpawn>();
        var enemyparent = GetComponentInChildren<EnemySpawn>();
        
        if (3<int.Parse(enemycell))
        {
          if (movecellchild==null)
          {
            enemyparent.defaultParent = movecell.transform;
            enemyparent.transform.SetParent(enemyparent.defaultParent,false);

          }
        }

      }
      if(category.Contains(5))//down
      {
        var cell = GetComponent<Transform>();
        var enemycell = Regex.Replace(cell.ToString(), @"[^0-9]", "");
        var i = int.Parse(enemycell)+3 ;
        
        var movecell = GameObject.Find("EnemyCell"+i.ToString());
        var movecellchild =movecell.GetComponentInChildren<EnemySpawn>();
        var enemyparent = GetComponentInChildren<EnemySpawn>();
        
        if (7>int.Parse(enemycell))
        {
          if (movecellchild==null)
          {
            enemyparent.defaultParent = movecell.transform;
            enemyparent.transform.SetParent(enemyparent.defaultParent,false);

          }
        }

      }
      if(category.Contains(6))//push
      {
        var cell = GetComponent<Transform>();
        var enemycell = Regex.Replace(cell.ToString(), @"[^0-9]", "");
        var i = int.Parse(enemycell)+1 ;
        
        var movecell = GameObject.Find("EnemyCell"+i.ToString());
        var movecellchild =movecell.GetComponentInChildren<EnemySpawn>();
        var enemyparent = GetComponentInChildren<EnemySpawn>();
        
        if (3!=int.Parse(enemycell))
        {
          if (6!=int.Parse(enemycell))
          {
          if (9!=int.Parse(enemycell))
          {
          if (movecellchild==null)
          {
            enemyparent.defaultParent = movecell.transform;
            enemyparent.transform.SetParent(enemyparent.defaultParent,false);

          }
          }
          }
        }

      }
      if(category.Contains(7))//pull
      {
        var cell = GetComponent<Transform>();
        var enemycell = Regex.Replace(cell.ToString(), @"[^0-9]", "");
        var i = int.Parse(enemycell)-1 ;
        
        var movecell = GameObject.Find("EnemyCell"+i.ToString());
        var movecellchild =movecell.GetComponentInChildren<EnemySpawn>();
        var enemyparent = GetComponentInChildren<EnemySpawn>();
        
         if (1!=int.Parse(enemycell))
        {
          if (4!=int.Parse(enemycell))
          {
          if (7!=int.Parse(enemycell))
          {
          if (movecellchild==null)
          {
            enemyparent.defaultParent = movecell.transform;
            enemyparent.transform.SetParent(enemyparent.defaultParent,false);

          }
        }
        }
        }


      }
      if(category.Contains(8))
      {
        int drawCard =GetShield(cardID);
        for (int i = 0; i <drawCard; i++)
        {
          GameManeger.instance.DrawCard(playerHand);
        }
      }
    }

public int Attack(int cardID)
  { 
    CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
    int damage=CardEntity.atk;
    return damage;
  }
public void SetenemyHP(int enemyHP)
  {
    var texts= gameObject.GetComponentsInChildren<Text>();
    texts[1].text = enemyHP.ToString();

  }
public void SetEnemyShield(int enemyshield)
  {
    var texts= gameObject.GetComponentsInChildren<Text>();
    texts[2].text = enemyShield.ToString();

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
  private UnityEngine.Sprite Geticon(int buffID)
  {
    BuffDebuffEntity BuffDebuffEntity = Resources.Load<BuffDebuffEntity>("BuffDebuffEntity/BuffDebuff"+buffID);

    UnityEngine.Sprite icon=BuffDebuffEntity.icon;
    return icon;
  }
  public int GetShield(int cardID)
  { 
  CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
    int shield=CardEntity.recost;
    return shield;
  }
}
