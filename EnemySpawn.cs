using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public EnemyView view; // 見た目
    public Transform defaultParent;
    public static List<string> enemys = new List<string>();
    private void Awake() 
    {
        view = GetComponent<EnemyView>();
        
    }
    void Start () {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        defaultParent = transform.parent;
    }
    public EnemyModel model;// データに関すること
 
    public void Init(int enemyID)
    {
        model = new EnemyModel(enemyID);
        view.Show(model);
        enemys.Add(model.name);

        EnemysWatcher.instance.SetEnemyValue(enemys.Count);
    }
    public void DestroyEnemy(EnemySpawn enemy)
    {
        Destroy(enemy.gameObject);
        enemys.Remove(model.name);
        EnemysWatcher.instance.SetEnemyValue(enemys.Count);
        if(enemys.Count==0)
        {
            EndBattle();
        }
    }
    public void EndBattle()
    {
        PlayerParameter.buffDebuff.Clear();
        PlayerParameter.limitBuffDebuff.Clear();
        GameManeger.deck.Clear();
        GameManeger.deckOut.Clear();
        GameManeger.cemetary.Clear();
        PlayerParameter.maxHp = GameManeger.maxHp;
        PlayerParameter.nowHp = GameManeger.nowHp;
        PlayerParameter.coin = GameManeger.coin; 

        SceneManager.LoadScene("Reward", LoadSceneMode.Single);
    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded (Scene nextScene, LoadSceneMode mode) {
        // RewardManager.instance.SetUP();
    }

}
