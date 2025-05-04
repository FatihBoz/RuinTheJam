using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private string loadlanacakScene = "Level1";

    private void OnPlayerDied()
    {
        SceneLoader.Instance.LoadScene(loadlanacakScene);
    }

    private void OnEnable()
    {
        PlayerCombat.OnPlayerDied += OnPlayerDied;
        PlayerCombat.OnLevelReset += OnPlayerDied;

    }

    private void OnDisable()
    {
        PlayerCombat.OnPlayerDied -= OnPlayerDied;
        PlayerCombat.OnLevelReset -= OnPlayerDied;
    }
}
