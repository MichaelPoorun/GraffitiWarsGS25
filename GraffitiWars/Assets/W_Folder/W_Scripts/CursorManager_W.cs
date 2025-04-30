using UnityEngine;

public class CursorManager_W : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D clickCursor;
    public Vector2 hotspot = Vector2.zero;
    private bool isCursorHeld = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Draw") && !isCursorHeld)
        {
            Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
            isCursorHeld = true;
        }
        else if (Input.GetButtonUp("Draw") && isCursorHeld)
        {
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
            isCursorHeld = false;
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // reset to system default
        }
        else
        {
            Cursor.SetCursor(isCursorHeld ? clickCursor : defaultCursor, hotspot, CursorMode.Auto);
        }
    }
}
