using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class slug : MonoBehaviour
{
    
    GameObject body;
    GameObject head;
    public Material BodyMaterial;

    public Material HeadMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Vector3[] bodyProfile = MeshUtilities.MakeCircleProfile(0.5f, 16);
        Matrix4x4[] bodyPath = MeshUtilities.MakeCirclePath(0f, 16);

        Vector3[] headProfile = MeshUtilities.MakeCircleProfile(1.5f, 16);
        Matrix4x4[] headPath = MeshUtilities.MakeCirclePath(0f, 16);

        // =========================================================
        // bottom of thwe slug
        // =========================================================
        body = new GameObject();
        body.name = "Body";

        MeshRenderer bodyRenderer =
            body.AddComponent<MeshRenderer>();

        // Apply public bottom material
        bodyRenderer.sharedMaterial = BodyMaterial;

        MeshFilter bodyFilter =
            body.AddComponent<MeshFilter>();

        // use shared MeshUtilities helpers
        

        bodyFilter.mesh =
            MeshUtilities.Sweep(
                bodyProfile,
                bodyPath,
                true);

        body.transform.localPosition =
          new Vector3(2, 5, 0);

        body.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));



        // =========================================================
        // head of thwe slug
        // =========================================================\


        head = new GameObject();
        head.name = "Head";

        MeshRenderer headRenderer =
            head.AddComponent<MeshRenderer>();

        // Apply public head material
        headRenderer.sharedMaterial = HeadMaterial;

        MeshFilter headFilter =
            head.AddComponent<MeshFilter>();

        // use shared MeshUtilities helpers


        headFilter.mesh =
            MeshUtilities.Sweep(
                headProfile,
                headPath,
                true);

        head.transform.parent = transform;

        head.transform.localPosition =
            new Vector3(2f, 5, 0.2f);

        head.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
