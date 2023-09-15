using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{

    [SerializeField] Transform playerHand;
    [SerializeField] CardController cardPrefab;
    [SerializeField] Transform enemycell1;
    [SerializeField] Transform enemycell2;
    [SerializeField] Transform enemycell3;
    [SerializeField] Transform enemyfield;
    [SerializeField] EnemySpawn enemyPrefab;
    [SerializeField] EnemyCardController enemycardPrefab;
    [SerializeField] BuffDebuffController buffdebuffPrefab;
    [SerializeField] HpWatcher hpWatch;
    [SerializeField] Text nowManaText;
    [SerializeField] Text maxManaText;
    [SerializeField] Transform buffDebuff;
    [SerializeField] Transform limitbuffDebuff;
    [SerializeField] CemeWatcher cemeWatch;
    [SerializeField] DeckWatcher deckWatch;
    [SerializeField] EnemysWatcher enemysWatch;
    [SerializeField] CoinWatcher coinWatch;
    [SerializeField] TotalDeckWatcher totaldeckWatch;
    [SerializeField] DeckOutWatcher deckOutWatch;
    [SerializeField] ShieldWatcher shieldWatch;

    public static List<int> cemetary= new List<int>();

    bool isPlayerTurn = true; //
    public static List<int> deck = new List<int>();  //
    public static List<int> deckOut = new List<int>();
    public static List<int> enemyIDs = new List<int>() {1,1,1};//***********
    public static List<Transform> enemyTransforms = new List<Transform>();
    public static List<GameObject> enemygameobjects = new List<GameObject>();

    public List<int> enemycardIDs = new List<int>();
    public int turncount;
    public List<Transform> enemycells = new List<Transform>();
    public int enemycount;
    public int nowMana;
    public int maxMana;
    public static int nowHp;
    public static int maxHp;
    public static int coin;

    public static GameManeger instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    //値が変更されたら更新する者たち
    deckWatch.ChangedValue += (value) => 
    deckWatch.GetComponentInChildren<Text>().text = deck.Count.ToString();
    
    cemeWatch.ChangedValue += (value) =>
    cemeWatch.GetComponentInChildren<Text>().text = cemetary.Count.ToString();
    
    enemysWatch.ChangedValue += (value) =>
    AddEnemyList();
    
    hpWatch.ChangedValue += (value) =>
    hpWatch.GetComponentInChildren<Text>().text = nowHp.ToString()+"/"+maxHp.ToString();
    
    coinWatch.ChangedValue += (value) =>
    coinWatch.GetComponentInChildren<Text>().text = coin.ToString();

    totaldeckWatch.ChangedValue += (value) =>
    totaldeckWatch.GetComponentInChildren<Text>().text = PlayerParameter.totalDeck.Count.ToString();
    
    shieldWatch.ChangedValue += (value) =>
    shieldWatch.GetComponentInChildren<Text>().text = PlayerParameter.playerShield.ToString();

    }


    void Start()
    {   PlayerParameter.battleDeck = PlayerParameter.totalDeck;
        if((MapSelect.yourmap==1)&&(MapSelect.beforeStage==1))
        {
        maxHp = PlayerParameter.startmaxHp;//1-1だけ初期値を入れる。
        nowHp = PlayerParameter.startHp;
        coin = PlayerParameter.startcoin;
        coinWatch.GetComponentInChildren<Text>().text = PlayerParameter.startcoin.ToString();
        totaldeckWatch.GetComponentInChildren<Text>().text = PlayerParameter.totalDeck.Count.ToString();
        shieldWatch.GetComponentInChildren<Text>().text = PlayerParameter.playerShield.ToString();
        for(int i = 0; i<PlayerParameter.totalDeck.Count; i++)
        {
        deck.Add(PlayerParameter.totalDeck[i]);
        }
        }else{
        maxHp = PlayerParameter.maxHp;
        nowHp = PlayerParameter.nowHp;
        coin = PlayerParameter.coin;
        CoinWatcher.instance.SetCoinValue(coin);
        totaldeckWatch.GetComponentInChildren<Text>().text = PlayerParameter.totalDeck.Count.ToString();
        shieldWatch.GetComponentInChildren<Text>().text = PlayerParameter.playerShield.ToString();
        for(int i = 0; i<PlayerParameter.battleDeck.Count; i++)
        {
        deck.Add(PlayerParameter.battleDeck[i]);
        }
        }


        Shuffle(deck);
        StartGame();
    }

    void StartGame() // 初期値の設定 
    {
        // ターンの決定
        enemycells = GetChildren(enemyfield).ToList();
        
        SpawnEnemy(enemycell1);
        SpawnEnemy(enemycell2);
        SpawnEnemy(enemycell3);
        enemygameobjects = SetEnemyGameobject();

        GameObject enemyobject1 = enemygameobjects[0];
        Debug.Log(enemyobject1);
        enemyobject1.AddComponent<EnemyBuff1>();
        var enemyobject2 = enemygameobjects[1];
        enemyobject2.AddComponent<EnemyBuff2>();
        var enemyobject3 = enemygameobjects[2];
        enemyobject3.AddComponent<EnemyBuff3>();
        

        enemycount = EnemySpawn.enemys.Count;
        nowMana = 0;
        maxMana = 30;

        deckOutWatch.GetComponentInChildren<Text>().text = 0.ToString();
        cemeWatch.GetComponentInChildren<Text>().text = 0.ToString();
        hpWatch.GetComponentInChildren<Text>().text = nowHp.ToString()+"/"+maxHp.ToString();
        ShowManaPoint();
        SetCanUsePanelHand();
        TurnCalc();

    }

        void SetStartHand() // 手札を5枚配る
    {
        for (int i = 0; i < 5; i++)
        {
            DrawCard(playerHand);
            
        }
    }
        public void DrawCard(Transform hand) // カードを引く
    {
        // デッキがないなら墓地のカードをシャッフルして組みなおす。
        if (deck.Count == 0)
        {
            for(int i = 0; i < cemetary.Count; i++)
            deck.Add(cemetary[i]);  
            cemetary.Clear();
            Shuffle(deck);
            CemeWatcher.instance.SetDeckValue(cemetary.Count);
        }
        
        CardController[] handCardList = playerHand.GetComponentsInChildren<CardController>();
        Debug.Log(handCardList.Length);

        if (handCardList.Length < 10)
        {
            // デッキの一番上のカードを抜き取り、手札に加える
            int cardID = deck[0];
            deck.RemoveAt(0);
            CreateCard(cardID, hand);
        }
        else
        {
            //11以上の時は墓地へ送る。
            int cardID = deck[0];
            deck.RemoveAt(0);
            CemeCard(cardID);
        }
        
        SetCanUsePanelHand();
        DeckWatcher.instance.SetDeckValue(deck.Count);
    }

    void TurnCalc() // ターンを管理する
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }
    public void ChangeTurn() // ターンエンドボタンにつける処理
    {

        isPlayerTurn = !isPlayerTurn; // ターンを逆にする
        TurnCalc(); // ターンを相手に回す
    }
    void PlayerTurn()
    {
        
        PlayerParameter.playerShield =0;
        ShieldWatcher.instance.SetShieldValue(PlayerParameter.playerShield);
        ShieldOnOff();
        turncount++;
        Debug.Log("Playerのターン");
        nowMana += maxMana;
        ShowManaPoint();
        SetStartHand(); // 手札を5枚加える
        enemyTransforms = SetEnemyTransforms();
        
        int enemycardID=GetEnemyCardID(1);//********************
        for(int k =0; k<EnemySpawn.enemys.Count; k++){
            var A= SetEnemyMoves();
            EnemyCardset(enemycardID,A[k]);//敵の行動が出る
        }
    
    }
    
     async void EnemyTurn()
    {
        //敵のシールドを0にするメソッド
        
        AllDisCard();//手札のカードをすべて捨てる。
        Debug.Log("Enemyのターン");
        enemyTransforms = SetEnemyTransforms();
        for (int i = 0; i < enemyTransforms.Count; i++)
        {
            SetEnemyShieldZero(enemyTransforms[i]);
        }
        EnemyMoves();
        for (int i = 0; i < enemyTransforms.Count; i++)
        {
            EnemyShieldOnOff(enemyTransforms[i]);
        }
        await Task.Delay(TimeSpan.FromSeconds(1.0f));
        ChangeTurn(); // ターンエンドする
        
    }
    
    
    public void CreateCard(int cardID,Transform erea)
    {
      CardController card =  Instantiate(cardPrefab,erea,false);
      // Playerの手札に生成されたカードはPlayerのカードとする
        if (erea == playerHand)
        {
            card.Init(cardID, true);
        }
        else
        {
            card.Init(cardID, false);
        }
    }
    void SpawnEnemy(Transform cell)
    {
      EnemySpawn enemy =  Instantiate(enemyPrefab,cell,false);
      enemy.Init(1);//************************

    }
    void EnemyCardset(int enemycardID ,Transform enemymove)
    {
      EnemyCardController enemycard =  Instantiate(enemycardPrefab,enemymove,false);
      enemycard.Init(enemycardID);
    }
    public void CemeCard(int cardID)
    {
        cemetary.Add(cardID);
        CemeWatcher.instance.SetDeckValue(cemetary.Count);
    }
    public void AllDisCard()
    {
        CardController[] handcards =playerHand.GetComponentsInChildren<CardController>();
        List<int>cardIDs =new List<int>();

        for (int i = 0; i < handcards.Length; i++)
        {
            cardIDs.Add(handcards[i].model.id);
            CemeCard(cardIDs[i]);
            handcards[i].DestroyCard(handcards[i]);
        }
        

    }
    public void Shuffle<T>(IList<T> array)
    {
        for (var i = array.Count - 1; i > 0; --i)
        {
            // 0以上i以下のランダムな整数を取得
            // Random.Rangeの最大値は第２引数未満なので、+1することに注意
            var j = UnityEngine.Random.Range(0, i + 1);
            
            // i番目とj番目の要素を交換する
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }

    void ShowManaPoint() // マナポイントを表示するメソッド
    {
        nowManaText.text = nowMana.ToString();
        maxManaText.text = "+"+maxMana.ToString();
    }

    public void ReduceManaPoint(int cost) // コストの分、マナポイントを減らす
    {
        nowMana -= cost;
        ShowManaPoint();
        SetCanUsePanelHand();
    }    
    void SetCanUsePanelHand() // 手札のカードを取得して、使用可能なカードにCanUseパネルを付ける
    {        
        CardController[] playerHandCardList = playerHand.GetComponentsInChildren<CardController>();
        foreach (CardController card in playerHandCardList)
        {
            if (card.model.cost <= nowMana)
            {
                card.model.canUse = true;
                card.view.SetCanUsePanel(card.model.canUse);
            }
            else
            {
                card.model.canUse = false;
                card.view.SetCanUsePanel(card.model.canUse);
            }
        }
    }    
    void AddEnemyList()
    {   Text[] enemystext = enemysWatch.GetComponentsInChildren<Text>();
        for (int i = 0; i < EnemySpawn.enemys.Count; i++)
        {
            enemystext[i].text = EnemySpawn.enemys[i];
            if(EnemySpawn.enemys.Count<3)
            {
                enemystext[i+1].text = "";  
            }

        }

        if(EnemySpawn.enemys.Count==0)
        {
            enemystext[0].text="";
        }


    }
    int GetEnemyCardID(int enemyID)
    {  
        EnemyModel model;
        model = new EnemyModel(enemyID);
        int i =
        turncount%model.moveList1.Count;
        
        int enemycardID = model.moveList1[i];
        
        return  enemycardID ;
    }

    Transform[] GetChildren(Transform parent)
{
    // 子オブジェクトを格納する配列作成
    var children = new Transform[parent.childCount];

    // 0～個数-1までの子を順番に配列に格納
    for (var i = 0; i < children.Length; ++i)
    {
        children[i] = parent.GetChild(i);
    }

    // 子オブジェクトが格納された配列
    return children;
}

    List<Transform> SetEnemyMoves()
    {
        List<Transform> enemymovehere = new List<Transform>();
        var AAA = SetEnemyTransforms();
        for(int i = 0; i<AAA.Count; i++)
        {
            enemymovehere.Add(AAA[i].GetChild(4));
        }        
        return enemymovehere;
    }
    public List<Transform> SetEnemyTransforms()
    {
        List<Transform> enemytransformhere = new List<Transform>();
        for(int i = 0; i<9; i++)
        {
            if(!(enemycells[i].childCount == 0))
            {
            enemytransformhere.Add(enemycells[i].GetChild(0));
            }
        }
        return enemytransformhere;
    }

    public List<GameObject> SetEnemyGameobject()
    {
        List<GameObject> enemytransformhere = new List<GameObject>();
        for(int i = 0; i<9; i++)
        {
            if(!(enemycells[i].childCount == 0))
            {
            enemytransformhere.Add(enemycells[i].GetChild(0).gameObject);
            }
        }
        return enemytransformhere;
    }

        public void EnemyMoves()
    {
        for(int k =0; k<EnemySpawn.enemys.Count; k++)
        {
            EnemyAttack.instance.PleyerTargeted(GetEnemyCardID(enemyIDs[k]),enemyTransforms[k]);
            HpWatcher.instance.SetHpValue(nowHp);
        }

 
    }
    public void SetEnemyShieldZero( Transform enemyTransform)
    {
        var AAA = enemyTransform.GetChild(3).GetComponent<Text>();
        if(int.Parse(AAA.text)>0)
        {
            AAA.text = 0.ToString();
        }
        
    }

    public void ShieldOnOff()
    {
            var shieldimage = shieldWatch.GetComponent<Image>();
            var shieldtext = shieldWatch.GetComponentInChildren<Text>();
        if(PlayerParameter.playerShield<1)
        {
                    shieldimage.enabled = false;
                    shieldtext.enabled = false;
        }else if(PlayerParameter.playerShield>0)
        {
            shieldimage.enabled = true;
            shieldtext.enabled = true;
        }
    }
    public void EnemyShieldOnOff(Transform enemy)
    {
            var shield = enemy.Find("ShieldImage");
            var shieldimage = shield.GetComponent<Image>();
            var shieldtext = enemy.GetComponentsInChildren<Text>();
        if(int.Parse(shieldtext[2].text)<1)
        {
                    shieldimage.enabled = false;
                    shieldtext[2].enabled = false;
        }else if(int.Parse(shieldtext[2].text)>0)
        {
            shieldimage.enabled = true;
            shieldtext[2].enabled = true;
        }        


    }
        public void CreateBuffDebuff(int buffID,int cardID,List<int> category,Transform erea,Transform field)
        //ID category どこへ作るか　対象とされている場所
    {
      BuffDebuffController buffDebuff =  Instantiate(buffdebuffPrefab,erea,false);
       
        buffDebuff.Init(buffID,cardID,category,field);
        
    }



}
