using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTopDown : MonoBehaviour
{
    // float es número decimal
    [SerializeField] float speed;

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer sprite;

    // Vamos a referenciar el input para el movimiento
    InputAction moveAction;

    void Awake()
    {
        // Cargarmos los componentes del objeto (Player)}
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Al momento de iniciar el player, cargamos del archivo Inputs y guardamos en nuestro inputAction
        // Como se carga de un archivo externo, entonces por preceptos usamos el Start
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        // Acá vamos a detectar en todo momento qué tecla hemos presionado, de eso se encarga moveAction
        Vector2 direction = moveAction.ReadValue<Vector2>();
        // El personaje se mueve en dirección de las teclas que se han presionado.
        body.linearVelocityX = direction.x * speed;
        body.linearVelocityY = direction.y * speed;

        // La condición que vamos a colocar es, si no se está moviendo llamamos a la animación Idle
        // Y si no se cumple esa condición es porque se está moviendo y llamamos a la animación Run
        // para usar operadores lógicos se usa en C# los siguiente
        // 'y' -> &&
        // 'o' -> ||
        // 'negación' -> !
        bool estaCorriendo = false;
        if (direction.x == 0 && direction.y == 0)
        {
            // Acá vamos a llamar a la animación de idle, tengo que enviarle información de la velocidad
            estaCorriendo = false;
        }
        else
        {
            // Acá sería el caso en que se esté moviendo, que significa que tenemos que decirle qué velocidad tiene
            estaCorriendo = true;
            if (direction.x > 0.1)
            {
                // Como estoy yendo a la derecha, no lo flipeo en x porque es la posición original
                sprite.flipX = false;
            }
            else if (direction.x < -0.1)
            {
                // Como estoy yendo a la izquierda, lo flipeo en x para que voltee a la izquierda
                sprite.flipX = true;
            }
        }
        // Si no hay animación no llama al bool
        if (anim != null)
            anim.SetBool("estaCorriendo", estaCorriendo);
    } 
}
