using UnityEngine;

public class GetWeaponActivator : MonoBehaviour
{
    public GameObject onDieEnemy;     
    public GameObject onActiveObject;  
    private bool isActivated = false;  

    void Update()
    {
        if (!isActivated && onDieEnemy == null)
        {
            onActiveObject.SetActive(true);
            isActivated = true;
        }
    }
}
