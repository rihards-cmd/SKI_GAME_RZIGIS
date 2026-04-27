using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]private AudioClip collisionSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
    private void OnEnable()
    {
        Obstacle.OnPlayerHit += PlayCollisionSound;
    }
}