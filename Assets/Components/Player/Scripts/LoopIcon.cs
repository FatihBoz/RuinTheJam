using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoopIcon : MonoBehaviour
{
    [SerializeField] private Image closer;
    [SerializeField] private float initialFillTime = 3f;
    private float currentFillTime;
    private void Awake()
    {
        currentFillTime = initialFillTime;
    }

    public void SetCooldown()
    {
        closer.fillAmount = 1f;
        closer.DOFillAmount(0f, currentFillTime);
    }

    public float FillTime
    {
        get => currentFillTime;

    }

}
