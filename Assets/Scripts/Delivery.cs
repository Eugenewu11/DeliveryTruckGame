using UnityEngine;


public class Delivery : MonoBehaviour
{
    public Driver driver;
    void Start()
    {

        driver = GetComponent<Driver>();
    }
    bool hasPackage; //Default False
    [SerializeField] float delay = 0.3f; //Delay de destruccion
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Si pasamos por el paquete y no tenemos el paquete
        if (collision.CompareTag("IceCreamPackage") && !hasPackage)
        {
            Debug.Log("Recogiste un paquete");  
            hasPackage = true;
            playParticles();
            Destroy(collision.gameObject,delay); //Destuye el paquete cuando lo recoge
            driver.currentSpeed = 30; 
        }

        //Si pasamos por el cliente y tenemos el paquete entonces se ejecutrara lo siguiente
        if (collision.CompareTag("Customer") && hasPackage)
        {
            Debug.Log("Entregaste un paquete");
            hasPackage = false;
            stopParticles();
            driver.currentSpeed = driver.regularSpeed;
        }
    }



    void playParticles()
    {
        GetComponent<ParticleSystem>().Play(); //Al hacer play se iniciara el sistema de particulas
    }
    void stopParticles()
    {
        GetComponent<ParticleSystem>().Stop(); //Parar el sistema de particulas
    }
}
