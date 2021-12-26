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

    public void HitBrick()
    {
        hitsToBreak--;
        if(hitsToBreak <= 0)
        {
            GameManager.GM.AddPoints(points);
            GameManager.GM.UpdateBricks();

            var rndChance = Random.Range(1, 100);
            if(rndChance <= chanceToDropItem)
            {
                Debug.Log("Spawn Powerup");
                Instantiate(lifePowerup,transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = brickSprites[hitsToBreak - 1];
        }
    }
}
