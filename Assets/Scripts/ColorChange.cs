using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

public class ColorChange : MonoBehaviour
{
    public Color DefaultColor = Color.magenta;
    public Color HighlightColor = Color.blue;
    public float duration = 1.0f;

    private Skybox skybox;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) renderer = GetComponent<Renderer>();

        RenderSettings.skybox.SetColor("_Tint", DefaultColor);
    }

    private void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(DefaultColor, HighlightColor, lerp));
    }
    
}
