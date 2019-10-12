using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InnovatedCollision : MonoBehaviour
{
    //every movement feed back the tiles occupied. on update check if below this tile is the bottom or a taken tile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "InnovatedLevel")
        {
            InnovatedGame.currenttiles.Add(collision.gameObject);
        }
        else
        {
            Setup.currenttiles.Add(collision.gameObject);
        }
        
    }
}
