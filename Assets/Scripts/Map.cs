using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    //creating a singleton
    private static Map _instance;
    public static Map Instance {  get { return _instance; } }
    //variables for creating the grid
    public OverlayTile overlay;
    public GameObject overlayContainer;
    public Dictionary<Vector2Int, OverlayTile> map;

    //singleton code
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // looping through all the tiles
    void Start()
    {
        var tilemap = gameObject.GetComponentInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTile>();
        BoundsInt bounds = tilemap.cellBounds;
        for(int z = bounds.min.z; z < bounds.max.z; z++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for(int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    var tileLocation = new Vector3Int(x, y, z);
                    var tileKey = new Vector2Int(x, y);
                    if (tilemap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                    {
                        var overlayTile = Instantiate(overlay, overlayContainer.transform);
                        var cellWorldPosition = tilemap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder+1;
                        overlayTile.location = tileLocation;
                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }
    }
}
