using UnityEngine;

[CreateAssetMenu(menuName = "Buff System/Buff")]
public class BuffData : ScriptableObject
{
    public BuffType buffType;
    public float duration;
    public float value;
    public bool isStackable;
    public Sprite buffIcon;
}