using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class RestManager : MonoBehaviour
{
    [SerializeField] TotalDeckWatcher totaldeckWatch;
    [SerializeField] HpWatcher hpWatch;
    [SerializeField] CoinWatcher coinWatch;

    public static int nowHp;
    public static int maxHp;
    public static int coin;
    public static RestManager instance;
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

    public void Exit()
    {   
        if (MapSelect.beforeStage==6)
        {
            MapSelect.beforeStage = 1;
            MapSelect.yourmap++;
        }

        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);

    }

    public void Rest()
    {
        nowHp += maxHp/10*3;
        
        var aaa = GameObject.Find("Rest");
        var healbutton = aaa.GetComponent<Button>();
        healbutton.enabled = false;

    }
}
