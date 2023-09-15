using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardView view; // 見た目
    public bool model2 =false;

    private void Awake() 
    {
        view = GetComponent<CardView>();
    }
    public CardModel model;// データに関すること
 
    public void Init(int cardID, bool playerCard)
    {
        model = new CardModel(cardID, playerCard);
        view.Show(model);
    }
    public void DestroyCard(CardController card)
    {
        Destroy(card.gameObject);
    }

}
