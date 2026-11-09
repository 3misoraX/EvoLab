using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public GameObject unitPrefab;
    public UnitMovement unit;
    // Update is called once per frame
    void LateUpdate()
    {
        var focusTile = GetFocusTile();

        if (focusTile.HasValue)
        {
            GameObject overlay = focusTile.Value.collider.gameObject;
            transform.position = overlay.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlay.GetComponent<SpriteRenderer>().sortingOrder;

            if(Mouse.current.leftButton.ReadValue() > 0)
            {
                overlay.GetComponent<OverlayTile>().Show();

                if(unit == null)
                {
                    unit = Instantiate(unitPrefab).GetComponent<UnitMovement>();
                    PositionUnit(overlay.GetComponent<OverlayTile>());
                }
            }
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
}
