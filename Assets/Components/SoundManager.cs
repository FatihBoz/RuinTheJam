using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;


    [SerializeField]
    private AudioClip hitClick;
    [SerializeField]
    private AudioClip uiClick;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHitClick()
    {
        audioSource.PlayOneShot(hitClick);
    }
    public void PlayUIClick()
    {
        audioSource.PlayOneShot(uiClick);
    }


}
