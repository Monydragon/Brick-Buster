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
        EventManager.BrickHit();
        AudioManager.Instance.Play(hitSound, Camera.main.transform);
        Debug.Log(hitSound.name);
        if (hitsToBreak <= 0)
        {
            EventManager.ScoreChanged(points);
            //GameManager.GM.AddPoints(points);
            GameManager.Instance.UpdateBricks();

            var rndChance = Random.Range(1, 100);
            if (rndChance <= chanceToDropItem)
            {
                Debug.Log("Spawn Powerup");
                Instantiate(lifePowerup, transform.position, Quaternion.identity);
            }
            AudioManager.Instance.Play(brickExplodedSound, Camera.main.transform);
            EventManager.BrickDestroyed();
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = brickSprites[hitsToBreak - 1];
        }
    }
}
