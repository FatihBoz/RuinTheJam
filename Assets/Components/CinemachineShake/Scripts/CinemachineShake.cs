using UnityEngine;
using Unity.Cinemachine;

public class CinemachineShake : MonoBehaviour {

    public static CinemachineShake Instance { get; private set; }

    private CinemachineCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake() {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineCamera>();
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
            cinemachineVirtualCamera.Follow = GameObject.FindFirstObjectByType<PlayerCombat>().transform;
        }
        
    }

}
