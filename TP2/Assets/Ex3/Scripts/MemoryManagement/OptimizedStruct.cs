using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public struct ComponentContainer<T>
    where T : IComponent
{
    private uint[] _ids;
    private  T[] _components;

    public void Clear()
    {
        this._ids = new uint[ComponentsManager.maxEntities];
        this._components = new T[ComponentsManager.maxEntities];

    }

    public T[] Values => _components;
    

    public void Remove(uint key)
    {
        int count = 0;
        T[] tempComponents = new T[ComponentsManager.maxEntities];
        uint[] tempIds = new uint[ComponentsManager.maxEntities] ;
        
        for(var i = 0; i < _ids.Length; i++ )
        {
            if (i != key)
            {
                tempComponents[count] = _components[i];
                tempIds[count] = _ids[i];
                count++;

            }
            
        }
    }



    public bool ContainsKey(uint key)
    {
        foreach (var id in _ids)
        {
            if (id == key)
            {
                return true;
            }
        }
        return false;
    }

    public ref T this[uint key]
    {
        get
        {
            var j = 0;
            foreach (var id in _ids)
            {
                if (id == key)
                {
                    break;
                }
                j++;
            }

            return ref _components[j];

        }


    }

}



