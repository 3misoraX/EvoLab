using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public int hp;
    public int atk;
    public int def;
    public int lvlToEvolve;
}
