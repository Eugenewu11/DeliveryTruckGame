using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
public class Driver : MonoBehaviour
{
    //Variables

    [SerializeField] float steerSpeed = 20; //Velocidad de direccion
    public float currentSpeed = 20; //Velocidad de movimiento
    [SerializeField] float boostSpeed = 45; //Velocidad del boost
    public float regularSpeed = 20;
    [SerializeField] TMP_Text boostText;
    public bool isBoosting = false;

    float delay = 0.3f; //delay de destruccion del booster
    
    void Start()
    {
        //Aqui se ejecutara una accion inmediatamente despues de darle play
        boostText.gameObject.SetActive(false);
    }
    

    void Update()
    {
        float steer = 0; //Girar
        float move = 0; //Moverse

        /*
        move = + -----> Adelante
        move = - -----> Atras

        Steer = + -----> Derecha
        Steer = - -----> Izquierda
        */

        //Para tecla W (adelante)
        if (Keyboard.current.wKey.isPressed )
        {
            move = 0.2f;
        }

        //Para tecla S (atras)
        else if (Keyboard.current.sKey.isPressed)
        {
            move = -0.2f;

        }

        //Para tecla A (izquierda), gira sobre su propio eje
        if (Keyboard.current.aKey.isPressed)
        {
            move = 0f;
            steer = 40f;
        }

        //Para tecla D (derecha), gira sobre su propio eje
        else if (Keyboard.current.dKey.isPressed)
        {
            move = 0f;
            steer = -40;
        }

        //Avanzar hacia adelante mientras gira hacia la izquierda (D)
        if (Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            move = 0.2f;
            steer = 20;
        }

        //Avanzar hacia adelante mientras gira hacia la derecha (A)
        if (Keyboard.current.wKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            move = 0.2f;
            steer = -20;
        }

        //Ir hacia atras mientras gira hacia la izquierda (A)
        if (Keyboard.current.sKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            move = -0.2f;
            steer = -20f;
        }

        //Ir hacia atras mientras gira hacia la derecha (D)
        if (Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            move = -0.2f;
            steer = 20;
        }

        //Shift para acelerar
        if(Keyboard.current.shiftKey.isPressed)
        {
            move = 0.4f;
        }

        //Hacer que sea independiente a los FPS con Time.deltaTime
        float cuantoGira = steer * steerSpeed * Time.deltaTime; 
        float cuantoMov = currentSpeed * move * Time.deltaTime;

        //Acceso a la propiedad transform.Rotate(x,yz), que me van a permitir rotar un componente
        transform.Rotate(0, 0, cuantoGira);

        //Acceso a la propiedad transform.Translate(x,y,z), para desplazar algo sobre la escena.
        transform.Translate(0, cuantoMov, 0);
    }

    //Cuando pase sobre un booster va a aumentar su velocidad y se va a destruir el objeto
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si está en boost, ignora cambios de terreno
        if (isBoosting && collision.CompareTag("Road") || 
            collision.CompareTag("Grass") || 
            collision.CompareTag("Ground"))
        {
            return;
        }

        switch (collision.tag)
        {
            case "Booster":
                Destroy(collision.gameObject, delay);
                StartCoroutine(boostDuration(6f));
                StartCoroutine(boostTextDuration(3f));
                break;

            case "Grass":
                currentSpeed = 17;
                break;

            case "Ground":
                currentSpeed = 12;
                break;

            case "Road":
                currentSpeed = 30;
                break;

            case "Customer":
                currentSpeed = regularSpeed;
                break;

            default:
                currentSpeed = regularSpeed;
                break;
        }
    }
    //Cuando se estrelle en algo va a reducir su velocidad
    void OnCollisionEnter2D(Collision2D collision)
    {
        currentSpeed = regularSpeed;
        Debug.Log($"Chocaste {currentSpeed}");
    }


    //Corrutina
    IEnumerator boostDuration(float time)
    {
        isBoosting = true;
        currentSpeed = boostSpeed;
        yield return new WaitForSeconds(time);
        isBoosting = false;
        currentSpeed = regularSpeed; // o el valor que tenga el suelo actual, si deseas
    }

    IEnumerator boostTextDuration(float time)
    {
        boostText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        boostText.gameObject.SetActive(false);
    }
}
