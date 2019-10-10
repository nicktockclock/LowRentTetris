using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    public const int xsize = 10;
    public const int ysize = 24;
    private float moveWait = 1.0f;
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
    public GameObject next1;
    public GameObject next2;
    public GameObject next3;
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
    public bool gameover = false;
    private float longbuffer = 0;
    private int score;
    private int linescleared;
    public UnityEngine.UI.Text finalscore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        linescleared = 0;
        currenttiles = new ArrayList();
        tiles = new GameObject[xsize, ysize];
        stepsize = tile.GetComponent<SpriteRenderer>().bounds.size.x;
        leftwall = 1 - stepsize / 2;
        rightwall = leftwall + (stepsize * xsize);
        bottom = -3 - stepsize / 2;
        top = bottom + (stepsize * ysize);
        BuildGrid();
        StartGame();
        InvokeRepeating("MovePlayerPiece", moveWait, moveWait);
    }

    private void OnGUI()
    {
        if (!gameover)
        {
            guiStyle.fontSize = 20;
            GUI.Label(new Rect(120, 280, 100, 20), "Next", guiStyle);
            GUI.Label(new Rect(120, 150, 100, 20), "Score: " + score, guiStyle);
        }
        else
        {
            return;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    void StartGame()
    {
        int num = Random.Range(0, 7);
        playerpiece = Instantiate(pieces[num], new Vector3(leftwall + stepsize * 4, top - stepsize), Quaternion.identity);
        currentheight = playerpiece.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
        currentwidth = playerpiece.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        playerpiece.transform.SetParent(board.transform);
        num = Random.Range(0, 7);
        next1 = Instantiate(pieces[num], new Vector3(leftwall - stepsize * 3, bottom + stepsize * 2, 0), Quaternion.identity);
        next1.transform.SetParent(board.transform);
        num = Random.Range(0, 7);
        next2 = Instantiate(pieces[num], new Vector3(leftwall - stepsize * 3, bottom + stepsize * 6, 0), Quaternion.identity);
        next2.transform.SetParent(board.transform);
        num = Random.Range(0, 7);
        next3 = Instantiate(pieces[num], new Vector3(leftwall - stepsize * 3, bottom + stepsize * 10, 0), Quaternion.identity);
        next3.transform.SetParent(board.transform);
    }

    void UpdateScore(int multiplier)
    {
        score += 100 * (multiplier*2);
    }

    void CreatePlayerPiece()
    {
        longbuffer = 0;
        int num = Random.Range(0, 7);
        next1.transform.position = new Vector3(leftwall + stepsize * 4, top - stepsize, 0.0f);
        next2.transform.position = new Vector3(leftwall - stepsize * 3, bottom + stepsize * 2, 0.0f);
        next3.transform.position = new Vector3(leftwall - stepsize * 3, bottom + stepsize * 6, 0.0f);
        playerpiece = next1;
        next1 = next2;
        next2 = next3;
        next3 = Instantiate(pieces[num], new Vector3(leftwall - stepsize * 3, bottom + stepsize * 10, 0), Quaternion.identity);
        currentheight = playerpiece.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
        currentwidth = playerpiece.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        playerpiece.transform.SetParent(board.transform);
    }

    void MainLoop()
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
        else if (Input.GetKeyDown(KeyCode.A) && playerpiece.tag != "Box")
        {
            currenttiles.Clear();
            playerpiece.transform.Rotate(new Vector3(0, 0, -90), Space.Self);

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
        else if (Input.GetKeyDown(KeyCode.S) && playerpiece.tag != "Box")
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
        if (CheckHit())
        {
            HitBottomOrCollide();
        }
    }

    void MainLoopLong()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && playerpiece.transform.GetChild(0).position.x - longbuffer >= leftwall + 0.20)
        {
            currenttiles.Clear();
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x - stepsize, playerpiece.transform.position.y, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && playerpiece.transform.GetChild(0).position.x + longbuffer<= rightwall - 0.20)
        {
            currenttiles.Clear();
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x + stepsize, playerpiece.transform.position.y, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && playerpiece.transform.GetChild(0).position.y - stepsize * 2 + longbuffer >= bottom + 0.20)
        {
            currenttiles.Clear();
            playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            currenttiles.Clear();
            playerpiece.transform.Rotate(new Vector3(0, 0, -90), Space.Self);
            if (longbuffer == 0)
            {
                longbuffer = stepsize * 2;
            }
            else
            {
                longbuffer = 0;
            }
            if (playerpiece.transform.GetChild(0).position.x + currentwidth + longbuffer >= rightwall)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x - stepsize*2, playerpiece.transform.position.y, 0);
            }
            if (playerpiece.transform.GetChild(0).position.x - currentwidth - longbuffer <= leftwall)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x + stepsize*2, playerpiece.transform.position.y, 0);
            }
            if (playerpiece.transform.GetChild(0).position.y - currentheight <= bottom)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y + stepsize, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currenttiles.Clear();
            playerpiece.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            if (longbuffer == 0)
            {
                longbuffer = stepsize * 2;
            }
            else
            {
                longbuffer = 0;
            }
            if (playerpiece.transform.GetChild(0).position.x + currentwidth + longbuffer >= rightwall)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x - stepsize * 2, playerpiece.transform.position.y, 0);
            }
            if (playerpiece.transform.GetChild(0).position.x - currentwidth - longbuffer <= leftwall)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x + stepsize * 2, playerpiece.transform.position.y, 0);
            }
            if (playerpiece.transform.GetChild(0).position.y - currentheight <= bottom)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y + stepsize, 0);
            }
        }
        if (CheckHit())
        {
            HitBottomOrCollide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit && !gameover){
            if (playerpiece.CompareTag("Long"))
            {
                MainLoopLong();
            }
            else
            {
                MainLoop();
            }
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
        ArrayList topcleared = new ArrayList();
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
                topcleared.Add(i);
                num += 1;
                linescleared += 1;
                ClearLine(i);
                if (linescleared > 1 && moveWait >= 0.3f)
                {
                    moveWait -= 0.1f;
                    CancelInvoke("MovePlayerPiece");
                    InvokeRepeating("MovePlayerPiece", moveWait, moveWait);
                }
            }
        }
        if (num > 0)
        {
            DropLines(num, topcleared);
            UpdateScore(num);
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

    void DropLines(int num, ArrayList topcleared)
    {

        foreach (int g in topcleared)
        {
            for (int i = g; i < ysize; i++)
            {
                for (int j = 0; j < xsize; j++)
                {
                    if (tiles[j, i].CompareTag("Taken"))
                    {
                        tiles[j, i - 1].tag = "Taken";
                        tiles[j, i - 1].GetComponent<SpriteRenderer>().sprite = tiles[j, i].GetComponent<SpriteRenderer>().sprite;
                        tiles[j, i].tag = "Tile";
                        tiles[j, i].GetComponent<SpriteRenderer>().sprite = tilesprite;
                    }
                }
            }
        }
    }

    public void HitBottomOrCollide()
    {
        hit = true;
        foreach (GameObject g in currenttiles)
        {
            tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].tag = "Taken";
            if (playerpiece.CompareTag("OtherSnake"))
            {
                tiles[g.GetComponent<Position>().x, g.GetComponent<Position>().y].GetComponent<SpriteRenderer>().sprite = red;
            }
            else if (playerpiece.CompareTag("Mountain"))
            {
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
        for (int i = 0; i < xsize; i++)
        {
            if (tiles[i, ysize - 1].CompareTag("Taken")){
                gameover = true;
                CancelInvoke("MovePlayerPiece");
                Destroy(board);
                Destroy(next1);
                Destroy(next2);
                Destroy(next3);
                OnGUI();
                finalscore.text = "Final Score: " + score;
            }
        }
    }



    void MovePlayerPiece()
    {
        if (!hit)
        {
            currenttiles.Clear();
            if (!playerpiece.CompareTag("Long") && playerpiece.transform.GetChild(0).position.y - stepsize >= bottom + 0.2)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
            }
            else if (playerpiece.transform.GetChild(0).position.y - stepsize + longbuffer >= bottom + 0.2)
            {
                playerpiece.transform.position = new Vector3(playerpiece.transform.position.x, playerpiece.transform.position.y - stepsize, 0);
            }
        }
    }
}
