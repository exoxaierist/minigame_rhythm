using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandler : MonoBehaviour
{
    public int mode = 0;
    private List<SpriteRenderer> sprList = new();
    private bool switchTile = false;
    private int colIndex = 0;
    private Color offCol;
    public Color col1;
    public Color col2;
    public Color col3;
    [Space(10)]
    public Sprite empty;
    public Sprite full;

    private void Awake()
    {
        Global.OnBeat += UpdateTile;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            sprList.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        offCol = sprList[0].color;
        if (TryGetComponent(out TilemapRenderer tilemap)) tilemap.enabled = false;
    }

    private void UpdateTile()
    {
        switchTile = !switchTile;
        colIndex = (colIndex + 1) % 3;
        Color randCol = colIndex == 0 ? col1 : colIndex == 1 ? col2 : col3;
        foreach (SpriteRenderer sprite in sprList)
        {
            bool condition = (sprite.transform.position.x+sprite.transform.position.y) % 2 == 0;
            if (switchTile?condition:!condition)
            {
                if(mode == 0)
                {
                    sprite.DOComplete();
                    sprite.DOColor(randCol, 0.2f).SetDelay(0.1f);
                }
                else
                {
                    sprite.sprite = full;
                }
            }
            else
            {
                if (mode == 0)
                {
                    sprite.DOComplete();
                    sprite.DOColor(offCol, 0.2f);
                }
                else
                {
                    sprite.sprite = empty;
                }
            }
        }
    }
}
