using UnityEngine;
using UnityEngine.UI;

public class EnemyHeart : MonoBehaviour
{
    private Transform worldCanvas;

    public Transform target;
    public Vector3 offset;

    public Image onTopHeart;
    void Start()
    {
        worldCanvas = GameObject.Find("WorldCanvas").transform;
        transform.SetParent(worldCanvas.transform);
        onTopHeart.fillMethod = Image.FillMethod.Horizontal;
        onTopHeart.fillAmount = 1f;
    }

    void Update()
    {
        if (target!=null)
        {
        transform.position = target.position + offset;

        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log("Set target to: " + target.name);
    }
    public void SetHealthUI(float currentHealth,float maxHealth)
    {
        onTopHeart.fillAmount = currentHealth/maxHealth;

    }
}
