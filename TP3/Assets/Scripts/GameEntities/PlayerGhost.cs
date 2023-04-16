using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerGhost : NetworkBehaviour
{
    [SerializeField] private Player m_Player;
    private GameState m_GameState;

    private Vector2 positionLocal;
    private List<int> tickRegistery;
    private List<Vector2> positionRegistery;
    private int lastTickChecked;
    private Vector2 diffrence;

    [SerializeField] private SpriteRenderer m_SpriteRenderer;

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


        if (IsOwner)
        {
            m_SpriteRenderer.color = Color.red;

        }
    }

    public void Awake()
    {
        m_Player = FindObjectOfType<Player>();
        m_GameState = m_Player.m_GameState;
        tickRegistery = new List<int>();
        positionRegistery = new List<Vector2>();
        diffrence = new Vector2(0, 0);
        
        lastTickChecked = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(transform.position);
        }

        if (IsServer)
        {
            transform.position = m_Player.Position;
        }

        if (IsClient)
        {
            //Stun check
            if (m_GameState.m_stunnedLocal)
            {
                return;
            }

            positionLocal = (Vector2)transform.position + (InputCollecting() * (m_Player.m_Velocity * Time.deltaTime));
            
            //Wall collision check
            WallCollision();
            
            //Ajout de la position au registre
            positionRegistery.Add(positionLocal);
            tickRegistery.Add(m_GameState.localTick);
            // if (lastTickChecked == m_Player.m_ClientTick.Value)
            // {
            //     transform.position = positionRegistery[0];
            //     return;
            // }
            
            diffrence = DiffrenceVector();
            if (diffrence.magnitude > 1)
            {
                Reconciliation(diffrence);
                Debug.Log("recon");
                
            }
            // if (Input.GetKeyDown(KeyCode.Mouse0))
            // {
            //     Debug.Log(DiffrenceVector().magnitude);
            //     Debug.Log(tickRegistery[^1]);
            //     Debug.Log(m_Player.m_ClientTick.Value);
            // }
            
            
            transform.position = positionRegistery[^1];
            //Debug.Log(positionRegistery.Count);
            //positionRegistery.RemoveAt(0);
            //lastTickChecked = m_Player.m_ClientTick.Value;
            
            //Réécriture des registre sans les positions associées à des frames passées
            //RegisteryCleanUp();
            
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

    private void WallCollision()
    {
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
    }

    
    private void RegisteryCleanUp()
    {
        List<Vector2> vectorListBuffer = new List<Vector2>();
        List<int> intListBuffer = new List<int>();

        for (int i = 0; i < positionRegistery.Count - 1; i++)
        {
            if (tickRegistery[i] > lastTickChecked)
            {
                vectorListBuffer.Add(positionRegistery[i]);
                intListBuffer.Add(tickRegistery[i]);
            }
            positionRegistery = vectorListBuffer;
            tickRegistery = intListBuffer;
        }
    }

    private Vector2 DiffrenceVector()
    {
        int i = 0;
        float diffx = 0f;
        float diffy = 0f;
        
        foreach (int tick in tickRegistery)
        {
            if (tick == m_Player.m_ClientTick.Value)
            {
                i = tick;
                break;
            }
        }
        
        diffx = m_Player.Position.x - positionRegistery[i].x;
        diffy = m_Player.Position.y - positionRegistery[i].y;

        return new Vector2(diffx, diffy);
    }

    private void Reconciliation(Vector2 diff)
    {
        List<Vector2> buffer = new List<Vector2>();

        foreach (Vector2 position in positionRegistery)
        {
            buffer.Add(new Vector2((position.x + diff.x),(position.y + diff.y)));
        }

        positionRegistery = buffer;

    }



}

