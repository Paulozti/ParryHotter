using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Public
    public Transform groundCheck;
    public LayerMask whatIsGround;
    [HideInInspector]
    public bool isGrounded = false;
    public GameObject[] skin;
    public GameObject firePrefab;
    public enum Player
    {
        Player1,
        Player2
    };
    public Player player;

    public GameObject[] target;
    public RawImage playerLife;
    public Texture[] lifesSprites;
    public UIManager UI;

    //Private
    [SerializeField]
    private float _moveSpeed = 4;
    [SerializeField]
    private float _jumpForce = 200;
    private Rigidbody2D _rigidb2;
    private bool _jump = false;
    private bool _isLookingRight = true;
    private string _jumpButton;
    private string _horizontalAxis;
    private string _verticalAxis;
    private int _directionToShoot = 0;
    private int _targetPosition = 0;
    private int _life = 3;
    
    

    void Start()
    {
        _rigidb2 = GetComponent<Rigidbody2D>();
        playerConfig();
        target[3].SetActive(true);
        StartCoroutine(automaticShooting());
    }

    void Update()
    {
      
        inputCheck();
        move();
    }

    void playerConfig()
    {
        if (player == Player.Player1)
        {
            skin[0].SetActive(true);
            skin[1].SetActive(false);
            _jumpButton = "Jump1";
            _horizontalAxis = "Horizontal1";
            _verticalAxis = "Vertical1";
        }
        else
        {
            skin[0].SetActive(false);
            skin[1].SetActive(true);
            _jumpButton = "Jump2";
            _horizontalAxis = "Horizontal2";
            _verticalAxis = "Vertical2";
        }
    }

    void inputCheck() 
    { 
        if(Input.GetButtonDown(_jumpButton) && isGrounded)
        {
            _jump = true;
        }
        if(Input.GetButtonDown(_verticalAxis))
        {
            float verticalDirection = Input.GetAxis(_verticalAxis);
            if (verticalDirection < 0)
            {
                if(_targetPosition > -3)
                {
                    _targetPosition -= 1;
                }
            }
            else
            {
                if(_targetPosition < 3)
                {
                    _targetPosition += 1;
                }
            }
            adjustTarget(_targetPosition);
        }
    }

    void adjustTarget(int targetPosition) 
    {
        switch (targetPosition)
        {
            case -3:
                target[0].SetActive(true);
                target[1].SetActive(false);
                _directionToShoot = -150;
                break;
            case -2:
                target[0].SetActive(false);
                target[1].SetActive(true);
                target[2].SetActive(false);
                _directionToShoot = -100;
                break;
            case -1:
                target[1].SetActive(false);
                target[2].SetActive(true);
                target[3].SetActive(false);
                _directionToShoot = -50;
                break;
            case 0:
                target[2].SetActive(false);
                target[3].SetActive(true);
                target[4].SetActive(false);
                _directionToShoot = 0;
                break;
            case 1:
                target[3].SetActive(false);
                target[4].SetActive(true);
                target[5].SetActive(false);
                _directionToShoot = 50;
                break;
            case 2:
                target[4].SetActive(false);
                target[5].SetActive(true);
                target[6].SetActive(false);
                _directionToShoot = 100;
                break;
            case 3:
                target[5].SetActive(false);
                target[6].SetActive(true);
                _directionToShoot = 150;
                break;
        }
    }


    void move()
    {
        float horizontalForce = Input.GetAxis(_horizontalAxis); //-1.0 ~ -0.1 for left or 0.1 ~ 1.0 for right
        _rigidb2.velocity = new Vector2(horizontalForce * _moveSpeed, _rigidb2.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f,whatIsGround); // Check if layer whatIsGround is around the radius from groundCheck position. Return true or false.

        if((horizontalForce > 0 && !_isLookingRight) || (horizontalForce < 0 && _isLookingRight))
        {
            flip();
        }

        if (_jump)
        {
            _rigidb2.AddForce(new Vector2(0,_jumpForce));
            _jump = false;
        }
    }

    void flip()
    {
        _isLookingRight = !_isLookingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

  IEnumerator automaticShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Fire fire = Instantiate(firePrefab, new Vector2(transform.position.x, transform.position.y + 0.1f), Quaternion.identity).GetComponent<Fire>();
            if(fire == null)
            {
                Debug.LogError("FIRE RIGIDBODY IS NULL ON PLAYER");
            }
            if(player == Player.Player1)
            {
                fire.tag = "Fire1";
                fire.Shoot(_directionToShoot, 1);
            }
            else
            {
                fire.tag = "Fire2";
                fire.Shoot(_directionToShoot, 2);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire2" && player == Player.Player1)
        {
            damage(Player.Player2);
        }
        if (collision.gameObject.tag == "Fire1" && player == Player.Player2)
        {
            damage(Player.Player1);
        }
    }

    void damage(Player playerVencedor)
    {
        _life -= 1;
        Update_life_UI();
        if (_life == 0)
        {
            UI.GameOver(playerVencedor);
        }
    }

    public void Update_life_UI()
    {
        switch (_life)
        {
            case 3:
                playerLife.texture = lifesSprites[3];
                break;
            case 2:
                playerLife.texture = lifesSprites[2];
                break;
            case 1:
                playerLife.texture = lifesSprites[1];
                break;
            case 0:
                playerLife.texture = lifesSprites[0];
                break;
        }
        
    }
}
