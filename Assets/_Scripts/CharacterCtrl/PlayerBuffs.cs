using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour, IData
{

    [SerializeField] List<Buff> activeBuffs = new List<Buff>();


    private void Update()
    {
        foreach (Buff buff in activeBuffs)
        {
            if (!buff.IsStillActive()) activeBuffs.Remove(buff);
        }
    }


    public void AddNewBuff(BuffData data)
    {
        if (!data.isStackable)
        {
            var existing = activeBuffs.Find(b => b.buffData.buffType == data.buffType);
            if (existing != null)
            {
                existing = new Buff(data);
                return;
            }
        }

        Buff newBuff = new Buff(data);
        activeBuffs.Add(newBuff);
    }

    public float GetBonus(BuffType type)
    {
        if (activeBuffs.Count == 0) return 0;
        foreach (Buff buff in activeBuffs)
        {
            var data = buff.buffData;
            if (data.buffType == type) return data.value;
        }
        return 0;
    }
}
