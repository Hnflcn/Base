using _Main.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VPNest.UI.Scripts.InGameUI
{
	public class FillBar : MonoBehaviour
	{
		public UnityAction OnComplete;

		[SerializeField] private Image fillImage;
		// private bool isFill;
		private float startFillAmount = 0;
		private float endFillAmount = 1;

		[Space]
		[ColorUsage(true, true)]
		[SerializeField] private Color startColor = Color.red;
		[ColorUsage(true, true)]
		[SerializeField] private Color endColor = Color.green;
		[SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

		[SerializeField] private TMP_Text levelText;
		[SerializeField] private TMP_Text levelNextText;
		private float maxValue = 1;
		private float currentAmount;

		private static readonly int TopLeftColor = Shader.PropertyToID("_TopLeftColor");
		private static readonly int TopRightColor = Shader.PropertyToID("_TopRightColor");
		private static readonly int BottomLeftColor = Shader.PropertyToID("_BottomLeftColor");
		private static readonly int BottomRightColor = Shader.PropertyToID("_BottomRightColor");

		private void Awake()
		{
			Setup();
			levelText.text = PlayerPrefKeys.CurrentLevel.ToString();
			levelNextText.text = (PlayerPrefKeys.CurrentLevel+1).ToString();
		}
		
		

		private void Setup()
		{
			if (!fillImage) return;

			fillImage.fillAmount = startFillAmount;

			fillImage.material.SetColor(TopLeftColor, startColor);
			fillImage.material.SetColor(BottomLeftColor, startColor);
			fillImage.material.SetColor(TopRightColor, endColor);
			fillImage.material.SetColor(BottomRightColor, endColor);
		}

		public void SetupFillBar(float maxFill, bool isReverse = false)
		{
			if (isReverse)
			{
				startFillAmount = 1;
				endFillAmount = 0;
			}
			else
			{
				startFillAmount = 0;
				endFillAmount = 1;
			}

			maxValue = maxFill;
			// isFill = false;
			fillImage.fillAmount = startFillAmount;
			currentAmount = 0;
		}

		public void ChangeFillBar(float value, float time)
		{
			currentAmount += value;
			UpdateFillBar(currentAmount, time);
		}

		public void SetFillBar(float value, float time)
		{
			currentAmount = value;
			UpdateFillBar(currentAmount, time);
		}

		private void UpdateFillBar(float value, float time)
		{
			if (maxValue != 0)
			{
				float targetValue = value / maxValue;
				fillImage.DOComplete();
				fillImage.DOFillAmount(Mathf.Lerp(startFillAmount, endFillAmount, targetValue), time).SetEase(animationCurve).OnComplete(() =>
				{
					if (targetValue >= 1)
						OnComplete?.Invoke();
				});
			}
		}

		[ContextMenu("fill")]
		private void Fill()
		{
			SetFillBar(1, 1);
		}
	}
}