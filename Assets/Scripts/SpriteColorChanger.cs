using UnityEngine;

public class SpriteColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Texture2D texture = spriteRenderer.sprite.texture;

            Texture2D newTexture = new Texture2D(texture.width, texture.height);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixelColor = texture.GetPixel(x, y);
                    if (pixelColor.a > 0)  
                    {
                        newTexture.SetPixel(x, y, Color.white);
                    }
                    else
                    {
                        newTexture.SetPixel(x, y, pixelColor);
                    }
                }
            }

            newTexture.Apply();

            Sprite newSprite = Sprite.Create(newTexture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);
            //assigning t he new sprite
            spriteRenderer.sprite = newSprite;
        }
    }
}
