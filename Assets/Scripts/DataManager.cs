using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance {  get { return instance; } }

    public static Dictionary<int, PockmonBaseData> pockmonDic = new Dictionary<int, PockmonBaseData>();

    private void Awake()
    {
        instance = this;
        ReadPockmonBaseDta();
    }

    private void Start()
    {

    }

    private void ReadPockmonBaseDta()
    {
        pockmonDic.Clear();

        TextAsset text = Resources.Load<TextAsset>("Json/PockmonBaseData");

        string json = text.text;
        JsonData jsonData = JsonMapper.ToObject(json);

        for (int i = 0; i < jsonData.Count; i++)
        {
            int id = int.Parse(jsonData[i]["ID"].ToString());
            string pockmonName = jsonData[i]["PockmonName"].ToString();
            string description = jsonData[i]["Description"].ToString();
            string spritePath = jsonData[i]["SpritePath"].ToString();
            int maxHp = int.Parse(jsonData[i]["MaxHp"].ToString());
            int attack = int.Parse(jsonData[i]["Attack"].ToString());
            int defense = int.Parse(jsonData[i]["Defense"].ToString());
            int speed = int.Parse(jsonData[i]["Speed"].ToString());
            int spDefense = int.Parse(jsonData[i]["SpDefense"].ToString());
            int spAttack = int.Parse(jsonData[i]["SpAttack"].ToString());
            int pockmonType = int.Parse(jsonData[i]["PockmonType"].ToString());

            PockmonType type = PockmonType.Fire;
            switch (pockmonType)
            {
                case 1:
                    type = PockmonType.Fire;
                    break;
                case 2:
                    type = PockmonType.Nature;
                    break;
                case 3:
                    type = PockmonType.Water;
                    break;
            }
            PockmonBaseData pockmonBaseData = new PockmonBaseData(id,pockmonName,description,spritePath,maxHp,attack,
                defense,speed,spDefense,spAttack,type);
            pockmonDic.Add(id, pockmonBaseData);
        }
    }

    public PockmonBaseData GetPockmonBaseData(int id)
    {
        if (pockmonDic.ContainsKey(id))
        {
            return pockmonDic[id];
        }
        return null;
    }
}
