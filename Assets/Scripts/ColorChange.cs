using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Color DefaultColor = Color.magenta;
    public Color HighlightColor = Color.blue;
    public Color ambLightStart = Color.white;
    public Color ambEnd = Color.black;
    public float duration = 360.0f;
    public float dimmingDuration = 360.0f; 
    private Material skyboxMaterial;

    private void Start() {
        skyboxMaterial = RenderSettings.skybox;
        if (skyboxMaterial != null) {
            skyboxMaterial.SetColor("_Tint", DefaultColor);
        }
    }

    private void Update() {
        float colorLerp = Mathf.PingPong(Time.time / duration, 1f);
        float dimmingLerp = Mathf.Clamp01(Time.time / dimmingDuration);
        Color skyboxColor = Color.Lerp(DefaultColor, HighlightColor, colorLerp);
        skyboxColor *= 1 - dimmingLerp;
        if (skyboxMaterial != null) {
            skyboxMaterial.SetColor("_Tint", skyboxColor);
        }
    }
}
