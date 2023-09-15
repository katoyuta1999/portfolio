using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BuffDebuffEntity",menuName ="Create BuffDebuff Entity")]
public class BuffDebuffEntity : ScriptableObject
{
    public new string name;
    public int ID;
    public int buffEfect;
    public Sprite icon;

}
