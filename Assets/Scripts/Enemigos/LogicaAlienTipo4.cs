using UnityEngine;

public class LogicaAlienTipo4 : MonoBehaviour
{
    public float velocidadDescenso = 1f;
    public Transform barrera;
    public int vida = 500;
    public Sprite spriteMuerto;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false;
    public float tiempoMuerte = 2f;
    public Transform puntoDeteccion;
    public delegate void BarreraColisionHandler();
    public event BarreraColisionHandler OnBarreraColision;
    public int valorEter = 50;
    private bool alcanzadoCentro = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (puntoDeteccion == null)
        {
            puntoDeteccion = transform;
        }
    }

    void Update()
    {
        if (estaMuerto || alcanzadoCentro) return;

        transform.Translate(Vector2.down * velocidadDescenso * Time.deltaTime);

        if (transform.position.y <= Camera.main.transform.position.y)
        {
            alcanzadoCentro = true;
        }

        if (barrera != null && transform.position.y <= barrera.position.y + 0.46f)
        {
            OnBarreraColision?.Invoke();
            Destroy(gameObject);
        }
    }

    public void RecibirDaño(int daño)
    {
        if (estaMuerto) return;

        vida -= daño;

        if (vida <= 0)
        {
            MatarAlien();
        }
    }

    public Vector2 ObtenerPuntoDeteccion()
    {
        return puntoDeteccion.position;
    }

    void MatarAlien()
    {
        GestionarEter gestionarEter = FindObjectOfType<GestionarEter>();
        if (gestionarEter != null)
        {
            gestionarEter.SumarEter(valorEter);
        }

        estaMuerto = true;

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }

        if (spriteMuerto != null)
        {
            spriteRenderer.sprite = spriteMuerto;
        }

        gameObject.tag = "Muerto";
        StartCoroutine(Desaparecer());
    }

    System.Collections.IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(1f);

        float fadeDuration = 1f;
        float fadeStep = 0.1f;
        for (float t = 0; t < fadeDuration; t += fadeStep)
        {
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1, 0, t / fadeDuration);
                spriteRenderer.color = color;
            }
            yield return new WaitForSeconds(fadeStep);
        }

        OnBarreraColision = null;
        Destroy(gameObject);
    }
}