using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponItemData", menuName = "Weapon Item Data")]
public class WeaponItemData : ScriptableObject
{
    public bool isDestructible;
    public GameObject Thumbnail;
    public Weapon weapontoEquip;
    public float animationSpeed;
    public float rotationSpeed;
    public float bounceRange;
}
