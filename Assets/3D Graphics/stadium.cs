using UnityEngine;

public class stadium : MonoBehaviour
{
    GameObject stadiumBase;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stadiumBase = new GameObject();
        stadiumBase.name = "StadiumBase";

        MeshRenderer baseRenderer =
            stadiumBase.AddComponent<MeshRenderer>();

        // Apply public handle material
       

        MeshFilter Filter =
            stadiumBase.AddComponent<MeshFilter>();

        Filter.mesh =
            MeshUtilities.Cylinder(8, 10, 1);


        stadiumBase.transform.parent = transform;

        stadiumBase.transform.localPosition =
            new Vector3(0, 5, 0);

        stadiumBase.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

    }
}
