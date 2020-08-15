using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("Velocidad")]
    [SerializeField] private float speed;
    [SerializeField] public AudioClip audioGol, audioRaqueta, audioRebote;
    AudioSource fuenteDeAudio;
    [Header("Contador")]
    [SerializeField] private Text golLeft;
    [SerializeField] private Text golRight;

    // Start is called before the first frame update
    void Start()
    {        
        var direction = UnityEngine.Random.Range(0, 5);
        if (direction.Equals(2) && direction.Equals(4))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        }
        fuenteDeAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "PadLeft")
        {
            float y = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 direct = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = direct * speed;
            speed += (float)0.4;
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }else if (collision.gameObject.name == "PadRight")
        {
            float y = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 direct = new Vector2(-1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = direct * speed;
            speed += (float)0.4;
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }
        else
        {
            speed += (float)0.4;
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }

    private float HitFactor(Vector3 position1, Vector3 position2, float y)
    {
        return (position1.y - position2.y) / y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("GoalLeft"))
        {
            golRight.text = (Convert.ToInt32(golRight.text) + 1).ToString();
            ReiniciarBola(collision.gameObject.name);
        }
        else if (collision.gameObject.name.Equals("GoalRight"))
        {
            golLeft.text = (Convert.ToInt32(golLeft.text) + 1).ToString();
            ReiniciarBola(collision.gameObject.name);
        }
    }

    private void ReiniciarBola(string direct)
    {
        GetComponent<Rigidbody2D>().position = Vector2.zero;
        if (direct.Equals("GoalLeft"))
        {
            speed = 10;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
        else if (direct.Equals("GoalRight"))
        {
            speed = 10;
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        }
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }
}
