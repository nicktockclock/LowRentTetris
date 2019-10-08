using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Sprite red;
    public Sprite pink;
    public Sprite blue;
    public Sprite lightblue;
    public Sprite green;
    public Sprite yellow;
    public Sprite alsored;
    public Sprite tile;
    //every movement feed back the tiles occupied. on update check if below this tile is the bottom or a taken tile

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Setup.currenttiles.Add(collision.gameObject);
        //collision.gameObject.tag = "Occupied";
        /* if (collision.gameObject.CompareTag("Taken"))
         {
             Debug.Log("HHHH");
             Setup.hit = true;
             return;
         }
         else
         {
             collision.gameObject.tag = "Taken";
         }
         if (Setup.hit == true)
         {
             return;
         }
        collision.gameObject.tag = "Taken";
      //  collision.gameObject.GetComponent<Position>().current = true;
        if (gameObject.CompareTag("OtherSnake"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = red;
        }
        else if (gameObject.CompareTag("Mountain"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = pink;
        }
        else if (gameObject.CompareTag("Snake"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = blue;
        }
        else if (gameObject.CompareTag("Box"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = lightblue;
        }
        else if (gameObject.CompareTag("Lshape"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = green;
        }
        else if (gameObject.CompareTag("OtherLShape"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = yellow;
        }
        else if (gameObject.CompareTag("Long"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = alsored;
        }
      /*  if (collision.gameObject.GetComponent<Position>().y - 1 >=0 && Setup.tiles[collision.gameObject.GetComponent<Position>().x, collision.gameObject.GetComponent<Position>().y- 1].CompareTag("Taken"))
        {
            Setup.hit = true;
            Debug.Log("Here");
        }*/
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
       /* if (collision.gameObject.CompareTag("Occupied"))
        {
            Setup.currenttiles.Add(collision.gameObject);
        }
        else
        {
            collision.gameObject.tag = "Tile";
        }*/
    }


}
