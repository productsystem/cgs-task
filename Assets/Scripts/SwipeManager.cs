using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public RectTransform levelPages;
    public void NextPage()
    {
        levelPages.anchoredPosition += new Vector2(-800f, 0);
    }

    public void PrevPage()
    {
        levelPages.anchoredPosition += new Vector2(800f, 0);
    }
}
