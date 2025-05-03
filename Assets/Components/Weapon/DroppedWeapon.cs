using UnityEngine;
public enum WeaponType
{
    GUN,
    SWORD,
    SPEAR
}
public class DroppedWeapon : MonoBehaviour
{
    
    public WeaponType weaponType;

    public virtual void Take() { } // player will come here and press e take sword
}
