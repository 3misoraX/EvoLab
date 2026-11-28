using UnityEngine;

public class Arrows
{
    public enum ArrowDirection
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
        ContinueVertical = 5,
        ContinueHorizontal = 6,
        TurnUpRight = 7,
        TurnUpLeft = 8,
        TurnDownRight = 9,
        TurnDownLeft = 10
    }

    public ArrowDirection Translate(OverlayTile current, OverlayTile previous, OverlayTile future)
    {
        bool isFinal = future == null;

        Vector2Int previousDirection = previous != null ? current.location2D - previous.location2D : Vector2Int.zero;
        Vector2Int futureDirection = future != null ? future.location2D - current.location2D : Vector2Int.zero;
        Vector2Int direction = previousDirection != futureDirection ? previousDirection + futureDirection : futureDirection;

        //esto va a ser horrible :)
        //perdone estimado lector

        if ((direction == new Vector2Int(0, 1) && !isFinal) || (direction == new Vector2Int(0, -1) && !isFinal))
        {
            return ArrowDirection.ContinueVertical;
        }
        else if ((direction == new Vector2Int(1, 0) && !isFinal) || (direction == new Vector2Int(-1, 0) && !isFinal))
        {
            return ArrowDirection.ContinueHorizontal;
        }
        else if(direction == new Vector2Int(1, 1))
        {
            if(previousDirection == new Vector2Int(0, 1))
            {
                return ArrowDirection.TurnUpRight;
            }
            else
            {
                return ArrowDirection.TurnDownLeft;
            }
        }
        else if(direction == new Vector2Int(-1, 1))
        {
            if(previousDirection == new Vector2Int(0, 1))
            {
                return ArrowDirection.TurnUpLeft;
            }
            else
            {
                return ArrowDirection.TurnDownRight;
            }
        }
        else if(direction == new Vector2Int(1, -1))
        {
            if(previousDirection == new Vector2Int(0, -1))
            {
                return ArrowDirection.TurnDownRight;
            }
            else
            {
                return ArrowDirection.TurnUpLeft;
            }
        }
        else if(direction == new Vector2Int(-1, -1))
        {
            if(previousDirection == new Vector2Int(0, -1))
            {
                return ArrowDirection.TurnDownLeft;
            }
            else
            {
                return ArrowDirection.TurnUpRight;
            }
        }
        else if(direction == new Vector2Int(1, 0) && isFinal)
        {
            return ArrowDirection.Right;
        }
        else if(direction == new Vector2Int(-1, 0) && isFinal)
        {
            return ArrowDirection.Left;
        }
        else if(direction == new Vector2Int(0, 1)&& isFinal)
        {
            return ArrowDirection.Up;
        }
        else if(direction == new Vector2Int(0, -1) && isFinal)
        {
            return ArrowDirection.Down;
        }
        else
        {
            return ArrowDirection.None;
        }
    }
}
