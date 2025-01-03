using UnityEngine;

public class LogicaDisparos : MonoBehaviour
{
    public int dañoBase = 100;
    public int incrementoDañoPorNivel = 50;

    public int daño;

    void Start()
    {
        int nivelDaño = PlayerPrefs.GetInt("NivelDaño", 1);
        daño = dañoBase + (nivelDaño * incrementoDañoPorNivel);
    }

    public void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Pared"))
        {
            Destroy(gameObject);
        }

        if (colision.gameObject != null && colision.gameObject.CompareTag("Enemigo"))
        {
            LogicaAlienTipo1 alien = colision.gameObject.GetComponent<LogicaAlienTipo1>();
            LogicaAlienTipo2 alienTipo2 = colision.gameObject.GetComponent<LogicaAlienTipo2>();
            LogicaAlienTipo3 alienTipo3 = colision.gameObject.GetComponent<LogicaAlienTipo3>();
            LogicaAlienTipo4 alienTipo4 = colision.gameObject.GetComponent<LogicaAlienTipo4>();

            if (alien != null)
            {
                alien.RecibirDaño(daño);
            }
            if (alienTipo2 != null)
            {
                alienTipo2.RecibirDaño(daño);
            }
            if (alienTipo3 != null)
            {
                alienTipo3.RecibirDaño(daño);
            }
            if (alienTipo4 != null)
            {
                alienTipo4.RecibirDaño(daño);
            }

            Destroy(gameObject);
        }
    }
}
