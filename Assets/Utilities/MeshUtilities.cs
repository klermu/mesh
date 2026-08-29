using System.Collections;
using UnityEngine;

public class MeshUtilities
{
    // NOTE: size = extents * 2
	public static Mesh Cube(float extent)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4 * 6]
        {
            //front
		    new Vector3(-extent,-extent,-extent),
            new Vector3(extent, -extent, -extent),
            new Vector3(extent, extent, -extent),
            new Vector3(-extent, extent, -extent),
            
            // back
            new Vector3(-extent, -extent, extent),
            new Vector3(extent, -extent, extent),
            new Vector3(extent, extent, extent),
            new Vector3(-extent, extent, extent),
            
            // left
            new Vector3(-extent, -extent, -extent),
            new Vector3(-extent, extent, -extent),
            new Vector3(-extent, extent, extent),
            new Vector3(-extent, -extent, extent),
            
            // right
            new Vector3(extent, -extent, -extent),
            new Vector3(extent, extent, -extent),
            new Vector3(extent, extent, extent),
            new Vector3(extent, -extent, extent),
            
            // bottom
            new Vector3(-extent, -extent, -extent),
            new Vector3(-extent, -extent, extent),
            new Vector3(extent, -extent, extent),
            new Vector3(extent, -extent, -extent),
            
            // top
            new Vector3(-extent, extent, -extent),
            new Vector3(-extent, extent, extent),
            new Vector3(extent, extent, extent),
            new Vector3(extent, extent, -extent)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6 * 2 * 3]
        {
            //front
            3, 2, 1,
            3, 1, 0,

            // back
            4,5,6,
            4,6,7,

            // left
            11,10,9,
            11,9,8,

            // right
            12,13,14,
            12,14,15,

            // bottom
            19,18,17,
            19,17,16,

            // top
            20,21,22,
            20,22,23
        };
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        return mesh;
    }

    // divisions = number of divisions in the circle (there are no divisions in the height)
    public static Mesh Cylinder(int divisions, float radius, float height)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[divisions * 4 + 2]; // four rings (top, bottom, top-cap, bottom-cap) + 2 centers
        float dTheta = Mathf.PI * 2.0f / divisions;
        for (int i = 0; i < divisions; i++)
        {
            float theta = i * dTheta;
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            // top vertex
            vertices[i] = new Vector3(x, height, z);
            // bottom vertex
            vertices[i + divisions] = new Vector3(x, -height, z);
            // top-cap ring (duplicate of top ring)
            vertices[i + divisions * 2] = new Vector3(x, height, z);
            // bottom-cap ring (duplicate of bottom ring)
            vertices[i + divisions * 3] = new Vector3(x, -height, z);

        }
        // top and bottom center vertices
        vertices[divisions * 4] = new Vector3(0, +height, 0);
        vertices[divisions * 4 + 1] = new Vector3(0, -height, 0);

        mesh.vertices = vertices;

        // triangles: sides (2 per division) + top cap (divisions) + bottom cap (divisions)
        int[] tris = new int[divisions * 12];

        // side quads (two triangles per division)
        for (int i = 0; i < divisions; i++)
        {
            int baseSide = i * 6;
            tris[baseSide] = i;                                        // current top vertex
            tris[baseSide + 1] = (i + 1) % divisions;                  // next top vertex (wrapping)
            tris[baseSide + 2] = divisions + (i + 1) % divisions;      // next bottom vertex (wrapping)

            tris[baseSide + 3] = i;                                    // current top vertex
            tris[baseSide + 4] = divisions + (i + 1) % divisions;      // next bottom vertex (wrapping)
            tris[baseSide + 5] = divisions + i;                        // current bottom vertex
        }

        // top cap (triangle fan) - uses ring at divisions*2 and center at divisions*4
        int topStart = divisions * 6;
        for (int i = 0; i < divisions; i++)
        {
            int t = topStart + i * 3;
            tris[t] = divisions * 2 + i;
            tris[t + 1] = divisions * 2 + ((i + 1) % divisions);
            tris[t + 2] = divisions * 4; // top center
        }

        // bottom cap (triangle fan) - uses ring at divisions*3 and center at divisions*4 + 1
        int bottomStart = divisions * 9;
        for (int i = 0; i < divisions; i++)
        {
            int t = bottomStart + i * 3;
            tris[t] = divisions * 3 + ((i + 1) % divisions);
            tris[t + 1] = divisions * 3 + i;
            tris[t + 2] = divisions * 4 + 1; // bottom center
        }

        mesh.triangles = tris;

        mesh.RecalculateNormals();

        return mesh;
    }


    public static Mesh Sweep(Vector3[] profile, Matrix4x4[] path, bool closed)
    {
		Mesh mesh = new Mesh();

		int numVerts = path.Length * profile.Length;
		int numTris;

		if (closed)
			numTris = 2 * path.Length * profile.Length;
		else
			numTris = 2 * (path.Length-1) * profile.Length;


		Vector3[] vertices = new Vector3[numVerts];
		int[]tris = new int[numTris * 3];

		for (int i = 0; i < path.Length; i++)
		{
			for (int j = 0; j < profile.Length; j++)
			{
                Vector3 v = path[i].MultiplyPoint(profile[j]);
				vertices[i*profile.Length+j] = v;

				if (closed || i < path.Length - 1)
				{

					tris[6 * (i * profile.Length + j)] = (j + i * profile.Length);
					tris[6 * (i * profile.Length + j) + 1] = ((j + 1) % profile.Length + i * profile.Length);
					tris[6 * (i * profile.Length + j) + 2] = ((j + 1) % profile.Length + ((i + 1) % path.Length) * profile.Length);
					tris[6 * (i * profile.Length + j) + 3] = (j + i * profile.Length);
					tris[6 * (i * profile.Length + j) + 4] = ((j + 1) % profile.Length + ((i + 1) % path.Length) * profile.Length);
					tris[6 * (i * profile.Length + j) + 5] = (j + ((i + 1) % path.Length) * profile.Length);
				}
			}
		}

		mesh.vertices = vertices;

		mesh.triangles = tris;

		mesh.RecalculateNormals();

		return mesh;
	}

	public static Matrix4x4[] MakeCirclePath(float radius, int divisions)
	{
		Matrix4x4[] path = new Matrix4x4[divisions];
		for (int i = 0; i < divisions; i++)
		{
			float angle = (360.0f * i) / divisions;
			path[i] = Matrix4x4.Rotate(Quaternion.Euler(0, -angle, 0))* Matrix4x4.Translate(new Vector3(radius,0,0));
		}
		return path;
	}

	public static Vector3[] MakeCircleProfile(float radius, int divisions)
	{
		Vector3[] profile = new Vector3[divisions];
		for (int i = 0; i < divisions; i++)
		{
			float angle = (2.0f * Mathf.PI * i) / divisions;
			profile[i] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle),0);
		}
		return profile;
	}

}