using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarUI : MonoBehaviour
{
    public Image healthBar;
    public HealhtBase healthBase;

    [Header("Animation")]
    public float duration = 0.3f;
    public Ease ease = Ease.OutQuad;

    private void Start()
    {
        if (healthBase == null)
            return;

        healthBar.fillAmount = healthBase.CurrentLife / healthBase.MaxLife;
    }

    private void Update()
    {
        if (healthBase == null || healthBar == null)
            return;

        float targetFill = healthBase.CurrentLife / healthBase.MaxLife;

        if (healthBar.fillAmount != targetFill)
        {
            healthBar.DOFillAmount(targetFill, duration).SetEase(ease);
        }
    }
}