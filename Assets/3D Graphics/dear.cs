using UnityEngine;

public class dear : MonoBehaviour
{
    GameObject dear_head;
    GameObject dear_neck;
    GameObject dear_body;

    GameObject dear_tail;
    GameObject dear_leg_part1;
    GameObject dear_leg_part2;

    public Material dear_material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        CreateDeerNeck();
        CreateDeerBody();
        CreateDeerHead();

    }

    public void CreateDeerBody()
    {

        dear_body = new GameObject("Deer Body");
        MeshFilter filter =
            dear_body.AddComponent<MeshFilter>();
        MeshRenderer renderer =
            dear_body.AddComponent<MeshRenderer>();
        filter.mesh =
            MeshUtilities.Sweep(
                GetbodyProfile,
                dearPath(), true
            );


        renderer.sharedMaterial = dear_material;

        dear_body.transform.parent = transform;
        dear_body.transform.localPosition =
            new Vector3(0, 0.005f, 0);
        dear_body.transform.localRotation =
            Quaternion.identity;
    }



    public void CreateDeerHead()
    {
        dear_head = new GameObject("Deer Head");
        MeshFilter filter =
            dear_head.AddComponent<MeshFilter>();
        MeshRenderer renderer =
            dear_head.AddComponent<MeshRenderer>();
        filter.mesh =
            MeshUtilities.Sweep(
                GetHeadProfile,
                dearPath(), true
            );

        renderer.sharedMaterial = dear_material;
        dear_head.transform.parent = transform;
        dear_head.transform.localPosition =
            new Vector3(-1.833f, 1f, 0);
        dear_head.transform.localRotation =
            Quaternion.identity;
    }

    public void CreateDeerNeck()
    {
        dear_neck = new GameObject("Deer Neck");
        MeshFilter filter =
            dear_neck.AddComponent<MeshFilter>();
        MeshRenderer renderer =
            dear_neck.AddComponent<MeshRenderer>();
        filter.mesh =
            MeshUtilities.Sweep(
                GetNeckProfile,
                dearPath(), true
            );
        renderer.sharedMaterial = dear_material;
        dear_neck.transform.parent = transform;
        dear_neck.transform.localPosition =
            new Vector3(-1.585f, 0.565f, 0);
        dear_neck.transform.localRotation =
            Quaternion.identity;
    }


    Vector3[] GetbodyProfile = new Vector3[]
    {

           new Vector3(0f, 0.55f, 0f),
            new Vector3(-0.653f, 0.607f, 0f),
            new Vector3(-1.3f, 0.6f, 0f),
            new Vector3(-1.645f, 0.425f, 0f),
            new Vector3(-1.636f, 0.04f, 0f),
            new Vector3(-1.333f, -0.277f, 0f),
            new Vector3(-0.836f, -0.36f, 0f),
            new Vector3(-0.4f, -0.4f, 0f),
            new Vector3(0.253f, -0.45f, 0f),
            new Vector3(0.73f, -0.3f, 0f),
            new Vector3(1.3f, 0.2f, 0f),
            new Vector3(0.895f, 0.25f, 0f),
            new Vector3(0.545f, 0.45f, 0f)



    };

    Vector3[] GetNeckProfile = new Vector3[]
    {


          new Vector3(0.415f, -0.02f, 0f),
            new Vector3(0.36f, 0.21f, 0f),
            new Vector3(0.216f, 0.675f, 0f),
            new Vector3(-0.01f, 1.056f, 0f),
            new Vector3(-0.404f, 0.974f, 0f),
            new Vector3(-0.244f, 0.45f, 0f),
            new Vector3(0.03f, -0.26f, 0f)





    };

    Vector3[] GetHeadProfile = new Vector3[]

    {

            new Vector3(0f, 0f, 0f),
            new Vector3(0.352f, 0.282f, 0f),
            new Vector3(0.183f, 0.62f, 0f),
            new Vector3(-0.26f, 0.52f, 0f),
            new Vector3(-0.68f, 0.26f, 0f),
            new Vector3(-0.48f, 0.023f, 0f)



    };





    Matrix4x4[] dearPath()
    {
        Matrix4x4[] armPath =
            new Matrix4x4[10];

        // Start cap
        armPath[0] =
            Matrix4x4.Scale(
                new Vector3(0, 0, 35)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f)
            );


        // Start bevel
        armPath[1] =
            Matrix4x4.Scale(
                new Vector3(2, 2, 75)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f)
            );


        // Duplicate for sharp bevel
        armPath[2] =
            Matrix4x4.Scale(
                new Vector3(2, 2, 75)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0f)
            );


        // Start of straight section
        armPath[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.0075f)
            );


        // End of straight section
        armPath[4] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0075f)
            );


        // End bevel
        armPath[5] =
            Matrix4x4.Scale(
                new Vector3(2, 2, 75)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );


        // Duplicate for sharp bevel
        armPath[6] =
            Matrix4x4.Scale(
                new Vector3(2, 2, 75)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );


        // Cap
        armPath[7] =
            Matrix4x4.Scale(
                new Vector3(0, 0, 35)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );


        // Extra transforms to keep the sweep stable
        armPath[8] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );

        armPath[9] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );

        return armPath;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
