using UnityEngine;
using UnityEngine.InputSystem;

public class OverlayTile : MonoBehaviour
{
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
