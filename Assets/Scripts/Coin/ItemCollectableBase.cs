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
        // Collect the item when the correct object enters the trigger.
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        // Run the collection behavior before destroying the item.
        OnCollect();

        if (audioSource != null && audioSource.clip != null)
        {
            // Play the collection sound and hide the sprite.
            audioSource.pitch = Random.Range(0.6f, 1.4f);
            audioSource.Play();

            spriteRenderer.enabled = false;

            // Wait for the sound to finish before destroying the object.
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            // Destroy immediately when there is no audio to play.
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollect()
    {
        // Play the item's collection sound.
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        if (particlePrefab != null)
        {
            // Spawn and play the collection particle effect.
            ParticleSystem ps = Instantiate(
                particlePrefab,
                transform.position,
                Quaternion.identity
            );

            ps.Play();

            // Clean up the particle effect after a few seconds.
            Destroy(ps.gameObject, 5);
        }
    }
}