using System;
using System.Collections;
using Units;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Timer timerObject;
	public void Setup(float _duration){
		duration = _duration;
		passed = 0;
		left = duration;
		timer = TimerCoroutine();
	}

    public void Setup(int count, float _duration)
    {
        duration = _duration;
        passed = 0;
        left = duration;
        repeatCount = count;
        timer = RepeatableTimerCoroutine();
    }

    private IEnumerator timer;
	public float duration;
	public float passed;
	public float left;
    public int repeatCount;


	public event Action<float> Tick;
	public event Action OnStart;
	public event Action OnStop;
	public event Action OnComplete;
    public event Action OnEndLap;

	private bool isTimerRunning = false;

	public static Timer CreateTimer(float duration){
		GameObject timerObject = new GameObject("Timer Object");
		Timer timer = timerObject.AddComponent<Timer>();
		timer.Setup(duration);
		return timer;
	}
	
	public Timer CreateRepeatablTimer(int count, float delay)
    {
        GameObject timerObject = new GameObject("Timer Object");
        Timer timer = timerObject.AddComponent<Timer>();
        timer.Setup(count, delay);
        return timer;
    }

    public void AddToQueue(int count)
    {
        repeatCount += count - repeatCount;
    }

	public void StartTimer(){
		if (!isTimerRunning){
			isTimerRunning = true;
			OnStart?.Invoke();
			StartCoroutine(timer);
		}
		else throw new Exception();
	}

    public void Stop()
    {
        StopCoroutine(timer);
        isTimerRunning = false;
        OnStop?.Invoke();
    }

    public void DestroyTimer()
    {
        Destroy(gameObject);
    }

	private IEnumerator TimerCoroutine() 
	{
		while (passed < duration){
			passed += 1;
			Tick?.Invoke(passed);
			left -= 1;
			yield return new WaitForSeconds(1f);
		}

		OnComplete?.Invoke();
        isTimerRunning = false;
    }

    private IEnumerator RepeatableTimerCoroutine() 
	{
		while (repeatCount > 0)
        {
	        while (passed < duration)
            {
                passed += 1;
                Tick?.Invoke(passed);
                left -= 1;
                yield return new WaitForSeconds(1f);
            }
            repeatCount--;
            OnEndLap?.Invoke();
            ResetTimer();

        }
        OnComplete?.Invoke();
        isTimerRunning = false;
        DestroyTimer();
    }

    public void ResetTimer()
    {
        passed = 0;
        left = duration;
    }
}