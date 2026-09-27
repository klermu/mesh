using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class chicken : MonoBehaviour
{
    [Header("Body")]
    public float bodyRadius = 0.6f;
    public int bodyLongitude = 24;
    public int bodyLatitude = 16;
    public Color bodyColor = new Color(1f, 0.95f, 0.8f);

    [Header("Legs")]
    public int legCount = 2;
    public int legSamples = 18; // points along spline
    public int legRingSegments = 8; // cross-section resolution
    public float legRadius = 0.06f;
    public float footRadius = 0.09f;
    public Color legColor = new Color(0.9f, 0.7f, 0.5f);

    void Start()
    {
        GenerateChicken();
    }

    // Simple entry to generate body + legs
    public void GenerateChicken()
    {
        // create body mesh filter/renderer on this GameObject
        var mf = GetComponent<MeshFilter>();
        var mr = GetComponent<MeshRenderer>();
        mf.sharedMesh = BuildEllipsoidMesh(bodyRadius, bodyLatitude, bodyLongitude);
        mr.sharedMaterial = new Material(Shader.Find("Standard")) { color = bodyColor };

        // create legs
        for (int i = 0; i < legCount; i++)
        {
            float side = (i == 0) ? -1f : 1f;
            var legGO = new GameObject($"Leg_{i}");
            legGO.transform.parent = transform;
            legGO.transform.localPosition = Vector3.zero;
            // control points for a simple curved leg spline: hip -> knee -> ankle -> foot
            Vector3 hip = new Vector3(side * 0.18f, -bodyRadius * 0.45f, 0.05f);
            Vector3 knee = hip + new Vector3(side * 0.02f, -0.25f, 0.02f);
            Vector3 ankle = hip + new Vector3(side * 0.02f, -0.55f, -0.02f);
            Vector3 foot = hip + new Vector3(side * 0.06f, -0.7f, 0.08f);

            var spline = SampleQuadraticBezier(new Vector3[] { hip, knee, ankle, foot }, legSamples);

            var legMF = legGO.AddComponent<MeshFilter>();
            var legMR = legGO.AddComponent<MeshRenderer>();
            legMF.sharedMesh = BuildTubeAlongSpline(spline, legRingSegments, legRadius);
            legMR.sharedMaterial = new Material(Shader.Find("Standard")) { color = legColor };

            // foot as a small sphere at end
            var footGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            footGO.name = $"Foot_{i}";
            footGO.transform.parent = legGO.transform;
            footGO.transform.localScale = Vector3.one * footRadius * 2f;
            footGO.transform.localPosition = transform.InverseTransformPoint(spline[spline.Count - 1]);
            var footR = footGO.GetComponent<Renderer>();
            footR.sharedMaterial = new Material(Shader.Find("Standard")) { color = legColor };
        }
    }

    // Build an ellipsoid (squashed sphere) mesh centered at origin
    Mesh BuildEllipsoidMesh(float radius, int lat, int lon)
    {
        var mesh = new Mesh();
        var verts = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var tris = new List<int>();

        for (int y = 0; y <= lat; y++)
        {
            float v = (float)y / lat;
            float phi = Mathf.PI * (v - 0.5f);
            for (int x = 0; x <= lon; x++)
            {
                float u = (float)x / lon;
                float theta = u * Mathf.PI * 2f;
                float cx = Mathf.Cos(theta) * Mathf.Cos(phi);
                float cy = Mathf.Sin(phi);
                float cz = Mathf.Sin(theta) * Mathf.Cos(phi);
                Vector3 pos = new Vector3(cx, cy, cz) * radius;
                // slightly elongate in x/z for a chicken-body look
                pos.x *= 0.9f;
                pos.z *= 1.05f;
                verts.Add(pos);
                normals.Add(pos.normalized);
                uvs.Add(new Vector2(u, v));
            }
        }

        for (int y = 0; y < lat; y++)
        {
            for (int x = 0; x < lon; x++)
            {
                int i0 = y * (lon + 1) + x;
                int i1 = i0 + 1;
                int i2 = i0 + (lon + 1);
                int i3 = i2 + 1;
                tris.Add(i0); tris.Add(i2); tris.Add(i1);
                tris.Add(i1); tris.Add(i2); tris.Add(i3);
            }
        }

        mesh.SetVertices(verts);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateBounds();
        return mesh;
    }

    // Sample a cubic-like spline using chained quadratic Beziers for smoothness
    List<Vector3> SampleQuadraticBezier(Vector3[] control, int samples)
    {
        // If 4 control points given, make two quadratic segments (0-1-2, 2-3-?) but here we'll just use a simple Catmull-like interpolation
        var pts = new List<Vector3>();
        if (control.Length >= 4)
        {
            // Approximate with cubic bezier via the four points
            for (int i = 0; i < samples; i++)
            {
                float t = (float)i / (samples - 1);
                // use Catmull-Rom style blend of the four control points
                Vector3 p = CatmullRom(control[0], control[1], control[2], control[3], t);
                pts.Add(transform.TransformPoint(p));
            }
        }
        else
        {
            for (int i = 0; i < samples; i++) pts.Add(transform.TransformPoint(control[control.Length - 1]));
        }
        return pts;
    }

    // Catmull-Rom spline between p1 and p2 using p0 and p3 as tangential neighbors
    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;
        return 0.5f * ((2f * p1) + (-p0 + p2) * t + (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 + (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }

    // Build a simple tube mesh along world-space spline points
    Mesh BuildTubeAlongSpline(List<Vector3> pts, int ringSegments, float radius)
    {
        var mesh = new Mesh();
        var verts = new List<Vector3>();
        var norms = new List<Vector3>();
        var uvs = new List<Vector2>();
        var tris = new List<int>();

        if (pts.Count < 2) return mesh;

        // create frames using forward vector and arbitrary up
        Vector3 prevUp = Vector3.up;
        for (int i = 0; i < pts.Count; i++)
        {
            Vector3 forward;
            if (i == pts.Count - 1) forward = (pts[i] - pts[i - 1]).normalized;
            else forward = (pts[i + 1] - pts[i]).normalized;
            Vector3 right = Vector3.Cross(prevUp, forward).normalized;
            if (right.sqrMagnitude < 0.001f) right = Vector3.Cross(Vector3.up, forward).normalized;
            Vector3 up = Vector3.Cross(forward, right).normalized;
            prevUp = up;

            for (int r = 0; r < ringSegments; r++)
            {
                float a = (float)r / ringSegments * Mathf.PI * 2f;
                Vector3 offset = (Mathf.Cos(a) * right + Mathf.Sin(a) * up) * radius;
                verts.Add(transform.InverseTransformPoint(pts[i] + offset));
                norms.Add((pts[i] + offset - transform.InverseTransformPoint(pts[i])).normalized);
                uvs.Add(new Vector2((float)r / ringSegments, (float)i / (pts.Count - 1)));
            }
        }

        int rings = pts.Count;
        for (int i = 0; i < rings - 1; i++)
        {
            for (int r = 0; r < ringSegments; r++)
            {
                int nextR = (r + 1) % ringSegments;
                int a = i * ringSegments + r;
                int b = i * ringSegments + nextR;
                int c = (i + 1) * ringSegments + r;
                int d = (i + 1) * ringSegments + nextR;
                tris.Add(a); tris.Add(c); tris.Add(b);
                tris.Add(b); tris.Add(c); tris.Add(d);
            }
        }

        mesh.SetVertices(verts);
        mesh.SetNormals(norms);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateBounds();
        return mesh;
    }
}
