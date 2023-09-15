using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerデータとその処理
public class PlayerModel
{
    public int coin;
    public int maxHp;
    public int hp;
    public Sprite icon;
    public List<reric> rerics;
    public List<pot> pots;
    
    public  PlayerModel(int playerID)
    {
        PlayerParameter PlayerParameter =
        PlayerParameter.instance;

        coin = PlayerParameter.coin;
        maxHp = PlayerParameter.maxHp;
        hp = PlayerParameter.nowHp;
        rerics = PlayerParameter.rerics;
        pots = PlayerParameter.pots;



        
    }
    
    
}