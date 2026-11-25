using System.Collections;
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
    private bool range = false;

    void Update()
    {
        if(Mouse.current.leftButton.ReadValue() > 0 && !range)
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

    public IEnumerator ShowRange()
    {
        range = true;
        yield return new WaitForSeconds(0.1f);
        range = false;
    }
}
