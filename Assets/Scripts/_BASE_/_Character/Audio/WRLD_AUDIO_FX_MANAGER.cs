using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nutbusterz.Calamitus
{
    public class WRLD_AUDIO_FX_MANAGER : MonoBehaviour
    {
        public static WRLD_AUDIO_FX_MANAGER instance; 
        
        [HideInInspector] public AudioSource audioSource;

        [Header("SFX SOUNDS")]
        [Header("SE SOUNDS")]
        public AudioClip sfx;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            audioSource = GetComponent<AudioSource>();  
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            // Get the index of the currently active scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Check if the current scene index is not 0, 1, 2, or 3
            if (currentSceneIndex != 0 && currentSceneIndex != 1 && currentSceneIndex != 2 && currentSceneIndex != 3)
            {
                // Do something specific for scenes with index not equal to 0, 1, 2, or 3
                audioSource.enabled = true; // Enable the audio source
                                            // Your additional actions here...
            }
            else
            {
                // Do something specific for scene index 0, 1, 2, or 3
                audioSource.enabled = false; // Disable the audio source
                                             // Your additional actions here...
            }
        }
        public AudioClip ChooseRandomSFXFromArray(AudioClip[] array)
        {
            int index = Random.Range(0, array.Length);
            return array[index];
        }
        public void PlaySoundFX(AudioSource audioSource, AudioClip soundFX, float volume = 1, bool randomizePitch = true, float pitchRandom = 0.1f)
        {
            audioSource.PlayOneShot(soundFX, volume);
            // BELOW CODE: Resets Pitch
            audioSource.pitch = 1;

            if (randomizePitch)
            {
                audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
            }
        }
        public void PlaySoundFixedPitchFX(AudioSource audioSource, AudioClip soundFX, float volume = 1, float pitch = 1f)
        {
            audioSource.PlayOneShot(soundFX, volume);
            // BELOW CODE: Resets Pitch
            audioSource.pitch = pitch;
        }
    }
}