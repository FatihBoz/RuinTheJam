using UnityEngine;
using UnityEngine.EventSystems;

public class UiScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleFactor = 1.05f;
    private Vector3 originalScale;
    private RectTransform rectTransform;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale *= scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }


}
