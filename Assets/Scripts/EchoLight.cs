using System.Collections;
using System.Collections.Generic;
using SO.Echos;
using Unity.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Events;

namespace Utility{
  public class EchoLight : MonoBehaviour
  {
      [SerializeField] private UnityEvent onSound;
      [SerializeField] private Light lightComponent;
      [SerializeField] private float speed = 200f;
      [SerializeField] private Coroutine lightCor;
      [SerializeField] private AnimationCurve speedupFunction;
      [SerializeField] private GameObject lightTrigger;
      [SerializeField] private float inputIntensity;
      [SerializeField] private bool emitTrigger = true;
      [SerializeField] private bool enlightOnlySeenByPlayer = true;
      
      private static Dictionary<Vector2, Texture2D> bakedTextures = new();

      public float Range {
        get => lightComponent.range;
        set => lightComponent.range = value * 2.5f;
      }
      [SerializeField] private float lineWidth = 0.9f;

      private void Awake()
      {
        lightComponent.spotAngle = 0f;
        lightComponent.innerSpotAngle = 0f;
        lightComponent.enabled = true;
        BakeCookie();
      }

      public void SetRange(float range){
        lightComponent.range = range * 2.5f;
      }

      public void SetColor(Color color){
        lightComponent.color = color;
      }

      public void SetSpeed(float speed){
        this.speed = speed;
      }

      public void SetLineWidth(float width){
        lineWidth = width;
        BakeCookie();
      }

      public void SetCookie(Texture texture){
        lightComponent.cookie = texture;
      }

      public void BakeCookie(){
        lightComponent.cookie = GenerateCookie(1f, 1f-lineWidth);
      }

      public void SetLight(Echo echo){
        SetRange(echo.range);
        SetColor(echo.color);
        SetSpeed(echo.speed);
        SetLineWidth(echo.width);
        emitTrigger = echo.shouldCastTrigger;
        inputIntensity = echo.intensity;
        enlightOnlySeenByPlayer = echo.enlightIgnoresWalls;
      }
      
      public void CastLight(){
        if(lightCor != null) StopCoroutine(lightCor);
        lightCor = emitTrigger ? StartCoroutine(CastLightCoroutineWithTrigger()) : StartCoroutine(CastLightCoroutine());
        onSound.Invoke();
      }

      private IEnumerator CastLightCoroutineWithTrigger(){
        lightComponent.enabled = true;
        lightComponent.spotAngle = 0f;
        lightComponent.innerSpotAngle = 0f;

        GameObject tr = Instantiate(lightTrigger);
        tr.transform.position = transform.position - Vector3.up;
        tr.transform.SetParent(transform);
        EchoLightTrigger echoTrigger = tr.GetComponent<EchoLightTrigger>();
        echoTrigger.ShouldEnlightIgnoreWalls(enlightOnlySeenByPlayer);

        while(lightComponent.spotAngle < 155){
          yield return null;
          lightComponent.spotAngle += speed * speedupFunction.Evaluate(lightComponent.spotAngle/160) * Time.deltaTime;
          lightComponent.innerSpotAngle = lightComponent.spotAngle;
          lightComponent.intensity = inputIntensity * Mathf.Min(1 , (158 - lightComponent.spotAngle)/60);
          echoTrigger.AddRadius(speed / 360f * lightComponent.range * 
          speedupFunction.Evaluate(lightComponent.spotAngle/160) * Time.deltaTime / 1.5f);
        }
        lightComponent.enabled = false;
        lightCor = null;
        Destroy(gameObject);
      }
      
      private IEnumerator CastLightCoroutine(){
        lightComponent.enabled = true;
        lightComponent.spotAngle = 0f;
        lightComponent.innerSpotAngle = 0f;
        while(lightComponent.spotAngle < 155){
          yield return null;
          lightComponent.spotAngle += speed * speedupFunction.Evaluate(lightComponent.spotAngle/160) * Time.deltaTime;
          lightComponent.innerSpotAngle = lightComponent.spotAngle;
          lightComponent.intensity = inputIntensity * Mathf.Min(1 , (158 - lightComponent.spotAngle)/60);
        }
        lightComponent.enabled = false;
        lightCor = null;
        Destroy(gameObject);
      }
      
      private Texture2D GenerateCookie(float outerRadius, float innerRadius) {
        var textureKey = new Vector2(outerRadius, innerRadius);
        if(bakedTextures.TryGetValue(textureKey, out var cookie)) return cookie;
        
        innerRadius = innerRadius * 256;
        outerRadius = outerRadius * 256;
        int size = 512;
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);

        // Center of the texture
        Vector2 center = new Vector2(size / 2f, size / 2f);

        // Loop through each pixel
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                // Calculate the distance from the center
                float distance = Vector2.Distance(center, new Vector2(x, y));
                // Determine pixel color based on distance
                if (distance <= innerRadius)
                {
                    // Inside the black hole
                    texture.SetPixel(x, y, Color.black);
                }
                else if (distance <= outerRadius)
                {
                    // Inside the white circle
                    //texture.SetPixel(x, y, Color.white);
                    float gradient = Mathf.InverseLerp(outerRadius, innerRadius, distance);
                    texture.SetPixel(x,y,Color.Lerp(Color.white, Color.black, gradient));
                }
                else
                {
                    // Outside the circle
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }

        // Apply changes to the texture
        texture.Apply();
        bakedTextures.Add(textureKey, texture);
        return texture;
    }
  }
}
