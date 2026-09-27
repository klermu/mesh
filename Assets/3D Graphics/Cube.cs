using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class cube : MonoBehaviour
{
    GameObject Enemy;
    public Material handleMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy = new GameObject();
        Enemy.name = "Enemy";

        MeshRenderer enemyRenderer =
            Enemy.AddComponent<MeshRenderer>();

        // Apply public handle material
        enemyRenderer.sharedMaterial = handleMaterial;

        MeshFilter enemyFilter =
            Enemy.AddComponent<MeshFilter>();

        enemyFilter.mesh =
            MeshUtilities.Cube(0.4f);
       

        Enemy.transform.parent = transform;

        Enemy.transform.localPosition =
            new Vector3(0, 5, 0);

        Enemy.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

    }
  
    
}
