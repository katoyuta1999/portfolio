using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] Transform enemymove;
    [SerializeField] Text nameText;
    [SerializeField] Text hpText;
    [SerializeField] Text shieldText;
    [SerializeField] Image iconImage;
    public static Dictionary<int,int> buffDebuff = new Dictionary<int,int>()
        {  };
    public static Dictionary<int,int> limitBuffDebuff = new Dictionary<int,int>()
        {  };

    
    public void Show(EnemyModel EnemyModel)
    {
        nameText.text = EnemyModel.name;
        hpText.text = EnemyModel.hp.ToString();
        shieldText.text = EnemyModel.shield.ToString();
        iconImage.sprite = EnemyModel.icon;
        

    }
}
