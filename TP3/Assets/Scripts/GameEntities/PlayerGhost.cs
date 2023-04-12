using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerGhost : NetworkBehaviour
{
    [SerializeField] 
    private Player m_Player;

   // private GameState gamestate;

    private uint serverTickRate;
    private int serverTick;
    
    
    [SerializeField] 
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private float m_Size = 1;

    //private GameState m_GameState;
    //private GameState GameState
    //{
    //    get
    //    {
    //        if (m_GameState == null)
    //        {
    //            m_GameState = FindObjectOfType<GameState>();
    //        }
    //        return m_GameState;
    //    }
    //}

    public override void OnNetworkSpawn()
    {
        // L'entite qui appartient au client est recoloriee en rouge

        if (IsServer)
        {
            
        }
        if (IsClient)
        {
            //serverTickRate = gamestate.ServerTickRate.Value;

        }

        if (IsOwner)
        {
            m_SpriteRenderer.color = Color.red;
            
        }
    }

    private void Update()
    {
       
        if (IsServer)
        {
            transform.position = m_Player.Position;
        }
        
        if (IsClient)
        {
            //serverTick = gamestate.ServerTick.Value;
            transform.position += (Vector3)InputCollecting() * (m_Player.m_Velocity * Time.deltaTime);
            var size = m_Player.m_GameState.GameSize;
            if (transform.position.x - m_Size < -size.x)
            {
                transform.position = new Vector2(-size.x + m_Size, transform.position.y);
            }
            else if (transform.position.x + m_Size > size.x)
            {
                transform.position = new Vector2(size.x - m_Size, transform.position.y);
            }

            if (transform.position.y + m_Size > size.y)
            {
                transform.position = new Vector2(transform.position.x, size.y - m_Size);
            }
            else if (transform.position.y - m_Size < -size.y)
            {
                transform.position = new Vector2(transform.position.x, -size.y + m_Size);
            }
            //transform.position = m_Player.Position;
        }


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

