using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int ID;
    public string SkillName;
    public int Power;
    public int Accuracy;
    public int PP;
    public int LearnSkill;

    public int currentPP;

    public Skill(int iD, string skillName, int power, int accuracy, int pP, int learnSkill, int currentPP)
    {
        ID = iD;
        SkillName = skillName;
        Power = power;
        Accuracy = accuracy;
        PP = pP;
        LearnSkill = learnSkill;
        this.currentPP = currentPP;
    }
}
