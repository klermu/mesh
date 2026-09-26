using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Cube : MonoBehaviour
{
    GameObject handle;
    public Material handleMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handle = new GameObject();
        handle.name = "Handle";

        MeshRenderer handleRenderer =
            handle.AddComponent<MeshRenderer>();

        // Apply public handle material
        handleRenderer.sharedMaterial = handleMaterial;

        MeshFilter handleFilter =
            handle.AddComponent<MeshFilter>();

        handleFilter.mesh =
            MeshUtilities.Cylinder(4, 10f, 0.1f);
       

        handle.transform.parent = transform;

        handle.transform.localPosition =
            new Vector3(-15, 5, 0);

        handle.transform.localRotation = Quaternion.Euler(new Vector3(45f, 0f, -90f));

    }
  
    
}
