using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuffDebuffView : MonoBehaviour
{
    [SerializeField] Text buffEfectText;
    [SerializeField] Image iconImage;
    GameObject buffArea;
    GameObject limitBuffArea;
    
    public static int buffpow;
    public static int buffpow2;
    public static int[] buffEfect;
    public static int bufflimit;
    
    public void Show(BuffDebuffModel BuffDebuffModel,int cardID ,List<int> category,Transform field)
    {   
        //category field
        category.ToList();
        if(category.Contains(2))
        {
            if(field.ToString().Contains("PlayerCell"))
            {
                buffEfect = GetBuffEfect(cardID);//1=自分の力を上げる。
                buffpow = GetBuff(cardID);//5pow
                buffArea = GameObject.Find("BuffArea");
                var AAA = buffArea.GetComponentsInChildren<BuffDebuffController>().ToList();//自分のバフをリスト化
                for (int i = 0; i < buffEfect.Length; i++)
                {
                if (PlayerParameter.buffDebuff.ContainsKey(buffEfect[i]))
                    {
                        buffpow2  = PlayerParameter.buffDebuff[buffEfect[i]];
                    }
                }
                for (int i = 0; i < AAA.Count; i++)
                {
                        if(AAA[i].model.icon==BuffDebuffModel.icon&&buffpow<buffpow2)
                        {
                            buffEfectText.text = buffpow2.ToString();
                            iconImage.sprite = BuffDebuffModel.icon; 
                        }
                        else
                        buffEfectText.text = buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon; 
                    
                }

            }
            else
            {
                    buffEfect = GetBuffEfect(cardID);
                    buffpow = GetBuff(cardID);
                
                  
                var enemy = field.GetComponent<EnemyBuff1>();
                var enemy2 = field.GetComponent<EnemyBuff2>();
                var enemy3 = field.GetComponent<EnemyBuff3>();
                
                for (int i = 0; i < buffEfect.Length; i++)
                {
                if(enemy!=null)
                {
                if (enemy.buffDebuff.ContainsKey(buffEfect[i]))
                {
                        buffpow  = enemy.buffDebuff[buffEfect[i]];

                }
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;

                }else if (enemy==null&&enemy2!=null)
                {
                if (enemy2.buffDebuff.ContainsKey(buffEfect[i]))
                {
                        buffpow  = enemy2.buffDebuff[buffEfect[i]];
                        
                }
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;

                }else if(enemy2==null)
                {
                    if (enemy3.buffDebuff.ContainsKey(buffEfect[i]))
                {
                    buffpow  = enemy3.buffDebuff[buffEfect[i]];
                    
                }
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;
                }
                    
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;
                    }
            }


        }
        if(category.Contains(3))
        {
            if(field.ToString()=="PlayerCell (UnityEngine.RectTransform)")
            {
                buffEfect = GetBuffEfect(cardID);//1=自分の力を上げる。
                buffpow = GetBuff(cardID);//5 limit
                limitBuffArea = GameObject.Find("limitBuffArea");
                var AAA = limitBuffArea.GetComponentsInChildren<BuffDebuffController>().ToList();//自分のバフをリスト化
                for (int i = 0; i < buffEfect.Length; i++)
                {
                if (PlayerParameter.limitBuffDebuff.ContainsKey(buffEfect[i]))
                    {
                        buffpow2  = PlayerParameter.limitBuffDebuff[buffEfect[i]];
                    }
                }
                for (int i = 0; i < AAA.Count; i++)
                {
                        if(AAA[i].model.icon==BuffDebuffModel.icon&&buffpow<buffpow2)
                        {
                            buffEfectText.text = buffpow2.ToString();
                            iconImage.sprite = BuffDebuffModel.icon; 
                        }
                        else
                        buffEfectText.text = buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon; 
                    
                }

            }else
            {
                    buffEfect = GetBuffEfect(cardID);
                    buffpow = GetBuff(cardID);
                    
                
                   
                
                var enemy = field.GetComponent<EnemyBuff1>();
                var enemy2 = field.GetComponent<EnemyBuff2>();
                var enemy3 = field.GetComponent<EnemyBuff3>();
                
                for (int i = 0; i < buffEfect.Length; i++)
                {
                if(enemy!=null)
                {
                if (enemy.limitBuffDebuff.ContainsKey(buffEfect[i]))
                {
                        buffpow  = enemy.limitBuffDebuff[buffEfect[i]];
                        

                }
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;

                }else if (enemy==null&&enemy2!=null)
                {
                if (enemy2.limitBuffDebuff.ContainsKey(buffEfect[i]))
                {
                        buffpow  = enemy2.limitBuffDebuff[buffEfect[i]];
                        
                }
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;

                }else if(enemy2==null)
                {
                    if (enemy3.limitBuffDebuff.ContainsKey(buffEfect[i]))
                {
                    buffpow  = enemy3.limitBuffDebuff[buffEfect[i]];
                    
                }
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;
                }
                    
                        buffEfectText.text =buffpow.ToString();
                        iconImage.sprite = BuffDebuffModel.icon;
                    }


            }            

        }
        if(category.Contains(12))
        {
            buffEfect = GetEnemyBuffEfect(cardID);//1=自分の力を上げる。
            buffpow = GetEnemyBuff(cardID);//5pow   
            
                
            var enemy = field.GetComponent<EnemyBuff1>();
            var enemy2 = field.GetComponent<EnemyBuff2>();
            var enemy3 = field.GetComponent<EnemyBuff3>();
            
            for (int i = 0; i < buffEfect.Length; i++)
            {
            if(enemy!=null)
            {
            if (enemy.buffDebuff.ContainsKey(buffEfect[i]))
            {
                    buffpow  = enemy.buffDebuff[buffEfect[i]];
            Debug.Log(buffpow);
            }
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;

            }else if (enemy==null&&enemy2!=null)
            {
            if (enemy2.buffDebuff.ContainsKey(buffEfect[i]))
            {
                    buffpow  = enemy2.buffDebuff[buffEfect[i]];
                    Debug.Log(buffpow);
            }
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;

            }else if(enemy2==null)
            {
                if (enemy3.buffDebuff.ContainsKey(buffEfect[i]))
            {
                buffpow  = enemy3.buffDebuff[buffEfect[i]];
                Debug.Log(buffpow);
            }
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;
            }
                
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;
                }
        }
        if(category.Contains(13))
        {
            buffEfect = GetEnemyBuffEfect(cardID);//1=自分の力を上げる。
            buffpow = GetEnemyBuff(cardID);//5pow 
                
            
            var enemy = field.GetComponent<EnemyBuff1>();
            var enemy2 = field.GetComponent<EnemyBuff2>();
            var enemy3 = field.GetComponent<EnemyBuff3>();
            
            for (int i = 0; i < buffEfect.Length; i++)
            {
            if(enemy!=null)
            {
            if (enemy.limitBuffDebuff.ContainsKey(buffEfect[i]))
            {
                    buffpow  = enemy.limitBuffDebuff[buffEfect[i]];
                    

            }
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;

            }else if (enemy==null&&enemy2!=null)
            {
            if (enemy2.limitBuffDebuff.ContainsKey(buffEfect[i]))
            {
                    buffpow  = enemy2.limitBuffDebuff[buffEfect[i]];
                    
            }
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;

            }else if(enemy2==null)
            {
                if (enemy3.limitBuffDebuff.ContainsKey(buffEfect[i]))
            {
                buffpow  = enemy3.limitBuffDebuff[buffEfect[i]];
                
            }
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;
            }
                
                    buffEfectText.text =buffpow.ToString();
                    iconImage.sprite = BuffDebuffModel.icon;
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

      private int GetEnemyBuff(int cardID)
  {
    EnemyCardEntity EnemyCardEntity = Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+cardID);
    int buff=EnemyCardEntity.atk;
    return buff;
  }

private int[] GetEnemyBuffEfect(int cardID)
  {
    EnemyCardEntity EnemyCardEntity = Resources.Load<EnemyCardEntity>("EnemyCardEntityList/EnemyCard"+cardID);
    int[] buff =EnemyCardEntity.buffdebuff;   
    return buff;
  }
}
