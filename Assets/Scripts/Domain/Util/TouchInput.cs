using UnityEngine;


public class TouchInput
{
    /// <summary>
    /// Gets the touched position.
    /// upper left conner = (0,0).
    /// lower right conner = (Const.ScreenSize.width, Const.ScreenSize.height).
    /// </summary>
    /// <value>The position.</value>
    public static Vector2 position
    {
        get
        {
            Vector2 touchPos = Input.mousePosition;
            float targetRatio = (float)Const.ScreenSize.width / (float)Const.ScreenSize.height;
            float currentRatio = Screen.width / Screen.height;
            float marginX = 0;
            float marginY = 0;
            float ratio;
            if (currentRatio > targetRatio)
            {
                float idealWidth = Screen.height * targetRatio;
                marginX = (Screen.width - idealWidth) / 2;
                ratio = Screen.height / (float)Const.ScreenSize.height;
            }
            else
            {
                float idealHeight = Screen.width / targetRatio;
                marginY = (Screen.height - idealHeight) / 2;
                ratio = Screen.width / (float)Const.ScreenSize.width;
            }
            touchPos = touchPos - new Vector2(marginX, marginY);
            touchPos = touchPos / ratio;
            return new Vector2(touchPos.x, Const.ScreenSize.height - touchPos.y);
        }
    }
}
