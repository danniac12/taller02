using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Taller2 : MonoBehaviour
{



    /// <summary>
    /// usamos un valor random para determinar cuantos GameObjects habran.
    /// de manera aleatorea se le otorgan componentes a estos GameObjects
    /// </summary>

    void Start()
    {
        
        int unic = 0;
       
        
        for (int i = 0; i < Random.Range(10, 21); i++)
        {
            int entidades = Random.Range(unic, 3);
           
            if (unic == 0)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.AddComponent<Hero>().Init();
                unic = unic + 1;
            }
            if(entidades == 1)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.AddComponent<Aldeano>().Comun();
            }
            if(entidades == 2)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.AddComponent<Walker>().SinCerbro();
            }
        }
      
    }
}

public class Hero : MonoBehaviour
{
    /// <summary>
    /// se realiza una referencia para los scripts de movimiento y mirar del Heroe (jugador)
    /// Agregamos los componentes necesarios para realizar collisiones y se desactiva el uso de graveda 
    /// tambien se activan todas los constrains para hacer que solo se pueda mover por el codigo y no por alguna colision
    /// </summary>
    Movement movement;
    Look look;
    float speed;
    public void Init()
    {
       
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        gameObject.name = "Hero";
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        // se le agregan los scripts de movimiento y mirar al jugador 
        movement = gameObject.AddComponent<Movement>();
        look = gameObject.AddComponent<Look>();
        gameObject.AddComponent<Camera>();
        // determina una velocidad al azar para entregarcela al metodo de movimiento mas adelante
        speed = Random.Range(0.5f, 2);



    }
    /// <summary>
    /// llama a los constructores en las clases de Movement y Look
    /// </summary>
    private void Update()
    {
        movement.Move(speed);
        look.Arround();
    }
    /// <summary>
    /// se toman los valores almacendos en el estruc para imprimirlos cada que el heroe colisione con 
    /// el respectivo GameObject
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Walker>())
        {
            InfoZomb info = collision.gameObject.GetComponent<Walker>().GetInfo();
            Debug.Log("Waaar quiero comer " + info.gusto);
        }
        if(collision.gameObject.GetComponent<Aldeano>())
        {
            InfoAlde info = collision.gameObject.GetComponent<Aldeano>().GetInfo();
            Debug.Log("soy " +info.name+" y tengo " + info.edad );
        }
    }
}

public class Aldeano : MonoBehaviour
{

    /// <summary>
    /// se realiza una enumeracion para guardar los posibles nombres de los aldeanos 
    /// y se le da nombre a esa enumeracion, tambien creamos un espacio para almacenar la edad de los aldeanos
    /// </summary>
    int age;
    public enum Nombres
    {
      santiago, rio, tere, troy, Joe ,
      Aleesha ,Abdirahman ,Kaycee ,Chantal ,Cherise ,
      Taiba ,Sanah ,Jack , Pascal ,Kelly ,
      Everly ,Muna ,Anya ,Marguerite ,Kali 

    }

    public Nombres nombres;
    /// <summary>
    /// igual en en el heroe se le agrega un comoponente  Rigidbody y se desactivan las influencias externas
    /// para que no se mueva debido a colisiones 
    /// le damos un valor random a la edad entre 15 y 100 
    /// y tomamos un nombre random del enumerador ademas de ponerlo en un lugar aleatorio del mapa 
    /// entre las posiciones 10 y -10 en los ejes "x" y "z"
    /// </summary>
    public void Comun()
    {
        
        age = Random.Range(15, 100);
        nombres = (Nombres)Random.Range(0,21);
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        this.gameObject.name = nombres.ToString();
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        this.gameObject.transform.position = new Vector3(x, 0, z);
    }
    /// <summary>
    /// almacenamos en el struct del aldeano el nombre que le dimos aleatoreamente
    /// y su edad
    /// </summary>
    /// <returns></returns>
    public InfoAlde GetInfo()
    {
        InfoAlde infoAlde = new InfoAlde();
        infoAlde.edad = age;
        infoAlde.name = nombres.ToString();
        return infoAlde;
    }
}


public class Walker : MonoBehaviour
{
  /// <summary>
  /// declaramos 2 enumeradores uno para el estado del zombie y otro para la comida preferida
  /// asi como abrimos un espacio para lamacenar un contador que usaremos mas adelante
  /// </summary>
   
    float t;
   
    public enum Comer
    {
        Cerebro,
        Pierna,
        Vesicula,
        Brazo,
        Costilla
    }
    
    public enum Estado
    {
        idel,
        moving
    }
   
    Comer comer;
    Estado estado;
    /// <summary>
    /// le otorgamso un rigidboy como en las otras dos calses para efectuar la colisiones
    /// y congelamos en todas las posiciones para evitar comportamientos erraticos debido a estas colisiones
    /// se determina de que color sera el zombie de maenra aleatorea
    /// y se posicionea en un lugar random del mundo entre 10 y menos 10 de los ejes "z" y "x"
    /// </summary>
    public void SinCerbro()
    {
        Rigidbody rb;
        comer = (Comer)Random.Range(0, 5);
      
        int cambio = Random.Range(1, 4);
        if (cambio == 1)
        {
            
            Renderer color = this.gameObject.GetComponent<Renderer>();
            color.material.color = Color.green;
            this.gameObject.name = "Zombie";
            rb = this.gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
        }
        if (cambio == 2)
        {
           
            Renderer color = this.gameObject.GetComponent<Renderer>();
            color.material.color = Color.cyan;
            this.gameObject.name = "Zombie";
            rb = this.gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
        }
        if(cambio == 3)
        {
           
            Renderer color = this.gameObject.GetComponent<Renderer>();
            color.material.color = Color.magenta;
            this.gameObject.name = "Zombie";
            rb = this.gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;

        }
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        this.gameObject.transform.position = new Vector3(x, 0, z);
        
    }
    /// <summary>
    /// almacenamos en el struct del zombi su preferencia alimenticia
    /// </summary>
    public InfoZomb GetInfo()
    {
        InfoZomb infoZomb = new InfoZomb();
        infoZomb.gusto = comer.ToString();
        return infoZomb;
    }
    /// <summary>
    /// /utilisamos un contador "t" para determinar el tiempo entre cada estado del zombie 
    /// y acceder al siguiente estado de manera alkeatorea cada 5 segundos y cambia la direccion en la que mira
    /// en el estado moving el zombie se desplaza hacia el vector z con una velociad reducida 
    /// </summary>
   private void Update()
    {            
       t += Time.deltaTime;
        if (t >= 5)
        {
            estado = (Estado)Random.Range(0, 2);
            this.gameObject.transform.Rotate(0, Random.Range(1f, 361f), 0, 0);
            t = 0;
        }
        switch (estado)
        {
            case Estado.idel:                                                                      
                break;
            case Estado.moving:    
                
                this.gameObject.transform.Translate(transform.forward*0.02f);                  
                break;
            default:
                break;
        }              
    }
}


