using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particlePrefab;

    public AudioSource audioSource;

    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        OnCollect();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            spriteRenderer.enabled = false;
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    protected virtual void OnCollect()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        if (particlePrefab != null)
        {
            ParticleSystem ps = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            ps.Play();

            Destroy(ps.gameObject, 5);
            
        }
    }
}
