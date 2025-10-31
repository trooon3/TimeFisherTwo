using UnityEngine;

namespace Assets.Scripts
{
    public class BagAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _catchSound;
        private AudioSource _audioSource;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        public void PlayCatchSound() => _audioSource.PlayOneShot(_catchSound);
    }
}
