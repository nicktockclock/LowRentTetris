using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    //every movement feed back the tiles occupied. on update check if below this tile is the bottom or a taken tile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Setup.currenttiles.Add(collision.gameObject);
    }
}
