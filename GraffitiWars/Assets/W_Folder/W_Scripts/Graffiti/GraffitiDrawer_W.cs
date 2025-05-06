using UnityEngine;
using UnityEngine.UI;

public class GraffitiDrawer_W : MonoBehaviour
{
    public RawImage drawingCanvas;
    private Texture2D drawingTexture;
    public int brushSize = 10;
    public GameObject drawingUI;

    private Vector2 virtualCursorPos;
    public float controllerSpeed = 2000f;
    public RectTransform virtualCursor;

    void Start()
    {
        CanvasRenderer virtualCursorRenderer = virtualCursor.GetComponent<CanvasRenderer>();
        virtualCursorRenderer.SetAlpha(0f);  // Hide the virtual cursor

        drawingTexture = new Texture2D(500, 500, TextureFormat.RGBA32, false);
        /*ClearCanvas();*/
        drawingCanvas.texture = drawingTexture;

        virtualCursorPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        CanvasRenderer virtualCursorRenderer = virtualCursor.GetComponent<CanvasRenderer>();
        virtualCursor.position = virtualCursorPos;

        //if (drawingUI.activeSelf && Input.GetButton("Draw"))
        //{
        //    Vector2 mousePos = Input.mousePosition;
        //    DrawAt(mousePos);
        //}

        if (!drawingUI.activeSelf) return;

        float moveX = Input.GetAxis("Horizontal_LeftStick");
        float moveY = Input.GetAxis("Vertical_LeftStick");
        Debug.Log($"Stick X: {moveX}, Y: {moveY}");

        virtualCursorPos += new Vector2(moveX, moveY) * controllerSpeed * Time.deltaTime;
        virtualCursorPos.x = Mathf.Clamp(virtualCursorPos.x, 0, Screen.width);
        virtualCursorPos.y = Mathf.Clamp(virtualCursorPos.y, 0, Screen.height);

        /*if (Input.GetButton("Draw"))
        {
            Vector2 drawPos = Input.GetMouseButton(0) ? (Vector2)Input.mousePosition : virtualCursorPos;
        }*/

        bool usingMouse = Input.GetMouseButton(0);
        bool usingController = Input.GetButton("Draw");
        
        if (usingMouse)
        {
            DrawAt(Input.mousePosition);
            virtualCursorRenderer.SetAlpha(0f);
        }
        else if (usingController)
        {
            DrawAt(virtualCursorPos);
            virtualCursorRenderer.SetAlpha(1f);
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
