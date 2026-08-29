// Code written by Gemini 3.x

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshVertexVisualizer : MonoBehaviour
{
    [Header("Toggle Controls")]
    [Tooltip("Show labels only when this GameObject is clicked.")]
    public bool showOnlyWhenSelected = true;
    public bool showIndices = true;
    public bool showPositions = false;
    public bool concatenateIdenticalVertices = true; // Added toggle for combining identical vertices

    // x stores the min index, y stores the max index
    [HideInInspector]
    public Vector2 vertexDisplayRange = new Vector2(0, 10);

    [Header("Styling")]
    [Range(8, 24)] public int fontSize = 12;
    public Color fontColor = Color.green;
    public float handleSize = 0.02f;
    public Color handleColor = Color.red;

    private GUIStyle _labelStyle;

    // Static cached collections to prevent GC garbage allocation inside Gizmos loops
    private static readonly StringBuilder _sb = new StringBuilder(64);
    private static readonly Dictionary<Vector3, List<int>> _groupedVertices = new Dictionary<Vector3, List<int>>();
    private static readonly List<Vector3> _processedPositions = new List<Vector3>();

    private void OnDrawGizmos() => DrawRange(false);
    private void OnDrawGizmosSelected() => DrawRange(true);

    private void DrawRange(bool isSelectedMode)
    {
        if (showOnlyWhenSelected != isSelectedMode) return;
        DrawVertexLabels();
    }

    private void DrawVertexLabels()
    {
#if UNITY_EDITOR
        if (!showIndices && !showPositions) return;

        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null || filter.sharedMesh == null) return;

        Mesh mesh = filter.sharedMesh;
        Vector3[] vertices = mesh.vertices;

        if (_labelStyle == null) _labelStyle = new GUIStyle();
        _labelStyle.fontSize = fontSize;
        _labelStyle.normal.textColor = fontColor;

        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        // Extract int boundaries from our slider Vector2
        int minIdx = Mathf.Clamp(Mathf.FloorToInt(vertexDisplayRange.x), 0, vertices.Length - 1);
        int maxIdx = Mathf.Clamp(Mathf.CeilToInt(vertexDisplayRange.y), 0, vertices.Length - 1);

        // Clear cached buffers without re-instantiating them
        _groupedVertices.Clear();
        _processedPositions.Clear();

        // 1. Group vertices sharing the same local position within the selected index range
        for (int i = minIdx; i <= maxIdx; i++)
        {
            if (i >= vertices.Length) break;

            Vector3 pos = vertices[i];

            if (concatenateIdenticalVertices)
            {
                if (!_groupedVertices.TryGetValue(pos, out List<int> indexList))
                {
                    indexList = new List<int>();
                    _groupedVertices[pos] = indexList;
                    _processedPositions.Add(pos);
                }
                indexList.Add(i);
            }
            else
            {
                // Uncombined mode: process each point individually
                _processedPositions.Add(pos);
            }
        }

        // 2. Render labels and handles
        Vector3 textOffset = new Vector3(handleSize * 1.5f, handleSize * 1.5f, 0);

        for (int p = 0; p < _processedPositions.Count; p++)
        {
            Vector3 localPos = _processedPositions[p];
            Vector3 worldPos = localToWorld.MultiplyPoint3x4(localPos);

            UnityEditor.Handles.color = handleColor;
            UnityEditor.Handles.DrawSolidDisc(worldPos, Vector3.back, handleSize);

            // Construct label using static StringBuilder to eliminate GC string allocations
            _sb.Clear();

            if (concatenateIdenticalVertices)
            {
                List<int> indices = _groupedVertices[localPos];

                if (showIndices)
                {
                    for (int k = 0; k < indices.Count; k++)
                    {
                        if (k > 0) _sb.Append(", ");
                        _sb.Append(indices[k]);
                    }
                }

                if (showPositions)
                {
                    if (showIndices) _sb.AppendLine();
                    _sb.Append('(').Append(localPos.x.ToString("F2")).Append(", ").Append(localPos.y.ToString("F2")).Append(')');
                }
            }
            else
            {
                int i = minIdx + p;
                if (showIndices) _sb.Append(i);
                if (showPositions)
                {
                    if (showIndices) _sb.AppendLine();
                    _sb.Append('(').Append(localPos.x.ToString("F2")).Append(", ").Append(localPos.y.ToString("F2")).Append(')');
                }
            }

            UnityEditor.Handles.Label(worldPos + textOffset, _sb.ToString(), _labelStyle);
        }
#endif
    }
}