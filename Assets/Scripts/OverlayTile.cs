using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
