using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    public GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    public int selectedPanID;
    public int CurrentPanID;
    private bool isScrolling;
    public bool SnapStarted;
    
    public void StartSnap(int curElem = 0)
    {
        contentRect = GetComponent<RectTransform>();
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        for (int i = 0; i < panCount; i++)
        {
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;
        }

        if (curElem > 0)
        {
            contentVector.x = pansPos[curElem - 1].x;
            contentRect.anchoredPosition = contentVector;           
        }

        CurrentPanID = curElem - 1;

        SnapStarted = true;
    }

    private void Start()
    {
        StartSnap();
    }

    private void FixedUpdate()
    {
        if (!SnapStarted) { return; };

        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;                
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }

        if (CurrentPanID != selectedPanID)
        {
            CurrentPanID = selectedPanID;
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 500 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 500) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;

    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}