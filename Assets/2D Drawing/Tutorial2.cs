using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Tutorial2 : MonoBehaviour
{
    Texture2D texture;
    Color[] background;

    // Awake is called when the game object is initialised
    // This is perhaps not the best way to setup a full-screen quad but it is sufficient for our purposes
    private void Awake()
    {
        // set up the camera to look perfectly at the quad (which we are about to make)
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 360;
        Camera.main.rect = new Rect(0, 0, 1, 1);

        // create and postion a quad
        GameObject surface = GameObject.CreatePrimitive(PrimitiveType.Quad);
        surface.transform.localPosition = new Vector3(0, 0, 0);
        surface.transform.localRotation = Quaternion.identity;
        surface.transform.localScale = new Vector3(1280, 720, 1);

        // create a texture to display on the quad (that we can also draw into directly)
        texture = new Texture2D(1280, 720);
        texture.filterMode = FilterMode.Point;
        surface.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
        surface.GetComponent<Renderer>().material.mainTexture = texture;
        background = texture.GetPixels();
    }

    // Start is called before the first frame update
    void Start()
    {
        // nothing to do here
    }

    // Update is called once per frame
    void Update()
    {
        // this needs to happen _before_ your code
        texture.SetPixels(background); // DO NOT MOVE OR DELETE

        // *******************************************************
        // Write your Tutorial2 code here
        // *******************************************************

        // this needs to happen _after_ your code
        texture.Apply(); // DO NOT MOVE OR DELETE
    }
}

