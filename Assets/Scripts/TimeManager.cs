
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //public float slowdownFactor = 0.05f;
    private static float defaultFixedDeltaTime;

    private void Awake()
    {
        defaultFixedDeltaTime = 0.02f;
    }
    public void EnterBulletTime(float slowdownFactor)
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * defaultFixedDeltaTime;
    }
    
    public void ExitBulletTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
    }
}
