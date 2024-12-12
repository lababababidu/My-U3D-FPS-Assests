using UnityEngine;

public class DynamicSkyboxController : MonoBehaviour
{
    [Header("Skybox Settings")]
    public Material skyboxMaterial;
    public Transform sunTransform;

    [Header("Day Cycle Settings")]
    public float dayCycleDuration = 120f; // Day duration in seconds

    private float timeOfDay = 0f;

    void Start()
    {
        if (skyboxMaterial == null)
        {
            Debug.LogError("Skybox Material not assigned.");
            return;
        }

        if (sunTransform == null)
        {
            Debug.LogError("Sun Transform not assigned.");
            return;
        }
    }

    void Update()
    {
        // Calculate the current time of day
        timeOfDay += Time.deltaTime / dayCycleDuration;
        if (timeOfDay >= 1f)
        {
            timeOfDay -= 1f;
        }

        // Calculate sun rotation angle (rotate around X axis)
        float sunAngle = timeOfDay * 360f - 90f; // -90 to start at horizon
        sunTransform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);

        // Update the sun direction in the shader
        Vector3 sunDirection = sunTransform.forward;
        // skyboxMaterial.SetVector("_WorldSpaceLightPos0", new Vector4(sunDirection.x, sunDirection.y, sunDirection.z, 0));
    }
}
