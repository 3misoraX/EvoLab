using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Arrows;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;
    public int F { get { return G + H; } }
    public bool blocked = false;
    public OverlayTile previous;
    public Vector3Int location;
    public Vector2Int location2D { get { return new Vector2Int(location.x, location.y); } }
    [SerializeField]
    private SpriteRenderer arrow;
    [SerializeField]
    private SpriteRenderer tile;

    public List<Sprite> arrows;

    public void Show()
    {
        tile.color = new Color(1, 1, 1, 1);
    }

    public void Hide()
    {
        tile.color = new Color(1, 1, 1, 0);
        DisplayArrow(ArrowDirection.None);
    }

    public void DisplayArrow(ArrowDirection d)
    {
        if(d == ArrowDirection.None)
        {
            arrow.color = new Color(1, 1, 1, 0);
        }
        else
        {
            arrow.color = new Color(1, 1, 1, 1);
            arrow.sprite = arrows[(int)d];
            arrow.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder+1;
        }
    }
}
