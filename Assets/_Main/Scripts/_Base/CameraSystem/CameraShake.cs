using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts._Base.CameraSystem
{
    
    // add to Camera Manager
    public class CameraShake : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin noise;

       void Start()
       {
           if (virtualCamera != null)
               noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       }

       public void ShakeCamera(float intensity, float duration)
       {
           if (noise == null) return;
           noise.m_AmplitudeGain = intensity;
           DOVirtual.DelayedCall(duration, StopShake);
       }

       private void StopShake()
       {
           noise.m_AmplitudeGain = 0f;
       }
    }
}