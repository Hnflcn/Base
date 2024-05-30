using System.Collections;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VP.Nest.Economy;
using VP.Nest.Utilities;

namespace VP.Nest.UI.Currency
{
    public class CurrencyUI : UIPanel
    {
        private MoneyIconGroup moneyIconGroup;
        public TextMeshProUGUI moneyText;

        private bool isMoneyCounting;
        private float currentMoney, nextMoney;

        private Camera cam => UsefulFunctions.MainCamera;

        [SerializeField] public Transform target;
        [SerializeField] private GameObject moneyIconPrefab;

        [Space] public float moneyAnimationDuration = 1.5f;

        internal override void Initialize()
        {
            base.Initialize();
            transform.SetSiblingIndex(100);
            moneyText.text = GameEconomy.PlayerMoney.ToString(CultureInfo.InvariantCulture);
        }


        public void AddMoney(float amount, bool isAnimated = true)
        {
            DOTween.Complete("MoneyAnimation");

            StartCoroutine(AddMoneyCoroutine(amount, isAnimated));
        }

        [ContextMenu("Add 100K")]
        public void Add100K()
        {
            AddMoney(50, Vector3.zero, true);
        }

        public void AddMoney(float amount, Vector3 from, bool multiple = false)
        {
            if (amount.Equals(0)) return;
            StartCoroutine(AddMoneyCoroutine(amount, from, multiple));
        }

        private IEnumerator AddMoneyCoroutine(float amount, bool isAnimated)
        {
            currentMoney = GameEconomy.PlayerMoney;
            nextMoney = currentMoney + amount;
            isMoneyCounting = true;

            GameEconomy.AdjustPlayerMoney(amount);

            if (isAnimated)
            {
                moneyIconGroup.Init();
                yield return BetterWaitForSeconds.WaitRealtime(moneyAnimationDuration - moneyAnimationDuration / 6f);
            }

            moneyText.transform.DOComplete();
            moneyText.transform.DOPunchScale(Vector3.one * .4f, .2f, 2, .5f);
            moneyText.SetText(nextMoney.FormatMoney());

            target.DOComplete();
            target.DOPunchScale(Vector3.one * .8f, .2f, 2, .5f);

            isMoneyCounting = false;
        }

        private IEnumerator AddMoneyCoroutine(float amount, Vector3 fromPosition, bool multiple = false)
        {
            float currentMoney = GameEconomy.PlayerMoney;
            float nextMoney = currentMoney + amount;
            GameEconomy.AdjustPlayerMoney(amount);

            Vector3 pos = cam.WorldToScreenPoint(fromPosition);

            float count = multiple ? Mathf.Clamp(amount, 1, 10) : 1;

            float perMoney = amount / count;

            for (int i = 0; i < count; i++)
            {
                GameObject icon = ObjectPooler.Instance.Spawn("MoneyIcon", pos, transform);
                icon.transform.localScale = Vector3.one;
                icon.transform.DOMove(target.position, moneyAnimationDuration).SetEase(Ease.InBack).SetId("icon")
                    .OnComplete(() =>
                    {
                        currentMoney += perMoney;
                        target.DOComplete();
                        target.DOPunchScale(Vector3.one * .9f, .2f, 2, .5f);
                        icon.gameObject.SetActive(false);

                        moneyText.SetText(Mathf.Lerp(currentMoney, nextMoney, 0.5f).FormatMoney());
                    });

                yield return BetterWaitForSeconds.WaitRealtime(.04f);
            }

            yield return BetterWaitForSeconds.WaitRealtime(moneyAnimationDuration);
            moneyText.SetText(nextMoney.FormatMoney());
        }

        public void SpendMoney(float amount, bool isAnimated = true)
        {
            var currentMoney = GameEconomy.PlayerMoney;
            var nextMoney = currentMoney - amount;

            if (nextMoney < 0)
            {
                return;
            }

            isMoneyCounting = true;

            GameEconomy.AdjustPlayerMoney(-amount);

            DOTween.Complete("MoneyAnimation");
            DOTween.To(() => currentMoney, x => currentMoney = x, nextMoney, moneyAnimationDuration)
                .SetId("MoneyAnimation").SetEase(Ease.OutCubic)
                .OnUpdate(() => moneyText.SetText(nextMoney.FormatMoney()))
                .OnComplete(() => isMoneyCounting = false);
        }

        private IEnumerator SpendMoneyCoroutine(float amount)
        {
            float count = Mathf.Clamp(amount, 1, 6);
            for (int i = 0; i < count; i++)
            {
                string tweenId = "moneyRotate" + i;
                Image icon = ObjectPooler.Instance.Spawn("MoneyIcon", target).GetComponentInChildren<Image>();
                icon.transform
                    .DOLocalMove(Vector3.right * Random.Range(-100f, 100f) + Vector3.down * Random.Range(200f, 300f),
                        moneyAnimationDuration).SetEase(Ease.OutCubic)
                    .OnComplete(() => DOTween.Kill(tweenId));
                icon.transform.DORotate(Vector3.forward * 360, Random.Range(.5f, 1f), RotateMode.FastBeyond360)
                    .SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Incremental)
                    .SetId(tweenId);
                icon.DOAlpha(0, moneyAnimationDuration).OnComplete(() =>
                {
                    icon.gameObject.SetActive(false);
                    Color tempColor = icon.color;
                    tempColor.a = 1;
                    icon.color = tempColor;
                });

                yield return BetterWaitForSeconds.WaitRealtime(.1f);
            }
        }
    }
}