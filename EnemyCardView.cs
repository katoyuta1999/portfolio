using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCardView : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text atkText;
    [SerializeField] Text multiText;
    [SerializeField] GameObject icon1;
    [SerializeField] GameObject icon2;
    [SerializeField] GameObject icon3;
    [SerializeField] GameObject icon4;
  
    public void Show(EnemyCardModel enemyCardModel)
    {
        nameText.text = enemyCardModel.name;
        atkText.text = enemyCardModel.atk.ToString();
        
        if(enemyCardModel.multipul>1){
        multiText.text = "x"+enemyCardModel.multipul.ToString();
        }
        else
        {
            multiText.enabled = false;
        }

        
        if(enemyCardModel.icon1==0)
        {
            icon1.SetActive (false);
        // var iconA= icon1.GetComponent<Image>();
        //  iconA.sprite = Resources.Load<Sprite>("Icon/icon-000");
        }

        if(enemyCardModel.icon2==0)
        {
            icon2.SetActive (false);
        //icon2.sprite = Resources.Load<Sprite>("Icon/icon-000");
        }
        
        if(enemyCardModel.icon3==0)
        {
            icon3.SetActive (false);
        //icon3.sprite = Resources.Load<Sprite>("Icon/icon-000");
        }
        
        if(enemyCardModel.icon4==0)
        {
            icon4.SetActive (false);
        // icon4.sprite = Resources.Load<Sprite>("Icon/icon-000");
        }
        

    }
}
