using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private Button refreshButton;
    [SerializeField] private string currentScene = "Level0";
    [SerializeField] private Button homeButton;


    private void Start()
    {
        
        refreshButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneLoader.Instance.LoadScene(currentScene);
        });

        homeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneLoader.Instance.LoadScene("MainMenu");
        });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            finishPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
