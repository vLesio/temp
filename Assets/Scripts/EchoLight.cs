using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;

namespace Utility{
  public class EchoLight : MonoBehaviour
  {
      [SerializeField] private Light lightComponent;
      private float speed = 200f;
      private Coroutine lightCor;
      private float lineWidth = 0.9f;

      private void Awake()
      {
        lightComponent.spotAngle = 0f;
        lightComponent.innerSpotAngle = 0f;
        lightComponent.enabled = true;
        BakeCookie();
      }

      public void SetRange(float range){
        lightComponent.range = range;
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

      public void SetLight(float range, float speed, Color color){
        SetRange(range);
        SetColor(color);
        SetSpeed(speed);
      }

      private IEnumerator Start()
      {
          while(true){
            yield return new WaitForSeconds(2.5f);
            castLight();
          }
      }
      
      private void castLight(){
        if(lightCor != null) StopCoroutine(lightCor);
        lightCor = StartCoroutine(castLightCoroutine());
      }

      private IEnumerator castLightCoroutine(){
        lightComponent.enabled = true;
        lightComponent.spotAngle = 0f;
        lightComponent.innerSpotAngle = 0f;
        while(lightComponent.spotAngle < 179){
          yield return null;
          lightComponent.spotAngle += speed * Time.deltaTime;
          lightComponent.innerSpotAngle = lightComponent.spotAngle;
        }
        lightComponent.enabled = false;
        lightCor = null;
      }

      private Texture2D GenerateCookie(float outerRadius, float innerRadius)
    {
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
        return texture;
    }
  }
}