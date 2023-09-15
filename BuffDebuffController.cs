using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffController : MonoBehaviour
{
    public BuffDebuffView view; // 見た目
    public bool model2 =false;
public static BuffDebuffController instance;
    private void Awake() 
    {
        view = GetComponent<BuffDebuffView>();

        if(instance == null)
    {
        instance = this;
    }
    }
    public BuffDebuffModel model;// データに関すること
 
    public void Init(int buffID,int cardID,List<int> category,Transform field)
    {   
        model = new BuffDebuffModel(buffID);
        view.Show(model,cardID,category,field);

    }
    public void DestroyBuffDebuff(BuffDebuffController BuffDebuff)
    {
        Destroy(BuffDebuff.gameObject);
    }

}
