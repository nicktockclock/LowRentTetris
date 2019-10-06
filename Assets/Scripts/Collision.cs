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


    private void OnTriggerEnter2D(Collider2D collision)
    {
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
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().sprite = tile;
    }
}
