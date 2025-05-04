using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.Cinemachine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPanel;

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

    [Header("Tutorial")]
    [SerializeField] private Button tutorialButton;
    [SerializeField] private string tutorialScene = "Level0";


    private void Start()
    {
        buildBridgeButton.onClick.AddListener(OnBuildButtonPressed);
        raiseWaterButton.onClick.AddListener(OnRaiseWaterButtonPressed);
        tutorialButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadScene(tutorialScene);
        });
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
        buttonPanel.SetActive(false);
    }

}
