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
    public float currentheight;
    public float currentwidth;
    public GameObject tile;
    public static GameObject[,] tiles;
    public GameObject[] pieces;
    public GameObject playerpiece;
    public int score;
    private GUIStyle guiStyle = new GUIStyle();
    public GameObject board;
    public GameObject activepieces;
    public static bool hit = false;
    public static ArrayList currenttiles;
    public Sprite red;
    public Sprite pink;
    public Sprite blue;
    public Sprite lightblue;
    public Sprite green;
    public Sprite yellow;
    public Sprite alsored;
    public Sprite tilesprite;
    public bool canmove = false;
    // Start is called before the first frame update
    void Start()
    {
        currenttiles = new ArrayList();
        tiles = new GameObject[xsize, ysize];
        stepsize = tile.GetComponent<SpriteRenderer>().bounds.size.x;
        leftwall = 1 - stepsize / 2;
        rightwall = leftwall + (stepsize * xsize);
        bottom = -3 - stepsize / 2;
        top = bottom + (stepsize * ysize);
        BuildGrid();
        CreatePlayerPiece();
        InvokeRepeating("MovePlayerPiece", moveWait, moveWait);
        playerpiece.transform.SetParent(board.transform);
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        GUI.Label(new Rect(120, 370, 100, 20), "Next", guiStyle);
        GUI.Label(new Rect(120, 150, 100, 20), "Score: 0", guiStyle);
    }

    void CreatePlayerPiece()
    {
        int num = Random.Range(0, 6);
        playerpiece = Instantiate(pieces[num], new Vector3(leftwall + stepsize * 4, top - stepsize), Quaternion.identity);
        currentheight = playerpiece.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
        currentwidth = playerpiece.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        playerpiece.transform.SetParent(board.transform);
    }


    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && playerpiece.transform.GetChild(0).position.x - stepsize >= leftwall + 0.20)
            {
                currenttiles.Clear();
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x - stepsize, playerpiece.transform.position.y, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && playerpiece.transform.GetChild(0).position.x + stepsize <= rightwall - 0.20)
            {
                currenttiles.Clear();
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x + stepsize, playerpiece.transform.position.y, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && playerpiece.transform.GetChild(0).position.y - stepsize * 2 >= bottom + 0.20)
            {
                currenttiles.Clear();
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && playerpiece.tag != "Box")
            {
                currenttiles.Clear();
                playerpiece.transform.Rotate(new Vector3(0, 0, 90), Space.Self);

                if (playerpiece.transform.GetChild(0).position.x + currentwidth >= rightwall)
                {
                    playerpiece.transform.position = new Vector3(playerpiece.transform.position.x - stepsize, playerpiece.transform.position.y, 0);
                }
                if (playerpiece.transform.GetChild(0).position.x - currentwidth <= leftwall)
                {
                    playerpiece.transform.position = new Vector3(playerpiece.transform.position.x + stepsize, playerpiece.transform.position.y, 0);
                }
                if (playerpiece.transform.GetChild(0).position.y - currentheight <= bottom)
                {
                    playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y + stepsize, 0);
                }
            }
        }
        Debug.Log(currenttiles.Count);
        if (CheckHit())
        {
            HitBottomOrCollide();
        }
    }

    bool CheckHit()
    {
        foreach (GameObject g in currenttiles)
        {
            if (g.GetComponent<Position>().y-1<0 || tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y - 1].CompareTag("Taken"))
            {
                return true;
            }
        }
        return false;
    }

    void BuildGrid()
    {
        float x = 1;
        float y = -3;
        for (int i = 0; i < ysize; i++)
        {
            for (int j = 0; j < xsize; j++)
            {
                GameObject newtile = Instantiate(tile, new Vector3(x, y, 0f), Quaternion.identity);
                newtile.GetComponent<Position>().x = j;
                newtile.GetComponent<Position>().y = i;
                newtile.GetComponent<Position>().current = false;
                tiles[j, i] = newtile;
                newtile.transform.SetParent(board.transform);
                x += tile.GetComponent<SpriteRenderer>().bounds.size.x;
            }

            y += tile.GetComponent<SpriteRenderer>().bounds.size.y;
            x = 1;
        }
    }

    void CheckLines()
    {
        int num = 0;
        int topcleared = 0;
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
                topcleared = i;
                num += 1;
                ClearLine(i);
                Debug.Log("HERE");
            }
        }
        if (num > 0)
        {
            DropLines(num, topcleared+1);
        }
    }

    void ClearLine(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            tiles[i, y].tag = "Tile";
            tiles[i, y].GetComponent<SpriteRenderer>().sprite = tilesprite;
        }
    }

    void DropLines(int num, int topcleared)
    {
        for (int i = topcleared; i < ysize; i++)
        {
            for (int j = 0; j < xsize; j++)
            {
                if (tiles[j, i].CompareTag("Taken"))
                {
                    tiles[j, i - num].tag = "Taken";
                    tiles[j, i - num].GetComponent<SpriteRenderer>().sprite = tiles[j, i].GetComponent<SpriteRenderer>().sprite;
                    tiles[j, i].tag = "Tile";
                    tiles[j, i].GetComponent<SpriteRenderer>().sprite = tilesprite;
                }
            }
        }
    }

    public void HitBottomOrCollide()
    {
        hit = true;
        foreach (GameObject g in currenttiles)
        {
            Debug.Log("Here1");
            tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].tag = "Taken";
            if (playerpiece.CompareTag("OtherSnake"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = red;
            }
            else if (playerpiece.CompareTag("Mountain"))
            {
                Debug.Log("Here2");
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = pink;
            }
            else if (playerpiece.CompareTag("Snake"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = blue;
            }
            else if (playerpiece.CompareTag("Box"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = lightblue;
            }
            else if (playerpiece.CompareTag("Lshape"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = green;
            }
            else if (playerpiece.CompareTag("OtherLShape"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = yellow;
            }
            else if (playerpiece.CompareTag("Long"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = alsored;
            }

        }
        currenttiles.Clear();
        Destroy(playerpiece);
        CheckLines();
        CreatePlayerPiece();
        hit = false;
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
        currenttiles.Clear();
        if (playerpiece.transform.GetChild(0).position.y - stepsize >= bottom+0.2)
        {
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
        }
        
    }
}
