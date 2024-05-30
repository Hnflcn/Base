using UnityEngine;

namespace _Main.Scripts._Base.Audio
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] private GameObject muteImage;
        [SerializeField] private GameObject openImage;

        private float _initVolume;

        public void ClickMute()
        {
            AddHaptic();
            ChangeImages(false);
            
            _initVolume = AudioListener.volume;
            EditVolume(0);
        }

        public void ClickOpenAudio()
        {
            AddHaptic();
            ChangeImages(true);
            EditVolume(_initVolume);
        }
        
        private void AddHaptic()
        {
        }

        private void EditVolume(float value)
        {
            AudioListener.volume = value;
        }

        private void ChangeImages(bool sound)
        {
            muteImage.SetActive(!sound);
            openImage.SetActive(sound);
        }
    }
}