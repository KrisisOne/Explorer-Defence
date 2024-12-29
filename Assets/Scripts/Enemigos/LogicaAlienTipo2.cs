using UnityEngine;

public class LogicaAlienTipo2 : MonoBehaviour
{
    public float velocidadDescenso = 1f;
    public Transform barrera;
    public int vida = 100;
    public Sprite spriteMuerto;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false;
    public float tiempoMuerte = 2f;
    public delegate void BarreraColisionHandler();
    public event BarreraColisionHandler OnBarreraColision;
    public int valorEter = 5;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (estaMuerto) return;

        transform.Translate(Vector2.down * velocidadDescenso * Time.deltaTime);

        if (barrera != null && transform.position.y <= barrera.position.y + 0.46f)
        {
            Debug.Log("Alien tipo 2 chocó con la barrera.");
            OnBarreraColision?.Invoke();
            Destroy(gameObject);
        }
    }

    public void RecibirDaño(int daño)
    {

        Debug.Log("daño.");
        if (estaMuerto) return;

        vida -= daño;

        if (vida <= 0)
        {
            MatarAlien();
        }
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
        StartCoroutine(desaparecer());
    }

    System.Collections.IEnumerator desaparecer()
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
