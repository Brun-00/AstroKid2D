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
        spriteRenderers = new List<SpriteRenderer>();
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    public void Flash()
    {
        if(_currentTween!=null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(s => s.color = Color.white);
        }
        foreach(var s in spriteRenderers)
        {
            s.DOColor(color, flashDuration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
