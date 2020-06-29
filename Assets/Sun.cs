using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float timeScale = 5f;
    public Gradient backgroundGradient;
    public Color midnightColor;
    public Color sunriseDawnColor;
    public Color noonColor;
    public Light sun;
    public float sunIntensity = 1f;
    public Light moon;
    public float moonIntensity = 0.25f;

    public AnimationCurve sunCurve;
    public AnimationCurve moonCurve;

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * -timeScale * Mathf.PI, 0f, 0f);

        float x = transform.localEulerAngles.x;
        float dayRatio = Mathf.Sin(Mathf.PI * x / 180f);
        camera.backgroundColor = backgroundGradient.Evaluate(dayRatio);

        dayRatio = dayRatio > 0f ? dayRatio : 0f;
        float nightRatio = Mathf.Sin(Mathf.PI * (x + 180f) / 180f);
        nightRatio = nightRatio > 0f ? nightRatio : 0f;

        sun.color = Color.Lerp(sunriseDawnColor, noonColor, dayRatio);
        moon.color = Color.Lerp(sunriseDawnColor, midnightColor, nightRatio);

        sun.intensity = sunIntensity * dayRatio;
        moon.intensity = moonIntensity * nightRatio;

        //moon.intensity = moonIntensity * (Mathf.Sin(moonCurve.Evaluate(((transform.eulerAngles.y -180f) / 180f)) * Mathf.PI) + 0.5f);

    }
}
