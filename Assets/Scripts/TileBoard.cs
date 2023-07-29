using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileBoard : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip clip;

    public GameManager gameManager;
    public Tile tilePrefab;
    public TileState[] tileStates;

    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;
    private bool gameover = true;

    private Vector2 touchStartPos;
    private bool isSwiping;
    private float minSwipeDistance = 50f;

    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
    }

    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }

    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Update()
    {
        if (gameover == true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector2Int.up, 0, 1, 1, 1);
                audio.PlayOneShot(clip);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2Int.left, 1, 1, 0, 1);
                audio.PlayOneShot(clip);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector2Int.down, 0, 1, grid.height - 2, -1);
                audio.PlayOneShot(clip);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2Int.right, grid.width - 2, -1, 0, 1);
                audio.PlayOneShot(clip);
            }
            else if (Input.touchCount > 0)
            {
                HandleSwipeInput();
            }
        }
    }

    private void HandleSwipeInput()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                isSwiping = true;
                break;

            case TouchPhase.Ended:
                if (isSwiping)
                {
                    Vector2 swipeDelta = touch.position - touchStartPos;

                    if (swipeDelta.magnitude > minSwipeDistance)
                    {
                        swipeDelta.Normalize();

                        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                        {
                            if (swipeDelta.x > 0)
                            {
                                Move(Vector2Int.right, grid.width - 2, -1, 0, 1);
                                audio.PlayOneShot(clip);
                            }
                            else
                            {
                                Move(Vector2Int.left, 1, 1, 0, 1);
                                audio.PlayOneShot(clip);
                            }
                        }
                        else
                        {
                            if (swipeDelta.y > 0)
                            {
                                Move(Vector2Int.up, 0, 1, 1, 1);
                                audio.PlayOneShot(clip);
                            }
                            else
                            {
                                Move(Vector2Int.down, 0, 1, grid.height - 2, -1);
                                audio.PlayOneShot(clip);
                            }
                        }
                    }
                }
                isSwiping = false;
                break;
        }
    }

    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;

        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }

        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    MergeTiles(tile, adjacent.tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.number == b.number && !b.locked;
    }

    private void MergeTiles(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        int number = b.number * 2;

        b.SetState(tileStates[index], number);

        gameManager.IncreaseScore(number);
    }

    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i])
            {
                return i;
            }
        }

        return -1;
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.size)
        {
            CreateTile();
        }

        if (CheckForGameOver())
        {
            yield return new WaitForSeconds(3);
            gameManager.GameOver();
            Debug.Log("No moves available. Game over!");
            //SceneManager.LoadScene("Gameover");
            LoadNewScene("Gameover");
        }
    }
    public void LoadNewScene(string sceneName)
    {
        // Save the score before switching to the new scene.
        FindObjectOfType<GameManager>().SaveScore();
        SceneManager.LoadScene("Gameover");
    }

    public bool CheckForGameOver()
    {
        if (tiles.Count != grid.size)
        {
            return false;
        }

        bool movesAvailable = false;

        foreach (var tile in tiles)
        {
            TileCell[] adjacentCells = grid.GetAdjacentCells(tile.cell);

            foreach (var adjacentCell in adjacentCells)
            {
                if (adjacentCell != null && CanMerge(tile, adjacentCell.tile))
                {
                    movesAvailable = true;
                    break;
                }
            }

            if (movesAvailable)
            {
                break;
            }
        }

        return !movesAvailable;
    }
}
