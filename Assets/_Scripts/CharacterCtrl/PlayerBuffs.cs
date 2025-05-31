using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour, IData
{

    [SerializeField] public List<Buff> activeBuffs = new List<Buff>();

    public event System.Action OnBuffChanged;


    private void Update()
    {

        int before = activeBuffs.Count;
        activeBuffs.RemoveAll(buff => !buff.IsStillActive());
        int after = activeBuffs.Count;

        if (before != after) // Chỉ khi có buff bị remove
        {
            OnBuffChanged?.Invoke();
        }
    }


    public void AddNewBuff(BuffData data)
    {
        if (!data.isStackable)
        {
            var existing = activeBuffs.Find(b => b.buffData.buffType == data.buffType);
            if (existing != null)
            {
                activeBuffs.Remove(existing);
            }
        }

        Buff newBuff = new Buff(data);
        activeBuffs.Add(newBuff);
        OnBuffChanged();
    }

    public float GetBonus(BuffType type)
    {
        if (activeBuffs.Count == 0) return 0;
        float bonusVal = 0;
        foreach (Buff buff in activeBuffs)
        {
            var data = buff.buffData;
            if (data.buffType == type) bonusVal += data.value;
        }
        return bonusVal;
    }
}
