using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CinemachineCamera cam;

    private void OnPlayerDied()
    {
        SceneLoader.Instance.LoadScene("Level1");
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
