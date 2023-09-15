using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GetCard : MonoBehaviour
{
    public CardController selectCard;
    public int cardID;
    public int price;
    


    public void OnClickGetCard()
    {
        if(GameObject.Find("RewardManager")!=null)
        {
        selectCard = GetComponent<CardController>();
        cardID = selectCard.model.id;
        PlayerParameter.totalDeck.Add(cardID);
        selectCard.DestroyCard(selectCard);
        TotalDeckWatcher.instance.SetTotalDeckValue(PlayerParameter.totalDeck.Count);
        var noselectcard1 = GameObject.Find("Card(Clone)");
        noselectcard1.SetActive (false);
        var noselectcard2 = GameObject.Find("Card(Clone)");
        noselectcard2.SetActive (false);
        var noselectcard3 = GameObject.Find("Card(Clone)");
        noselectcard3.SetActive (false);
        }

        
        else if(GameObject.Find("ShopManager")!=null)
        {
        var coin =GameObject.Find("Coin");
        var cointext = coin.GetComponent<Text>();
        var aaa = transform.parent.gameObject ;
        var pricetext = aaa.GetComponentInChildren<Text>();
        string ptext = Regex.Replace(pricetext.text, @"[^0-9]", "");
        price = int.Parse(ptext);

        if(price<PlayerParameter.coin)
        {
        selectCard = GetComponent<CardController>();
        cardID = selectCard.model.id;
        PlayerParameter.totalDeck.Add(cardID);
        selectCard.DestroyCard(selectCard);
        TotalDeckWatcher.instance.SetTotalDeckValue(PlayerParameter.totalDeck.Count);

        PlayerParameter.coin -= price;
        
        cointext.text = PlayerParameter.coin.ToString();
        }
        else
        {
            return;
        }

        }
        else
        {
            return;
        }

    }
}
