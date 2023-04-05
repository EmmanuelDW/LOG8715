using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerGhost : NetworkBehaviour
{
    [SerializeField] 
    private Player m_Player;

    private float _LocalSpeed;
    private Vector2 _LocalPosition;
    private List<Vector2> _LocalPositionBuffer;
    
    
    
    [SerializeField] 
    private SpriteRenderer m_SpriteRenderer;

    public override void OnNetworkSpawn()
    {
        // L'entite qui appartient au client est recoloriee en rouge
        _LocalPosition = m_Player.Position;
        _LocalSpeed = m_Player.m_Velocity;
        
        if (IsOwner)
        {
            m_SpriteRenderer.color = Color.red;
        }
    }

    private void Update()
    {



        _LocalPosition += InputCollecting();
        transform.position = _LocalPosition * (Time.deltaTime * _LocalSpeed);
        //transform.position = m_Player.Position;
    
    
    
    }

    private Vector2 InputCollecting()
    {
        Vector2 inputDirection = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputDirection += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputDirection += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputDirection += Vector2.right;
        }

        return inputDirection;

    }



    
}

