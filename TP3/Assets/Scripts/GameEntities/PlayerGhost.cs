using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerGhost : NetworkBehaviour
{
    [SerializeField] 
    private Player m_Player;
    private GameState m_GameState;

   // private GameState gamestate;

    private uint serverTickRate;
    private int serverTick;
    private Vector2 positionLocal;
    
    
    [SerializeField] 
    private SpriteRenderer m_SpriteRenderer;

    public override void OnNetworkSpawn()
    {
        // L'entite qui appartient au client est recoloriee en rouge

        if (IsServer)
        {
            
        }
        if (IsClient)
        {
            serverTickRate = m_GameState.ServerTickRate.Value;

        }

        if (IsOwner)
        {
            m_SpriteRenderer.color = Color.red;
            
        }
    }

    public void Awake()
    {
        m_Player = FindObjectOfType<Player>();
        m_GameState = m_Player.m_GameState;
    }
    private void Update()
    {
        //Debug.Log(m_GameState.m_CurrentRtt);
       
        if (IsServer)
        {
            transform.position = m_Player.Position;
        }
        
        if (IsClient)
        {
            if (m_GameState.m_stunnedLocal)
            {
                return;
            }
                
            serverTick = m_GameState.ServerTick.Value;
            
            positionLocal = (Vector2)transform.position + (InputCollecting() * (m_Player.m_Velocity * Time.deltaTime));
            
            var size = m_GameState.GameSize;
            var m_Size = m_Player.m_Size;
            if (positionLocal.x - m_Size < -size.x)
            {
                positionLocal = new Vector2(-size.x + m_Size, positionLocal.y);
            }
            else if (positionLocal.x + m_Size > size.x)
            {
                positionLocal = new Vector2(size.x - m_Size, positionLocal.y);
            }

            if (positionLocal.y + m_Size > size.y)
            {
                positionLocal = new Vector2(positionLocal.x, size.y - m_Size);
            }
            else if (positionLocal.y - m_Size < -size.y)
            {
                positionLocal = new Vector2(positionLocal.x, -size.y + m_Size);
            }

            transform.position = positionLocal;
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

