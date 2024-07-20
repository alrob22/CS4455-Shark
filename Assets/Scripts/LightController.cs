using UnityEngine;

public class LightController : MonoBehaviour
{
    public string lightGameObjectName = "Directional Light"; 
    public float dimmingDuration = 360.0f;

    private Light directionalLight; 
    private float startTime;

    public Color startColor = Color.yellow; 
    public Color endColor = Color.black; 

    private void Start() {
        initializeLight();
    }
    private void initializeLight() {
        GameObject lightObject = GameObject.Find(lightGameObjectName);
        if (lightObject != null)
        {
            directionalLight = lightObject.GetComponent<Light>();
            if (directionalLight == null)
            {
                Debug.LogError("No Light component found on the GameObject named " + lightGameObjectName);
            }
            else {
                ResetLight();
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the name " + lightGameObjectName);
        }
    }

    private void Update() {
        if (directionalLight != null) {
            float elapsedTime = Time.time - startTime;
            float lerpFactor = Mathf.Clamp01(elapsedTime / dimmingDuration);
            //float lerpFactor = Mathf.Clamp01(Time.time / dimmingDuration);
            directionalLight.intensity = Mathf.Lerp(1.0f, 0.0f, lerpFactor);
            directionalLight.color = Color.Lerp(startColor, endColor, lerpFactor);
        }
    }

    public void ResetLight() {
        if (directionalLight != null) {
            startTime = Time.time; // Reset the start time
            directionalLight.color = startColor;
            directionalLight.intensity = 1.0f; 
        }
    }
}
