using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalDeckWatcher : MonoBehaviour
{
    public static TotalDeckWatcher instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
//変更を監視する値
private int _value = PlayerParameter.totalDeck.Count;

//値が変更された時に実行されるイベント
public event System.Action<int> ChangedValue = delegate{};

//値を設定する
public void SetTotalDeckValue(int value){
  //同じ値が来た場合は設定しないし、イベントも実行しない
  if (_value == value){
    return;
  }

  _value = value;
  ChangedValue(_value);
}
}
