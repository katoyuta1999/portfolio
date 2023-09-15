using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerデータとその処理
public class PlayerParameter
{
    public static PlayerParameter instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public static int startcoin=50;
    public static int startmaxHp=800;
    public static int startHp=800;
    
    public static int coin;
    public static int maxHp;
    public static int nowHp;
    public static Sprite icon;
    public static int playerShield=0;
    public static Dictionary<int,int> buffDebuff = new Dictionary<int,int>()
        {  };
    public static Dictionary<int,int> limitBuffDebuff = new Dictionary<int,int>()
        {  };
    public static int map;
    public static int stage;

    public static List<int> totalDeck = new List<int>{3,3,3,3,4,4,4,4,4,9,5,12,12,11,11,10,10};//****
    public static List<int> battleDeck = new List<int>{ };//****
    public static List<reric> rerics;
    public static List<pot> pots;
    
}
public class reric
{
}

public class pot
{
}