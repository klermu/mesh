using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Character : MonoBehaviour
{
   
    GameObject arm;
    GameObject dress;
    public Material skin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Vector3[] bodyProfile = MeshUtilities.MakeCircleProfile(0.5f, 16);
        Matrix4x4[] bodyPath = MeshUtilities.MakeCirclePath(0f, 16);

        

        Vector3[] miniArmProfile = new Vector3[]
     {
            new Vector3(-0.2f, 0f, 0f),
            new Vector3(0.2f, 0f, 0f),
            new Vector3(0.16f, 1f, 0f),
            new Vector3(-0.16f, 1f, 0f),

     };

        Vector3[] dressProfile = new Vector3[]
     {
            new Vector3(-0.146f, 3.985f, 0f),
            new Vector3(-0.4f, 3.6f, 0f),
            new Vector3(-0.45f, 3.26f, 0f),
            new Vector3(-0.25f, 2.97f, 0f),
            new Vector3(-1.74f, 0.06f, 0f),

            new Vector3(0.146f, 3.985f, 0f),
            new Vector3(0.4f, 3.6f, 0f),
            new Vector3(0.45f, 3.26f, 0f),
            new Vector3(0.25f, 2.97f, 0f),
            new Vector3(1.74f, 0.06f, 0f)

     };

        //===================================================================================================================
        //mini arm path
        Matrix4x4[] armPath = new Matrix4x4[6];

        armPath[0] =
                Matrix4x4.Scale(new Vector3(0, 0, 15)) *
                Matrix4x4.Translate(
                    new Vector3(0, 0, -0.01f));

        armPath[1] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f));

        armPath[2] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.0075f));

        armPath[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0075f));

        armPath[4] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));

        armPath[5] =
            Matrix4x4.Scale(new Vector3(0, 0, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));


        //===================================================================================================================
        // dress path
        Matrix4x4[] dressPath = new Matrix4x4[6];

        dressPath[0] =
                Matrix4x4.Scale(new Vector3(0, 0, 35)) *
                Matrix4x4.Translate(
                    new Vector3(0, 0, -0.01f));

        dressPath[1] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 35)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f));

        dressPath[2] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.0075f));

        dressPath[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0075f));

        dressPath[4] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 35)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));

        dressPath[5] =
            Matrix4x4.Scale(new Vector3(0, 0, 35)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));
        //===================================================================================================================
        //mini arm

        arm = new GameObject();
        arm.name = "Arm";

        MeshRenderer armRenderer =
            arm.AddComponent<MeshRenderer>();

        // Apply public arm material
        armRenderer.sharedMaterial = skin;

        MeshFilter armFilter =
            arm.AddComponent<MeshFilter>();

        armFilter.mesh =
            MeshUtilities.Sweep(
                miniArmProfile,
                armPath,
                false);

        arm.transform.parent = transform;

        //===================================================================================================================

        dress = new GameObject();
        dress.name = "Dress";

        MeshRenderer dressRenderer =
            dress.AddComponent<MeshRenderer>();

        // Apply public dress material
        dressRenderer.sharedMaterial = skin;

        MeshFilter dressFilter =
            dress.AddComponent<MeshFilter>();

        dressFilter.mesh =
            MeshUtilities.Sweep(
                dressProfile,
                dressPath,
                false);

        dress.transform.parent = transform;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
