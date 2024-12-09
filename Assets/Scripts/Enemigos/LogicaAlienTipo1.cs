using UnityEngine;

public class LogicaAlienTipo1 : MonoBehaviour
{
    public float velocidadDescenso = 1f;
    
    public Transform barrera;
    public int vida = 300;
    public Sprite spriteMuerto;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false;
    public float tiempoMuerte = 2f; 


    void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update() {

        if (estaMuerto){
            return;
        } 
    
        transform.Translate(Vector2.down * velocidadDescenso * Time.deltaTime);

        if (transform.position.y <= barrera.position.y + 0.46f) {

            Destroy(gameObject);

        }

    }

    public void RecibirDaño(int daño) {

        if (estaMuerto){
            return;
        } 

        vida -= daño;

        if (vida <= 0)
        {
            MatarAlien();
        }

    }

    void MatarAlien() {

        //Debug.Log("MatarAlien llamado, cambiando sprite a muerto.");
        estaMuerto = true;

        Animator animator = GetComponent<Animator>();
        if (animator != null) {

            animator.enabled = false;
            
        }

        if (spriteMuerto != null) {

            spriteRenderer.sprite = spriteMuerto;
            //Debug.Log("Sprite cambiado a muerto.");

        } else {

            //Debug.LogError("SpriteMuerto no asignado en l inspector.");

        }

        gameObject.tag = "Muerto";

        Invoke(nameof(DestruirAlien), tiempoMuerte);
    }

    void DestruirAlien() {

        Destroy(gameObject);

    }
}