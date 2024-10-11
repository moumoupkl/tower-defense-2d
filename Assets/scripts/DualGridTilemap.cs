using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DualGridTilemap : MonoBehaviour
{
    public Tilemap baseTilemap;
    public Tilemap overlayTilemap;
    public List<TileBase> customTiles; // Ensure this list is populated with 16 custom tiles
    public TileBase Grass; // Assign this in the Unity Editor
    public TileBase Dirt; // Assign this in the Unity Editor

    // Dictionary to map tuples of neighboring tiles to custom tiles
    private Dictionary<(TileBase, TileBase, TileBase, TileBase), TileBase> neighbourTupleToTile;

    // Start is called before the first frame update
    void Start()
    {
        InitializeNeighbourTupleToTile();
        InitializeOverlayTilemap();
        // Deactivate the base tilemap renderer
        baseTilemap.GetComponent<TilemapRenderer>().enabled = false;
    }

    void InitializeNeighbourTupleToTile()
    {
        neighbourTupleToTile = new Dictionary<(TileBase, TileBase, TileBase, TileBase), TileBase>
        {
            {(Grass, Grass, Grass, Grass), customTiles[6]}, // CENTER_GRASS
            {(Dirt, Dirt, Dirt, Grass), customTiles[13]}, // OUTER_BOTTOM_RIGHT
            {(Dirt, Dirt, Grass, Dirt), customTiles[0]}, // OUTER_BOTTOM_LEFT
            {(Dirt, Grass, Dirt, Dirt), customTiles[8]}, // OUTER_TOP_RIGHT
            {(Grass, Dirt, Dirt, Dirt), customTiles[15]}, // OUTER_TOP_LEFT
            {(Dirt, Grass, Dirt, Grass), customTiles[1]}, // EDGE_RIGHT
            {(Grass, Dirt, Grass, Dirt), customTiles[11]}, // EDGE_LEFT
            {(Dirt, Dirt, Grass, Grass), customTiles[3]}, // EDGE_BOTTOM
            {(Grass, Grass, Dirt, Dirt), customTiles[9]}, // EDGE_TOP
            {(Dirt, Grass, Grass, Grass), customTiles[5]}, // INNER_BOTTOM_RIGHT
            {(Grass, Dirt, Grass, Grass), customTiles[2]}, // INNER_BOTTOM_LEFT
            {(Grass, Grass, Dirt, Grass), customTiles[10]}, // INNER_TOP_RIGHT
            {(Grass, Grass, Grass, Dirt), customTiles[7]}, // INNER_TOP_LEFT
            {(Dirt, Grass, Grass, Dirt), customTiles[14]}, // DUAL_UP_RIGHT
            {(Grass, Dirt, Dirt, Grass), customTiles[4]}, // DUAL_DOWN_RIGHT
            {(Dirt, Dirt, Dirt, Dirt), customTiles[12]}, // CENTER_DIRT
        };
    }

    void InitializeOverlayTilemap()
    {
        // Ensure the overlayTilemap size matches the expected dimensions
        BoundsInt bounds = baseTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int basePos1 = new Vector3Int(x, y, 0);
                Vector3Int basePos2 = new Vector3Int(x + 1, y, 0);
                Vector3Int basePos3 = new Vector3Int(x, y - 1, 0);
                Vector3Int basePos4 = new Vector3Int(x + 1, y - 1, 0);

                TileBase baseTile1 = baseTilemap.GetTile<TileBase>(basePos1);
                TileBase baseTile2 = baseTilemap.GetTile<TileBase>(basePos2);
                TileBase baseTile3 = baseTilemap.GetTile<TileBase>(basePos3);
                TileBase baseTile4 = baseTilemap.GetTile<TileBase>(basePos4);

                // Determine the custom tile based on the four base tiles
                TileBase customTile = DetermineCustomTile(baseTile1, baseTile2, baseTile3, baseTile4);

                if (customTile != null)
                {
                    // Offset the position by 0.5 to the left and 0.5 to the top
                    Vector3Int offsetPos = new Vector3Int(x, y - 1, 0);
                    overlayTilemap.SetTile(offsetPos, customTile);
                }
            }
        }
    }

    TileBase DetermineCustomTile(TileBase baseTile1, TileBase baseTile2, TileBase baseTile3, TileBase baseTile4)
    {
        // Use the dictionary to find the custom tile based on the neighboring tiles
        var key = (baseTile1, baseTile2, baseTile3, baseTile4);
        if (neighbourTupleToTile.TryGetValue(key, out TileBase customTile))
        {
            return customTile;
        }
        return null;
    }
}