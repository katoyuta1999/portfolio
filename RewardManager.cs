using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class RewardManager : MonoBehaviour
{
    [SerializeField] TotalDeckWatcher totaldeckWatch;
    [SerializeField] HpWatcher hpWatch;
    [SerializeField] CoinWatcher coinWatch;
    [SerializeField] Transform cardErea1;
    [SerializeField] Transform cardErea2;
    [SerializeField] Transform cardErea3;
    List<Transform> cardEreas = new List<Transform>();
    public static int nowHp;
    public static int maxHp;
    public static int coin;
    public static RewardManager instance;
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
        
        for(int i=0; i<cardEreas.Count; i++)
        {   Random r =new Random();
            int cardID = r.Next(13,37);///***********************
            GameManeger.instance.CreateCard(cardID,cardEreas[i]);
        }

        coin +=20;//***********
        PlayerParameter.coin = coin; 
        CoinWatcher.instance.SetCoinValue(coin);
        HpWatcher.instance.SetHpValue(nowHp);

    }

    public void Exit()
    {   
        if (MapSelect.beforeStage==6)
        {
            SceneManager.LoadScene("RestScene", LoadSceneMode.Single);
        }
        else
        {
        SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
        }
    }
}
