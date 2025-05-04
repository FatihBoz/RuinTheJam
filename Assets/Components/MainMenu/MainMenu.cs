using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.Cinemachine;

public class MainMenu : MonoBehaviour
{
    [Header("Bridge")]
    [SerializeField] private Transform bridge;
    [SerializeField] private Button buildBridgeButton;
    [SerializeField] private float targetedBridgeHeight = 0f;
    [SerializeField] private float bridgeConstructionTime = 1f;

    [Header("Water")]
    [SerializeField] private Transform water;
    [SerializeField] private Button raiseWaterButton;
    [SerializeField] private float targetedWaterLevel = 0f;
    [SerializeField] private float waterLevelTime = 2f;


    private void Start()
    {
        buildBridgeButton.onClick.AddListener(OnBuildButtonPressed);
        raiseWaterButton.onClick.AddListener(OnRaiseWaterButtonPressed);
    }

    public void OnBuildButtonPressed()
    {
        bridge.DOLocalMoveY(0, bridgeConstructionTime).OnComplete(() => SceneLoader.Instance.LoadScene("Level1"));
        MakeButtonInactive();
    }

    public void OnRaiseWaterButtonPressed()
    {
        water.gameObject.SetActive(true);
        water.DOLocalMoveY(targetedWaterLevel, waterLevelTime).OnComplete(() => Application.Quit());
        MakeButtonInactive();
    }

    private void MakeButtonInactive()
    {
        buildBridgeButton.gameObject.SetActive(false);
        raiseWaterButton.gameObject.SetActive(false);
        CinemachineCamera c = GetComponent<CinemachineCamera>();
    }

}
