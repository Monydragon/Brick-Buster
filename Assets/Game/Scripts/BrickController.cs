using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int points;
    public int hitsToBreak;
    public int chanceToDropItem = 25;
    public GameObject lifePowerup;
    public List<Sprite> brickSprites;
    public AudioClip hitSound;
    public AudioClip brickExplodedSound;
    public void HitBrick()
    {
        hitsToBreak--;
        AudioManager.Instance.Play(hitSound, Camera.main.transform);
        Debug.Log(hitSound.name);
        if (hitsToBreak <= 0)
        {
            GameManager.GM.AddPoints(points);
            GameManager.GM.UpdateBricks();

            var rndChance = Random.Range(1, 100);
            if (rndChance <= chanceToDropItem)
            {
                Debug.Log("Spawn Powerup");
                Instantiate(lifePowerup, transform.position, Quaternion.identity);
            }
            AudioManager.Instance.Play(brickExplodedSound, Camera.main.transform);

            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = brickSprites[hitsToBreak - 1];
        }
    }
}
