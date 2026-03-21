using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particlePrefab;

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
        Destroy(gameObject);
        
    }

    protected virtual void OnCollect()
    {
        if (particlePrefab != null)
        {
            ParticleSystem ps = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            ps.Play();

            Destroy(ps.gameObject, 5);
        }
    }
}
