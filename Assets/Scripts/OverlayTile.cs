using UnityEngine;
using UnityEngine.InputSystem;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;
    public int F { get { return G + H; } }
    public bool blocked = false;
    public OverlayTile previous;
    public Vector3Int location;
    void Update()
    {
        if(Mouse.current.leftButton.ReadValue() > 0)
        {
            Hide();
        }
    }

    public void Show()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}
