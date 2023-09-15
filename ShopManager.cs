using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class ShopManager : MonoBehaviour
{
    [SerializeField] TotalDeckWatcher totaldeckWatch;
    [SerializeField] HpWatcher hpWatch;
    [SerializeField] CoinWatcher coinWatch;
    [SerializeField] Transform cardErea1;
    [SerializeField] Transform cardErea2;
    [SerializeField] Transform cardErea3;
    [SerializeField] Transform cardErea4;
    [SerializeField] Transform cardErea5;

    List<Transform> cardEreas = new List<Transform>();
    public static int nowHp;
    public static int maxHp;
    public static int coin;
    public static ShopManager instance;
    
    int price;
    int rarity;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    //値が変更されたら更新する者たち    
    hpWatch.ChangedValue += (value) =>
    hpWatch.GetComponentInChildren<Text>().text = nowHp.ToString()+"/"+maxHp.ToString();
    
    coinWatch.ChangedValue += (value) =>
    coinWatch.GetComponentInChildren<Text>().text = coin.ToString();

    totaldeckWatch.ChangedValue += (value) =>
    totaldeckWatch.GetComponentInChildren<Text>().text = PlayerParameter.totalDeck.Count.ToString();

    }

    public void Start()
    {
        
        maxHp = PlayerParameter.maxHp;
        nowHp = PlayerParameter.nowHp;
        hpWatch.GetComponentInChildren<Text>().text = nowHp.ToString()+"/"+maxHp.ToString();
        totaldeckWatch.GetComponentInChildren<Text>().text = PlayerParameter.totalDeck.Count.ToString();
        coin = PlayerParameter.coin;  

        cardEreas.Add(cardErea1);
        cardEreas.Add(cardErea2);
        cardEreas.Add(cardErea3);
        cardEreas.Add(cardErea4);
        cardEreas.Add(cardErea5);

        
        for(int i=0; i<cardEreas.Count; i++)
        {   Random r =new Random();
            int cardID = r.Next(13,37);///***********************

            GameManeger.instance.CreateCard(cardID,cardEreas[i]);//カード作成
            
            rarity = GetRarity(cardID);//値段の決定
            if (rarity==1)
            {
              price = r.Next(40,71);   
            }else if(rarity==2)
            {
                price = r.Next(71,101);
            }else if(rarity==3)
            {
                price = r.Next(100,131);
            }

            var pricetext = cardEreas[i].GetComponentInChildren<Text>();
            pricetext.text = price.ToString();

        }

        
        CoinWatcher.instance.SetCoinValue(coin);
        HpWatcher.instance.SetHpValue(nowHp);

    }

    public void Exit()
    {   

        SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
    }
        public int GetRarity(int cardID)
  { 
  CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
    int aaa=CardEntity.rarity;
    return aaa;
  }
}
