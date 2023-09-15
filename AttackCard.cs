using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System.Linq;

public class AttackCard : MonoBehaviour, IDropHandler
{
    public static AttackCard instance;

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
        // attackerを選択　マウスポインターに重なったカードをアタッカーにする
       CardController attackCard = eventData.pointerDrag.GetComponent<CardController>();
       CardController cell = GetComponent<CardController>();;       
        if (attackCard.model.PlayerCard == cell.model2)
        {
            return;
        }      
       
        //カードのIDを取得する。
        int cardID = attackCard.model.id;
        List<int> category = attackCard.model.category.ToList();

        //カードの影響範囲を取得する。
        cardArea = attackCard.model.area;
        //なんのcellが対象となっているか。
        string cell2 = cell.ToString();
        string targetCell =Regex.Replace(cell2, @"[^0-9]", "");

        //fieldへ伝え、各セルへ連絡
        Debug.Log(cardID+"が"+cell+"を対象としています。");
        if(targetCell=="")
        {
            return;
        }
        if(cardArea.Contains(15))
        {
            return;
        }
        List<int> limitarea = new List<int> (GetLimitArea(cardID).ToList());

        if(limitarea.Contains(int.Parse(targetCell)))
        {  
            return;
        }else
        {
            CellManeger.instance.TellTarget(cardID,cardArea,targetCell,category);


            //cardを使用して墓地へ送る。自身を破壊する。
            attackCard.DestroyCard(attackCard);
            GameManeger.instance.CemeCard(cardID);

            GameManeger.instance.ReduceManaPoint(attackCard.model.cost);
            GameManeger.enemyTransforms = GameManeger.instance.SetEnemyTransforms();
            attackCard.model.canUse = false;
            attackCard.view.SetCanUsePanel(attackCard.model.canUse);
            
        }

        
    }
    public int[] GetLimitArea(int cardID)
  { 
    CardEntity CardEntity = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
    int[] d=CardEntity.limitArea;
    return d;
  }
}