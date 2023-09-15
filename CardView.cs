using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text hpText;
    [SerializeField] Text atkText;
    [SerializeField] Text costText;
    [SerializeField] Text areaText;
    [SerializeField] Text textText;

    [SerializeField] Image iconImage;
    [SerializeField] GameObject canUsePanel;
    
    public void Show(CardModel cardModel)
    {
        nameText.text = cardModel.names;
        hpText.text = cardModel.hp.ToString();
        atkText.text = cardModel.atk.ToString();
        costText.text = cardModel.cost.ToString();
        areaText.text = cardModel.area.ToString();
        iconImage.sprite = cardModel.icon;
        textText.text = cardModel.text;

        
        

    }
        public void SetCanUsePanel(bool flag) // フラグに合わせてCanUsePanelを付けるor消す
    {
        canUsePanel.SetActive(flag);
    }
}
