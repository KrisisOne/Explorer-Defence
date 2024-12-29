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
            LogicaAlienTipo2 alienTipo2 = colision.gameObject.GetComponent<LogicaAlienTipo2>();

            if (alien != null) {
                alien.RecibirDaño(daño);
            }
            if (alienTipo2 != null){
                alienTipo2.RecibirDaño(daño);
            }

            Destroy(gameObject); 
        
        }
    }
}