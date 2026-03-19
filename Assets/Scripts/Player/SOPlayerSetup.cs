using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Movement")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce = 2;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public float swipeDuration = .2f;
}
