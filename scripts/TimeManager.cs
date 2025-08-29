using UnityEngine;
using System;
public class TimeManager : MonoBehaviour
{
    //tipo action, lanzar eventos a nuestro juego cuando nosotros indiquemos, 
    //y y del lado de otros objetos recibirlos.
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    //medio segundo en tiempo real para que pase 1 minuto en el juego
    private float minuteToRealTime = 0.5f;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Minute = 0;
        Hour = 10;
        timer = minuteToRealTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Minute++;

            OnMinuteChanged?.Invoke();

            if(Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
            }

            timer = minuteToRealTime;
        }
            
        }
}

