using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public const int xsize = 10;
    public const int ysize = 24;
    private const float moveWait = 1.0f;
    public float leftwall;
    public float rightwall;
    public float bottom;
    public float top;
    public float stepsize;
    public GameObject tile;
    public GameObject[,] tiles;
    public GameObject[] pieces;
    public GameObject playerpiece;
    public int score;
    private GUIStyle guiStyle = new GUIStyle();
    public GameObject board;
    public GameObject activepieces;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[xsize, ysize];
        stepsize = tile.GetComponent<SpriteRenderer>().bounds.size.x;
        leftwall = 1 - stepsize / 2;
        rightwall = leftwall + (stepsize * xsize);
        bottom = -3 - stepsize / 2;
        top = bottom + (stepsize * ysize);
        BuildGrid();
        //ProofOfConcept();
        playerpiece = Instantiate(pieces[5], new Vector3(leftwall + pieces[5].GetComponent<SpriteRenderer>().bounds.extents.x + stepsize * 4, top - pieces[5].GetComponent<SpriteRenderer>().bounds.extents.y), Quaternion.identity);
        //  InvokeRepeating("MovePlayerPiece", moveWait, moveWait);
        playerpiece.transform.SetParent(board.transform);
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        GUI.Label(new Rect(120, 370, 100, 20), "Next", guiStyle);
        GUI.Label(new Rect(120, 150, 100, 20), "Score: 0", guiStyle);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerpiece.transform.position.y - (stepsize * 2) <= bottom)
        {

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && playerpiece.transform.position.x-(stepsize*2) >= leftwall)
        {
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x - stepsize, playerpiece.transform.position.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && playerpiece.transform.position.x+(stepsize*2) <= rightwall)
        {
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x + stepsize, playerpiece.transform.position.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && playerpiece.transform.position.y - (stepsize * 2) >= bottom)
        {
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
        }
        HitBottomOrCollide();

    }

    void BuildGrid()
    {
        float x = 1;
        float y = -3;
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < ysize; j++)
            {
                GameObject newtile = Instantiate(tile, new Vector3(x, y, 0f), Quaternion.identity);
                tiles[i, j] = newtile;
                newtile.transform.SetParent(board.transform);
                y += tile.GetComponent<SpriteRenderer>().bounds.size.y;
            }
            x += tile.GetComponent<SpriteRenderer>().bounds.size.x;
            y = -3;
        }
    }

    void CheckLines()
    {
        for (int i = 0; i < ysize; i++)
        {
            bool check = false;
            for (int j = 0; j < xsize; j++)
            {
                if (tiles[j, i].CompareTag("Taken"))
                {
                    check = true;
                }
                else
                {
                    check = false;
                    break;
                }
            }
            if (check == true)
            {
                ClearLine(i);
            }
        }
    }

    void ClearLine(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            tiles[i, y].tag = "Tile";
        }
    }

    void HitBottomOrCollide()
    {

    }

  /*  void ProofOfConcept()
    {
        GameObject current1 = Instantiate(pieces[1], new Vector3(leftwall + pieces[1].GetComponent<SpriteRenderer>().bounds.extents.x, bottom + pieces[1].GetComponent<SpriteRenderer>().bounds.extents.y, 0), Quaternion.identity);
        GameObject current2 = Instantiate(pieces[0], new Vector3(leftwall + pieces[0].GetComponent<SpriteRenderer>().bounds.extents.x, bottom + pieces[0].GetComponent<SpriteRenderer>().bounds.extents.y + stepsize * 4, 0), Quaternion.identity);
        GameObject current3 = Instantiate(pieces[3], new Vector3(leftwall + pieces[3].GetComponent<SpriteRenderer>().bounds.extents.x + stepsize * 3, bottom + pieces[3].GetComponent<SpriteRenderer>().bounds.extents.y, 0), Quaternion.identity);
        GameObject current4 = Instantiate(pieces[6], new Vector3(leftwall + pieces[6].GetComponent<SpriteRenderer>().bounds.extents.x + stepsize * 5, bottom + pieces[6].GetComponent<SpriteRenderer>().bounds.extents.y, 0), Quaternion.identity);
        GameObject current5 = Instantiate(pieces[2], new Vector3(leftwall + pieces[2].GetComponent<SpriteRenderer>().bounds.extents.x + stepsize * 6, bottom + pieces[2].GetComponent<SpriteRenderer>().bounds.extents.y, 0), Quaternion.identity);
        GameObject next1 = Instantiate(pieces[5], new Vector3(leftwall - stepsize*3, bottom + stepsize*2, 0), Quaternion.identity);
        GameObject next2 = Instantiate(pieces[4], new Vector3(leftwall - stepsize*3, bottom + stepsize*6, 0), Quaternion.identity);
        GameObject next3 = Instantiate(pieces[3], new Vector3(leftwall - stepsize*3, bottom + stepsize*10, 0), Quaternion.identity);

    }*/

    void MovePlayerPiece()
    {
        playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("HERE");
    }
}
