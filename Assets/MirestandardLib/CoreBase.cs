using UnityEngine;

public abstract class CoreBase<TCore> : MMono where TCore : CoreBase<TCore>
{
    protected override void Awake()
    {
        LoadComponents();
    }

    protected U LoadComponent<U>(ref U field, bool searchInChildren = false) where U : Component, IData<TCore>
    {
        if (field == null)
        {
            field = searchInChildren ? GetComponentInChildren<U>(true) : GetComponent<U>();
        }

        field.Init((TCore)this);
        return field;
    }
}
