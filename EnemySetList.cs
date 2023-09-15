using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySetList : MonoBehaviour
{
    [SerializeField] Transform cell1;
    [SerializeField] Transform cell2;
    [SerializeField] Transform cell3;
    [SerializeField] Transform cell4;
    [SerializeField] Transform cell5;
    [SerializeField] Transform cell6;
    [SerializeField] Transform cell7;
    [SerializeField] Transform cell8;
    [SerializeField] Transform cell9;

    public   Dictionary<Transform,int> EnemySetList1 = new Dictionary<Transform,int>()
        { };
    public   Dictionary<Transform,int> EnemySetList2 = new Dictionary<Transform,int>()
        {  };

    
    
}
