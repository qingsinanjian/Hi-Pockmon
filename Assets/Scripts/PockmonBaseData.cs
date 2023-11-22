using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PockmonBaseData : MonoBehaviour
{
    public int ID;
    public string PockmonName;
    public string Description;
    public string SpritePath;
    public int MaxHp;
    public int Attack;
    public int Defense;
    public int Speed;
    public int SpDefense;
    public int SpAttack;

    public PockmonType pockmonType;

    public PockmonBaseData(int iD, string pockmonName, string description, string spritePath, int maxHp, int attack, int defense, int speed, int spDefense, int spAttack, PockmonType pockmonType)
    {
        ID = iD;
        PockmonName = pockmonName;
        Description = description;
        SpritePath = spritePath;
        MaxHp = maxHp;
        Attack = attack;
        Defense = defense;
        Speed = speed;
        SpDefense = spDefense;
        SpAttack = spAttack;
        this.pockmonType = pockmonType;
    }
}

public enum PockmonType
{
    Fire,
    Nature,
    Water
}
