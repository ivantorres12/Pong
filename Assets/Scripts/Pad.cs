using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    [Header("Velocidad en el vector X")]
    [SerializeField] private Vector2 vector2;
    [Header("Controles para el game pad:")]
    [SerializeField] private KeyCode upControl;
    [SerializeField] private KeyCode downControl;
    private bool flagUp;
    private bool flagDown;

    void Update()
    {
        if (Input.GetKey(upControl) && flagUp.Equals(false))
        {
            transform.Translate(vector2);
        }
        else if (Input.GetKey(downControl) && flagDown.Equals(false))
        {
            transform.Translate(-vector2);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("PanelUp"))
        {
            flagUp = false;
        }
        else if (collision.collider.name.Equals("PanelDown"))
        {
            flagDown = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
        if (collision.collider.name.Equals("PanelUp"))
        {
            flagUp = true;
        }
        else if (collision.collider.name.Equals("PanelDown"))
        {
            flagDown = true;
        }
    }
}
