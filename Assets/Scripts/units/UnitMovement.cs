using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Dependencies")]
    public OverlayTile activeTile;
    public Unit form;
    [HideInInspector]
    public int maxHp;
    private int hp;

    [HideInInspector]
    public int atk;

    [HideInInspector]
    public int def;
    public int lvl = 1;

    void Start()
    {
        maxHp = form.hp;
        hp = maxHp;
        atk = form.atk;
        def = form.def;
    }
}
