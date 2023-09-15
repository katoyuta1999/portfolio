using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MapManager: MonoBehaviour
{
    [SerializeField] TotalDeckWatcher totaldeckWatch;
    [SerializeField] HpWatcher hpWatch;
    [SerializeField] CoinWatcher coinWatch;
    public static int nowHp;
    public static int maxHp;
    public static int coin;
    public static int map;
    public static int stage;
    public static int erea;
    public static List<int> ereaLog = new List<int>(){};//*********ID基準じゃないとダメ


    public static MapManager instance;
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
        CoinWatcher.instance.SetCoinValue(coin);
        HpWatcher.instance.SetHpValue(nowHp);
    }

    public void MapSelect()
    {
        for (int i = 0; i < ereaLog.Count; i++)
        {
            var load = ereaLog[i];
        }
        //乱数によってお店、敵、イベントマスを判定する。
        //クリックしたときのその数値を読み取って進行。

    
    }



}
