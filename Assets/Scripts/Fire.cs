using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject[] Skin;
    
    private float _shootForce = 500f;
    private Rigidbody2D _rigidb2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Fire")
        {
            Destroy(this.gameObject);
        }
    }


    public void Shoot(int directionToShoot, int player)
    {
        if(player == 1)
        {
            Skin[0].SetActive(true);
        }
        else
        {
            Skin[1].SetActive(true);

        }
        _rigidb2 = GetComponent<Rigidbody2D>();
        _rigidb2.AddForce(new Vector2(directionToShoot, _shootForce));
    }
}
