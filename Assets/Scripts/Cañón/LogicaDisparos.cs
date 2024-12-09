using UnityEngine;

public class LogicaDisparos : MonoBehaviour
{
    public int daño = 100;

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Pared")) {
            Destroy(gameObject);
        }

        if (colision.gameObject != null && colision.gameObject.CompareTag("Enemigo")) {

            LogicaAlienTipo1 alien = colision.gameObject.GetComponent<LogicaAlienTipo1>();

            if (alien != null) {
                alien.RecibirDaño(daño);
            }

            Destroy(gameObject); 
        
        }
    }
}