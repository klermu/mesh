// Code written by Gemini 3.x

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshNormalVisualizer : MonoBehaviour
{
    [Header("Toggle Controls")]
    public bool showOnlyWhenSelected = true;
    public bool showFaceNormals = true;
    public bool showVertexNormals = true; // Added toggle for vertex normals

    [Header("Normal Line Styling")]
    public Color normalColor = Color.cyan;
    public Color vertexNormalColor = Color.magenta; // Color to distinguish vertex normals from face normals
    [Range(0.05f, 10f)] public float normalLength = 0.5f;

    [Header("Base Disc Styling")]
    public Color baseDiscColor = Color.blue;
    public Color vertexDiscColor = Color.red; // Disc color for vertex normal anchors
    [Range(0.01f, 1f)] public float baseDiscSize = 0.05f;

    private void OnDrawGizmos() => DrawNormals(false);
    private void OnDrawGizmosSelected() => DrawNormals(true);

    private void DrawNormals(bool isSelectedMode)
    {
        if (showOnlyWhenSelected != isSelectedMode) return;

        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null || filter.sharedMesh == null) return;

        Mesh mesh = filter.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;

        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        // --- 1. DRAW FACE NORMALS ---
        if (showFaceNormals && triangles != null)
        {
            for (int i = 0; i < triangles.Length; i += 3)
            {
                // Convert positions to World Space
                Vector3 w0 = localToWorld.MultiplyPoint3x4(vertices[triangles[i]]);
                Vector3 w1 = localToWorld.MultiplyPoint3x4(vertices[triangles[i + 1]]);
                Vector3 w2 = localToWorld.MultiplyPoint3x4(vertices[triangles[i + 2]]);

                // Find the face center (Centroid)
                Vector3 faceCenter = (w0 + w1 + w2) / 3f;

                // Compute normal vector via cross product
                Vector3 sideA = w1 - w0;
                Vector3 sideB = w2 - w0;
                Vector3 faceNormal = Vector3.Cross(sideA, sideB).normalized;

#if UNITY_EDITOR
                // Draw the solid disc at the anchor base pointing along the normal axis
                UnityEditor.Handles.color = baseDiscColor;
                UnityEditor.Handles.DrawSolidDisc(faceCenter, faceNormal, baseDiscSize);
#endif

                // Draw the directional pointer line protruding outwards
                Gizmos.color = normalColor;
                Gizmos.DrawLine(faceCenter, faceCenter + faceNormal * normalLength);
            }
        }

        // --- 2. DRAW VERTEX NORMALS ---
        if (showVertexNormals && normals != null && normals.Length == vertices.Length)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                // Convert vertex position to World Space
                Vector3 worldPos = localToWorld.MultiplyPoint3x4(vertices[i]);

                // Convert local vertex normal to World Space direction
                Vector3 worldNormal = localToWorld.MultiplyVector(normals[i]).normalized;

#if UNITY_EDITOR
                UnityEditor.Handles.color = vertexDiscColor;
                UnityEditor.Handles.DrawSolidDisc(worldPos, worldNormal, baseDiscSize);
#endif

                Gizmos.color = vertexNormalColor;
                Gizmos.DrawLine(worldPos, worldPos + worldNormal * normalLength);
            }
        }
    }
}