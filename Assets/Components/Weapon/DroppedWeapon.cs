using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    public enum DroppedWeaponType
    {
        GUN,
        SWORD,
        SPEAR
    }
    public DroppedWeaponType weaponType;

    public virtual void Take() { } // player will come here and press e take sword
}
