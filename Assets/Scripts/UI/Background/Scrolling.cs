using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float scrollSpeed;
    public bool startScroll;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (startScroll)
        {
            Debug.Log("Scrolling");
            float y = Time.deltaTime * -scrollSpeed;
            Vector2 currOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
            Vector2 newOffset = currOffset + new Vector2(0, y);
            rend.sharedMaterial.SetTextureOffset("_MainTex", newOffset);

        }
    }
}
