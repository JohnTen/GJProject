using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tan : MonoBehaviour
{
    public float pw_x = 400f;
    private Vector2 a;
    private Vector2 b;
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        a = collision.transform.position - this.transform.position ;
		
        b = a.normalized;
        if (collision.transform.tag == "Caterpillar")
        {
			collision.transform.GetComponent<Rigidbody2D>().AddForce(b * pw_x, ForceMode2D.Impulse);
        }
    }
}
