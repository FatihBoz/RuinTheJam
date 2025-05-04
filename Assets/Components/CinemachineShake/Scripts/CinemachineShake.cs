using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CinemachineShake : MonoBehaviour {

    public static CinemachineShake Instance { get; private set; }

    private CinemachineCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    [Header("Initial Movement")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private GameObject enemies;

    private void Awake() {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        StartCoroutine(SmoothMoveCamera(targetTransform.position));
    }

    private IEnumerator SmoothMoveCamera(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = smoothTime;
        Vector3 startingPosition = playerTransform.position;

        while (elapsedTime < duration)
        {
            print("while");
            playerTransform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition;
        playerTransform.gameObject.SetActive(true);
        enemies.SetActive(true);
    }

    public void ShakeCamera(float intensity, float time) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            cinemachineVirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update() {
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.AmplitudeGain = 
                Mathf.Lerp(startingIntensity, 0f, 1 - shakeTimer / shakeTimerTotal);
        }

        if(cinemachineVirtualCamera.Follow == null)
        {
            FindPlayer();
        }
        
    }

    private void FindPlayer()
    {
        var player = GameObject.FindFirstObjectByType<PlayerCombat>();
        if(player != null)
        {
            cinemachineVirtualCamera.Follow = GameObject.FindFirstObjectByType<PlayerCombat>().transform;
        }
    }

}
