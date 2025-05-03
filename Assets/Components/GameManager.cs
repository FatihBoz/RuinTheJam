using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    

    private void OnEnable()
    {
        PlayerCombat.OnPlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        PlayerCombat.OnPlayerDied -= OnPlayerDied;
    }
}
