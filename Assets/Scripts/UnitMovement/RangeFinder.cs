using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RangeFinder
{
    public List<OverlayTile> GetTilesInRange(OverlayTile start, int range)
    {
        var inRangeTiles = new List<OverlayTile>();
        int stepCount = 0;

        inRangeTiles.Add(start);

        var previousTile = new List<OverlayTile>();
        previousTile.Add(start);

        while (stepCount < range)
        {
            var surrounding = new List<OverlayTile>();
            foreach(var tile in previousTile)
            {
                surrounding.AddRange(Map.Instance.GetNeighbors(tile));
            }

            inRangeTiles.AddRange(surrounding);
            previousTile = surrounding.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }
}
