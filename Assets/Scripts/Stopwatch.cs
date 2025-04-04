using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    TMP_Text textTime;

    void Start()
    {
        textTime = GetComponent<TMP_Text>();
    }

    void Update()
    {
        float t = Time.timeSinceLevelLoad;
        textTime.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(t/60), Mathf.FloorToInt(t%60));
    }
}
