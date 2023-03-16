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
    //version avec keyvaluepair
    /*
        private KeyValuePair<uint, T>[] _components;


        public void Clear()
        {

            this._components = new KeyValuePair<uint, T>[ComponentsManager.maxEntities];

        }
        public T[] Values()
        {
            T[] tempComponents = new T[ComponentsManager.maxEntities];
            foreach (var pair in _components)
            {
                tempComponents[pair.Key] = pair.Value;
            }
            return tempComponents;
        }

        public void Remove(uint key)
        {
            int count = 0;

            KeyValuePair<uint, T>[] tempComponents = new KeyValuePair<uint, T>[ComponentsManager.maxEntities] ;

            for(var i = 0; i<_components.Length; i++)
            {
                if (i != key)
                {
                    tempComponents[count] = this._components[i];
                    count++;
                }

            }

            this._components = tempComponents;
        }



        public bool ContainsKey(uint key)
        {
            foreach (var pair in _components)
            {
                if (pair.Key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public KeyValuePair<uint,T> this[uint key]
        {
            get
            {
                foreach (var pair in _components)
                {
                    if (pair.Key == key)
                    {
                        return this._components[key] ;
                    }
                }
                return default;

            }
            set
            {

                foreach (KeyValuePair<uint, T> pair in _components)
                {
                    if (pair.Key == key)
                    {
                        KeyValuePair<uint, T> newPair = new KeyValuePair<uint, T>(key, value.Value);
                        this._components[key] = newPair;

                    }


                }

            }

        }*/

    private uint[] _ids;
    private T[] _components;

    


    public void Clear()
    {
        this._ids = new uint[ComponentsManager.maxEntities];
        this._components = new T[ComponentsManager.maxEntities];

    }
    public T[] Values()
    {
        return _components;
    }

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

    public T this[uint key]
    {
        get
        {
            for(var i = 0; i < _ids.Length; i++ )
            {
                if (_ids[i] == key)
                {
                    return _components[i];

                }
            
            }
            return default;

        }
        set
        {
            for(var i = 0; i < _ids.Length; i++ )
            {
                if (_ids[i] == key)
                {
                    _components[i] = value;

                }
            
            }
            

        }

    }

}



