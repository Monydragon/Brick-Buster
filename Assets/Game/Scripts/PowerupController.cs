using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public int lifes = 1;
    public AudioClip powerupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.LivesChanged(lifes);
            AudioManager.Instance.Play(powerupSFX, Camera.main.transform);
            Destroy(gameObject);
        }
    }

}
