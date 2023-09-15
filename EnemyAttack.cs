using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour

{
public static Text enemyshield;
public static EnemyAttack instance;
public static int buffpow;
public static int[] buffEfect;
public static int bufflimit;
[SerializeField] Transform buffarea;
[SerializeField] Transform limitbuffarea;

    public void Awake()
  {
    if(instance == null)
    {
        instance = this;
    }
  }
  
//被対象
public void PleyerTargeted(int enemyCardID,Transform enemyTransform)
    {
      EnemyCardController enemycard = enemyTransform.GetComponentInChildren<EnemyCardController>();

      var enemyCategory = GetCategory(enemyCardID);
        var enemy = enemyTransform.GetComponent<EnemyBuff1>();
        var enemy2 = enemyTransform.GetComponent<EnemyBuff2>();
        var enemy3 = enemyTransform.GetComponent<EnemyBuff3>();



      if(enemyCategory.Contains(0))//攻撃カードは0
      {
        //攻撃する
        int damage=Attack(enemyCardID);
        int multipul=Multipul(enemyCardID);

        if(enemy!=null)
        {
          if (enemy.buffDebuff.ContainsKey(1))
          {
            damage += enemy.buffDebuff[1]; 
            damage = damage*multipul;
          }
          if (enemy.buffDebuff.ContainsKey(3))
          {
            damage -= enemy.buffDebuff[3]; 
            damage = damage*multipul;
          }
          if (enemy.limitBuffDebuff.ContainsKey(1))
          {
            damage = (int)(damage*1.5); 
            damage = damage*multipul;
          }
          if (enemy.limitBuffDebuff.ContainsKey(3))
          {
            damage = (int)(damage*0.75);
            damage = damage*multipul; 
          }
        }

        if(enemy2!=null)
        {
          if (enemy2.buffDebuff.ContainsKey(1))
          {
            damage += enemy2.buffDebuff[1]; 
            damage = damage*multipul;
          }
          if (enemy2.buffDebuff.ContainsKey(3))
          {
            damage -= enemy2.buffDebuff[3];
            damage = damage*multipul; 
          }
          if (enemy2.limitBuffDebuff.ContainsKey(1))
          {
            damage = (int)(damage*1.5);
            damage = damage*multipul; 
          }
          if (enemy2.limitBuffDebuff.ContainsKey(3))
          {
            damage = (int)(damage*0.75);
            damage = damage*multipul; 
          }
        }

        if(enemy3!=null)
        {
          if (enemy3.buffDebuff.ContainsKey(1))
          {
            damage += enemy3.buffDebuff[1];
            damage = damage*multipul; 
          }
          if (enemy3.buffDebuff.ContainsKey(3))
          {
            damage -= enemy3.buffDebuff[3]; 
            damage = damage*multipul;
          }
          if (enemy3.limitBuffDebuff.ContainsKey(1))
          {
            damage = (int)(damage*1.5); 
            damage = damage*multipul;
          }
          if (enemy3.limitBuffDebuff.ContainsKey(3))
          {
            damage = (int)(damage*0.75); 
            damage = damage*multipul;
          }
        }
        var nowHp = GameManeger.nowHp;
        int playerShield = PlayerParameter.playerShield;

        if (playerShield>=0)
        {
          playerShield -=damage;
          PlayerParameter.playerShield = playerShield;
          
          if (playerShield<=0)
          {
            nowHp +=playerShield;
            playerShield = 0;
            PlayerParameter.playerShield = playerShield;
            GameManeger.nowHp = nowHp;
          }
        }else
        {
          nowHp -=damage*multipul;;
        }

        GameManeger.nowHp = nowHp; 

        if (nowHp < 1)
        {
          Debug.Log("GameOver");
        }

      }

       if(enemyCategory.Contains(1))
      {
        //Debug.Log("敵が自分にシールドを付与");
        int shield = GetShield(enemyCardID);
        

         if(enemy!=null)
        {
          if (enemy.buffDebuff.ContainsKey(2))
          {
            shield += enemy.buffDebuff[2]; 
          }
          if (enemy.buffDebuff.ContainsKey(4))
          {
            shield -= enemy.buffDebuff[4]; 
          }
          if (enemy.limitBuffDebuff.ContainsKey(2))
          {
            shield = (int)(shield*1.5); 
          }
          if (enemy.limitBuffDebuff.ContainsKey(4))
          {
            shield = (int)(shield*0.75); 
          }
        }

        if(enemy2!=null)
        {
          if (enemy2.buffDebuff.ContainsKey(2))
          {
            shield += enemy2.buffDebuff[1]; 
          }
          if (enemy2.buffDebuff.ContainsKey(4))
          {
            shield -= enemy2.buffDebuff[3]; 
          }
          if (enemy2.limitBuffDebuff.ContainsKey(2))
          {
            shield = (int)(shield*1.5); 
          }
          if (enemy2.limitBuffDebuff.ContainsKey(4))
          {
            shield = (int)(shield*0.75); 
          }
        }

        if(enemy3!=null)
        {
          if (enemy3.buffDebuff.ContainsKey(2))
          {
            shield += enemy3.buffDebuff[2]; 
          }
          if (enemy3.buffDebuff.ContainsKey(4))
          {
            shield -= enemy3.buffDebuff[4]; 
          }
          if (enemy3.limitBuffDebuff.ContainsKey(2))
          {
            shield = (int)(shield*1.5); 
          }
          if (enemy3.limitBuffDebuff.ContainsKey(4))
          {
            shield = (int)(shield*0.75); 
          }
        }
        
        enemyshield = enemyTransform.GetChild(3).GetComponent<Text>();
        enemyshield.text = shield.ToString();

      }


      if(enemyCategory.Contains(12))
      {
        
         //ATｋ＝効果量
        buffEfect = GetBuffEfect(enemyCardID);//1=自分の力を上げる。
        buffpow = GetBuff(enemyCardID);//5pow

        
     
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
          buffarea = enemyTransform.GetChild(7);

          }else if (enemy==null&&enemy2!=null)
          {
          if (enemy2.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy2.buffDebuff[buffEfect[i]];
              enemy2.buffDebuff.Remove(buffEfect[i]);
          }
          enemy2.buffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemyTransform.GetChild(7);

          }else if(enemy2==null)
          {
            if (enemy3.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += enemy3.buffDebuff[buffEfect[i]];
              enemy3.buffDebuff.Remove(buffEfect[i]);
          }
          enemy3.buffDebuff.Add(buffEfect[i],buffpow);
          buffarea = enemyTransform.GetChild(7);

          }


        GameManeger.instance.CreateBuffDebuff(buffEfect[i],enemyCardID,enemyCategory,buffarea,enemyTransform);
        
        

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
      if(enemyCategory.Contains(13))
      {
        //Atk＝持続
        buffEfect = GetBuffEfect(enemyCardID);//1=自分の力を上げる。
        bufflimit = GetBuff(enemyCardID);//5turn
     
        for (int i = 0; i < buffEfect.Length; i++)
        {
         if(enemy!=null)
          {
          if (enemy.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              bufflimit  += enemy.limitBuffDebuff[buffEfect[i]];
              enemy.limitBuffDebuff.Remove(buffEfect[i]);
          }
          enemy.limitBuffDebuff.Add(buffEfect[i],bufflimit);
          limitbuffarea = enemyTransform.GetChild(8);
          }else if (enemy==null&&enemy2!=null)
          {
          if (enemy2.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              bufflimit  += enemy2.limitBuffDebuff[buffEfect[i]];
              enemy2.limitBuffDebuff.Remove(buffEfect[i]);
          }
          enemy2.limitBuffDebuff.Add(buffEfect[i],bufflimit);
          limitbuffarea = enemyTransform.GetChild(8);
          }else if(enemy2==null)
          {
            if (enemy3.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              bufflimit  += enemy3.limitBuffDebuff[buffEfect[i]];
              enemy3.limitBuffDebuff.Remove(buffEfect[i]);
          }
          enemy3.limitBuffDebuff.Add(buffEfect[i],bufflimit);
          limitbuffarea = enemyTransform.GetChild(8);
          }


          GameManeger.instance.CreateBuffDebuff(buffEfect[i],enemyCardID,enemyCategory,limitbuffarea,enemyTransform);
        var AAA = limitbuffarea.GetComponentsInChildren<BuffDebuffController>().ToList();//自分のバフをリスト化
        var BBB = limitbuffarea.GetComponentsInChildren<Text>().ToList();
        for (int j = 0; j < AAA.Count; j++)
        {
        if((bufflimit>int.Parse(BBB[j].text))&&(AAA[j].model.icon==Geticon(buffEfect[i])))
        {
          BuffDebuffController.instance.DestroyBuffDebuff(AAA[j]);
        }
        }     
        }
        
      }
      if(enemyCategory.Contains(4))
      {
        buffEfect = GetBuffEfect(enemyCardID);//1=自分の力を上げる。
        buffpow = GetBuff(enemyCardID);//5pow
        for (int i = 0; i < buffEfect.Length; i++)
        {
          if (PlayerParameter.buffDebuff.ContainsKey(buffEfect[i]))
          {
              buffpow  += PlayerParameter.buffDebuff[buffEfect[i]];
              PlayerParameter.buffDebuff.Remove(buffEfect[i]);
          }
          
          PlayerParameter.buffDebuff.Add(buffEfect[i],buffpow);
          Transform playerfield = GameObject.Find ("PlayerCell").transform;
          Transform playerbuffarea = GameObject.Find ("BuffArea").transform;

        GameManeger.instance.CreateBuffDebuff(buffEfect[i],enemyCardID,enemyCategory,playerbuffarea,playerfield);
        
        var AAA = playerbuffarea.GetComponentsInChildren<BuffDebuffController>().ToList();//プレイヤーのバフをリスト化
        var BBB = playerbuffarea.GetComponentsInChildren<Text>().ToList();
        for (int j = 0; j < AAA.Count; j++)
        {
        if((buffpow>int.Parse(BBB[j].text))&&(AAA[j].model.icon==Geticon(buffEfect[i])))
        {
          BuffDebuffController.instance.DestroyBuffDebuff(AAA[j]);
        }
        }
        }

      }
      if(enemyCategory.Contains(5))
      {
        buffEfect = GetBuffEfect(enemyCardID);//1=自分の力を上げる。
        bufflimit = GetBuff(enemyCardID);//5turn
        for (int i = 0; i < buffEfect.Length; i++)
        {
          if (PlayerParameter.limitBuffDebuff.ContainsKey(buffEfect[i]))
          {
              bufflimit  += PlayerParameter.limitBuffDebuff[buffEfect[i]];
              PlayerParameter.limitBuffDebuff.Remove(buffEfect[i]);
          }
          
          PlayerParameter.limitBuffDebuff.Add(buffEfect[i],bufflimit);
          Transform playerfield = GameObject.Find ("PlayerCell").transform;
          Transform playerbuffarea = GameObject.Find ("LimitBuffArea").transform;

        GameManeger.instance.CreateBuffDebuff(buffEfect[i],enemyCardID,enemyCategory,playerbuffarea,playerfield);
        
        var AAA = playerbuffarea.GetComponentsInChildren<BuffDebuffController>().ToList();//プレイヤーのバフをリスト化
        var BBB = playerbuffarea.GetComponentsInChildren<Text>().ToList();
        for (int j = 0; j < AAA.Count; j++)
        {
        if((bufflimit>int.Parse(BBB[j].text))&&(AAA[j].model.icon==Geticon(buffEfect[i])))
        {
          BuffDebuffController.instance.DestroyBuffDebuff(AAA[j]);
        }
        }
        }


      }


      enemycard.DestroyEnemyCard(enemycard);

    }

public int Attack(int enemyCardID)
  { 
    EnemyCardEntity EnemyCardEntity = 
    Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+enemyCardID);
    int damage=EnemyCardEntity.atk;
    return damage;
  }
  public int Multipul(int enemyCardID)
  { 
    EnemyCardEntity EnemyCardEntity = 
    Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+enemyCardID);
    int damage=EnemyCardEntity.multipul;
    return damage;
  }
public List<int> GetCategory(int enemyCardID)
  {
    EnemyCardModel model;
    model = new EnemyCardModel(enemyCardID);
    var enemycategory = model.category.ToList();

    return enemycategory;

  }

public int GetShield(int enemyCardID)
  { 
    EnemyCardEntity EnemyCardEntity = 
    Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+enemyCardID);
    int shield=EnemyCardEntity.atk;
    return shield;
  }
  public void SetEnemyShield(int enemyShield)
  {
    var texts= this.GetComponentsInChildren<Text>();
    texts[2].text = enemyShield.ToString();

  }
  private int GetBuff(int cardID)
  {
    EnemyCardEntity EnemyCardEntity = Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+cardID);
    int buff=EnemyCardEntity.atk;
    return buff;
  }

private int[] GetBuffEfect(int cardID)
  {
    EnemyCardEntity EnemyCardEntity = Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+cardID);
    int[] buff=EnemyCardEntity.buffdebuff;
    return buff;
  }
  private UnityEngine.Sprite Geticon(int buffID)
    {
      BuffDebuffEntity BuffDebuffEntity = Resources.Load<BuffDebuffEntity>("BuffDebuffEntity/BuffDebuff"+buffID);

      UnityEngine.Sprite icon=BuffDebuffEntity.icon;
      return icon;
    }

}
