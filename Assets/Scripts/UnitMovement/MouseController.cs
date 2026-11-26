using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using static Arrows;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public float speed = 2;
    public int range = 3;

    public GameObject unitPrefab;
    [SerializeField]
    private UnitMovement unit;

    private PathFinding pathFinder;
    private RangeFinder rangeFinder;
    private Arrows translator;
    public List<OverlayTile> path = new List<OverlayTile>();
    public List<OverlayTile> inRange = new List<OverlayTile>();
    [SerializeField]
    private bool moving = false;

    void Start()
    {
        pathFinder = new PathFinding();
        rangeFinder = new RangeFinder();
        translator = new Arrows();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusTile = GetFocusTile();

        if (focusTile.HasValue)
        {
            GameObject overlay = focusTile.Value.collider.gameObject;
            transform.position = overlay.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlay.GetComponent<SpriteRenderer>().sortingOrder;

            if (inRange.Contains(overlay.GetComponent<OverlayTile>()) && !moving)
            {
                path = pathFinder.Path(unit.activeTile, overlay.GetComponent<OverlayTile>());

                foreach(var tile in inRange)
                {
                    tile.DisplayArrow(ArrowDirection.None);
                }

                for(int i = 0; i < path.Count; i++)
                {
                    var previous = i > 0 ? path[i - 1] : unit.activeTile;
                    var next = i < path.Count - 1 ? path[i + 1] : null;
                    ArrowDirection dir = translator.Translate(path[i], previous, next);
                    path[i].DisplayArrow(dir);
                }
            }

            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                if(unit == null)
                {
                    try
                    {
                        unit = GameObject.FindWithTag("Unit").GetComponent<UnitMovement>();
                    }
                    catch
                    {
                        unit = Instantiate(unitPrefab).GetComponent<UnitMovement>();
                    }
                    PositionUnit(overlay.GetComponent<OverlayTile>());
                    GetTilesInRange();
                }
                else if(unit != null && overlay.GetComponent<OverlayTile>().blocked == false)
                {
                    moving = true;
                }
            }
        }

        if(path.Count > 0 && moving)
        {
            MoveUnit();
        }

    }

    public void MoveUnit()
    {
        var step = speed * Time.deltaTime;
        var zIndex = path[0].transform.position.z;

        unit.transform.position = Vector2.MoveTowards(unit.transform.position, path[0].transform.position, step);
        unit.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y, zIndex);

        if(Vector2.Distance(unit.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionUnit(path[0]);
            path.RemoveAt(0);
        }

        if(path.Count == 0)
        {
            GetTilesInRange();
            moving = false;
        }
    }

    public RaycastHit2D? GetFocusTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    public void PositionUnit(OverlayTile tile)
    {
        unit.transform.position = tile.transform.position;
        unit.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder+1;
        unit.activeTile = tile;
    }

    private void GetTilesInRange()
    {
        foreach (var tile in inRange)
        {
            tile.Hide();
            tile.blocked = true;
        }

        inRange = rangeFinder.GetTilesInRange(unit.activeTile, range);

        foreach(var tile in inRange)
        {
            tile.Show();
            tile.blocked = false;
        }
    }
}
