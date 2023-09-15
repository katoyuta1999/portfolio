using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System.Linq;

public class SupportCard : MonoBehaviour, IDropHandler
{
    public static SupportCard instance;

    public string cell ;
    public int cardID ;
    public int[] cardArea;

      public void Awake()
  {
    if(instance == null)
    {
        instance = this;
    }
  }
    public void OnDrop(PointerEventData eventData)
    {
        if (CardMovement.canDrag == false)
        {
            return;
        }
        
        /// 攻撃
        // attackerを選択　マウスポインターに重なったカードをサポーにする
       CardController supportCard = eventData.pointerDrag.GetComponent<CardController>();

       CardController cell = GetComponent<CardController>();;       
        if (supportCard.model.PlayerCard == cell.model2)
        {
            return;
        }      
       
        //カードのIDを取得する。
        int cardID = supportCard.model.id;
        List<int> category =supportCard.model.category.ToList();
        //いらない？→carderea targetCell自分専用のエフェクト処理スクリプトがいる?かも
        if (category.Contains(0))
        {
            return;     
        }
        
        SupportEfect.instance.TargetedMe(cardID,category);


        //cardを使用して墓地へ送る。自身を破壊する。
        supportCard.DestroyCard(supportCard);
        GameManeger.instance.CemeCard(cardID);
        //マナを消費
        GameManeger.instance.ReduceManaPoint(supportCard.model.cost);
        
        GameManeger.enemyTransforms = GameManeger.instance.SetEnemyTransforms();
        supportCard.model.canUse = false;
        supportCard.view.SetCanUsePanel(supportCard.model.canUse);


    }
}