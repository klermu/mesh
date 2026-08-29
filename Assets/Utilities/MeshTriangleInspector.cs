// Code written by Gemini 3.x

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshTriangleInspector : MonoBehaviour
{
    [Header("Hover Customization")]
    public Color hoverOverlayColor = new Color(1f, 0.92f, 0.016f, 0.3f); // Translucent yellow
    public Color outlineColor = Color.yellow;

    [Header("Text Info Styling")]
    public Color textColor = Color.white;
    [Range(10, 20)] public int fontSize = 14;

    // The inspector custom editor will update this value dynamically
    [HideInInspector]
    public int hoveredTriangleIndex = -1;
}