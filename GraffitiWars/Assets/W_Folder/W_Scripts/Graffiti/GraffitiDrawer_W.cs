using UnityEngine;
using UnityEngine.UI;

public class GraffitiDrawer_W : MonoBehaviour
{
    public RawImage drawingCanvas;
    private Texture2D drawingTexture;
    public int brushSize = 10;
    public GameObject drawingUI;

    void Start()
    {
        drawingTexture = new Texture2D(500, 500, TextureFormat.RGBA32, false);
        /*ClearCanvas();*/

        drawingCanvas.texture = drawingTexture;
    }

    void Update()
    {
        if (drawingUI.activeSelf && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            DrawAt(mousePos);
        }
    }

    void DrawAt(Vector2 position)
    {
        RectTransform rt = drawingCanvas.GetComponent<RectTransform>();
        Vector2 localPos;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, position, null, out localPos);

        localPos.x += rt.rect.width * 0.5f;
        localPos.y += rt.rect.height * 0.5f;

        int x = (int)((localPos.x / rt.rect.width) * drawingTexture.width);
        int y = (int)((localPos.y / rt.rect.height) * drawingTexture.height);

        if (x >= 0 && x < drawingTexture.width && y >= 0 && y < drawingTexture.height)
        {
            for (int i = -brushSize / 2; i < brushSize / 2; i++)
            {
                for (int j = -brushSize / 2; j < brushSize / 2; j++)
                {
                    int drawX = x + i;
                    int drawY = y + j;

                    if (drawX >= 0 && drawX < drawingTexture.width && drawY >= 0 && drawY < drawingTexture.height)
                    {
                        drawingTexture.SetPixel(drawX, drawY, Color.black);
                    }
                }
            }
            drawingTexture.Apply();
        }
    }

    public Texture2D GetDrawing()
    {
        return drawingTexture;
    }

    /*public void ClearCanvas()
    {
        Color[] clearColors = new Color[drawingTexture.width * drawingTexture.height];
        for (int i = 0; i < clearColors.Length; i++)
            clearColors[i] = Color.white; // Set to white

        drawingTexture.SetPixels(clearColors);
        drawingTexture.Apply();
    }*/
}
