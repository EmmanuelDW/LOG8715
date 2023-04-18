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
    //private int lastTickChecked;
    private Vector2 diffrence;

    [SerializeField] private SpriteRenderer m_SpriteRenderer;

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
        
        //lastTickChecked = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(transform.position);
            Debug.Log(tickRegistery.Count);
            Debug.Log(positionRegistery.Count);
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

            Vector2 inputDirection = InputCollecting();
            if (inputDirection != Vector2.zero)
            {
                positionLocal += inputDirection * (m_Player.m_Velocity * Time.deltaTime);
            }

            //Wall collision check
            WallCollision();

            //Ajout de la position au registre
            positionRegistery.Add(positionLocal);
            tickRegistery.Add(m_GameState.localTick);

            // Nettoyage des positions anciennes dans le registre
            RegisteryCleanUp();

            // Réconciliation avec le serveur si la différence est importante
            diffrence = DiffrenceVector();
            if (diffrence.magnitude > 0.1f)
            {
                Reconciliation(diffrence);
                Debug.Log("Reconciliation");
            }

            transform.position = positionRegistery[^1];
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
        // List<Vector2> vectorListBuffer = new List<Vector2>();
        // List<int> intListBuffer = new List<int>();
        //
        // for (int i = 0; i < positionRegistery.Count - 1; i++)
        // {
        //     if (tickRegistery[i] > lastTickChecked)
        //     {
        //         vectorListBuffer.Add(positionRegistery[i]);
        //         intListBuffer.Add(tickRegistery[i]);
        //     }
        //     positionRegistery = vectorListBuffer;
        //     tickRegistery = intListBuffer;
        // }
        while (positionRegistery.Count() > 300)
        {
            positionRegistery.RemoveAt(0);
            tickRegistery.RemoveAt(0);
            
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
                
                break;
            }
            i++;
        }

        if (i >= 0 && i < positionRegistery.Count)
        {
            diffx = m_Player.Position.x - positionRegistery[i].x;
            diffy = m_Player.Position.y - positionRegistery[i].y;
        }
            

        return new Vector2(diffx, diffy);
    }

    private void Reconciliation(Vector2 diff)
    {
        for (int i = 0; i < positionRegistery.Count; i++)
        {
            positionRegistery[i] += diff;
        }
    }



}

