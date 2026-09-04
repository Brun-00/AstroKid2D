using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float flashDuration = 0.5f;

    private Tween _currentTween;

    private void OnValidate()
    {
        // Automatically collect all SpriteRenderers in the object hierarchy.
        spriteRenderers = new List<SpriteRenderer>();

        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    public void Flash()
    {
        // Stop any previous flash before starting a new one.
        if (_currentTween != null && _currentTween.IsActive())
        {
            _currentTween.Kill();
        }

        // Clear existing tweens from the affected sprites.
        spriteRenderers.ForEach(s => s.DOKill());

        Sequence seq = DOTween.Sequence();

        // Set all sprites to the flash color.
        foreach (var s in spriteRenderers)
        {
            s.color = color;
        }

        // Keep the flash color visible briefly.
        seq.AppendInterval(0.08f);

        // Fade all sprites back to their normal color.
        foreach (var s in spriteRenderers)
        {
            seq.Join(s.DOColor(Color.white, 0.5f));
        }

        _currentTween = seq;
    }
}