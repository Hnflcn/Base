using _Main.Scripts.Pool;
using UnityEngine;
using Zenject;
using AudioType = _Main.Scripts.Pool.AudioType;

namespace _Main.Scripts._Base.Audio
{
    public class AudioManager : MonoBehaviour
    {
      // [Inject] private AudioPool _audioPool;

      // public void SoundPlay(AudioType type)
      // {
      //     switch (type)
      //     {
      //         case AudioType.Success:
      //             successSound.Play();
      //             break;
      //         case AudioType.Fail:
      //             wrongSound.Play();
      //             break;
      //         case AudioType.Selection:
      //             buttonSound.Play();
      //             break;
      //     }
      // }
    }
}