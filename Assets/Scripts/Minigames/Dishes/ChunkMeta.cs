using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChunkMeta
{
    public enum ChunkType {
        DISH,
        WALL
    }

    public ChunkMeta(Color color)
    {
        this.Color = color;
        this.pointList = new List<Vector2Int>();
        this.chunkType = ChunkType.DISH;
    }

    private Color color;

    private List<Vector2Int> pointList;

    private ChunkType chunkType;

    public Color Color { get => color; set => color = value; }
    public List<Vector2Int> PointList { get => pointList; set => pointList = value; }
    public ChunkType ChunkTypeValue { get => chunkType; set => chunkType = value; }

    public void AddPoint(Vector2Int point) {
        pointList.Add(point);
    }

    public void RemovePoint(Vector2Int point) {
        pointList.Remove(point);
    }

    public void MergeChunk(ChunkMeta chunk) {
        pointList = pointList.Concat(chunk.PointList).ToList();
    }

    public static ChunkMeta GenerateChunk() {
        return new ChunkMeta(Random.ColorHSV());
    }

    public void NormalizePoints() {

        int minX = pointList.Min(point => point.x);
        int minY = pointList.Min(point => point.y);

        List<Vector2Int> normalizedPoints = new List<Vector2Int>();

        foreach(Vector2Int point in pointList) {
            normalizedPoints.Add(new Vector2Int(point.x - minX, point.y - minY));
        }

        this.pointList = normalizedPoints;        
    }

    public Vector2Int GetGridBounds() {
        return new Vector2Int(pointList.Max(point => point.x) + 1, 
                              pointList.Max(point => point.y) + 1);
    }

    public void SetWall(Color wallColor) {
        this.color = wallColor;
        this.ChunkTypeValue = ChunkType.WALL;
    }

    public bool IsWall() {
        return this.ChunkTypeValue == ChunkType.WALL;
    }

    public void RotateClockwise() {
        Vector2Int gridBounds = GetGridBounds();
        List<Vector2Int> rotatedPoints = new List<Vector2Int>();
        foreach(Vector2Int point in pointList) {
            rotatedPoints.Add(new Vector2Int(gridBounds.y - 1 - point.y, point.x));
        }
        this.pointList = rotatedPoints;
    }

    public void RotateCounterClockwise() {
        Vector2Int gridBounds = GetGridBounds();
        List<Vector2Int> rotatedPoints = new List<Vector2Int>();
        foreach(Vector2Int point in pointList) {
            rotatedPoints.Add(new Vector2Int(point.y, gridBounds.x - 1 - point.x));
        }
        this.pointList = rotatedPoints;
    }

    public bool OnEdge(Vector2Int gridBounds) {
        return pointList.Exists(point => point.x.Equals(0) || point.x.Equals(gridBounds.x - 1) ||
                                         point.y.Equals(0) || point.y.Equals(gridBounds.y - 1));
    }

}
