using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardController : MonoBehaviour
{
    public EnemyCardView view; // 見た目

    private void Awake() 
    {
        view = GetComponent<EnemyCardView>();
    }
    public EnemyCardModel model;// データに関すること
 
    public void Init(int EnemyCardID)
    {
        model = new EnemyCardModel(EnemyCardID);
        view.Show(model);
    }
    public void DestroyEnemyCard(EnemyCardController EnemyCard)
    {
        Destroy(EnemyCard.gameObject);
    }

}
