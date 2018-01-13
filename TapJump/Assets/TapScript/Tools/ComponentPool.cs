using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool <T> where T: Component {

    GameObject _prefab;
    public ComponentPool(int initNum, GameObject prefab,Transform parent = null)
    {
        _prefab = prefab;
        for(int i  = 0; i < initNum; i ++)
        {
            var com = GenGameObject();
            Debug.Assert(com != null);
            com.gameObject.SetActive(false);

            com.transform.parent = parent;
            
            _unused.Add(com);
        }
    }

   public T GenGameObject()
    {
        var gb = GameObject.Instantiate<GameObject>(_prefab);
        var com = gb.GetComponent<T>();
        
        return com;
    }

    public HashSet<T> GetUsedSet()
    {
        return _usedSet;
    }

    public T GetUnusedOne()
    {
        T thing = null;
        var unusedEn = _unused.GetEnumerator();
        while (unusedEn.MoveNext())
        {

             thing = unusedEn.Current;
            break;
        }
        if (thing == null)
        {
            thing = GenGameObject();
        }

        _unused.Remove(thing);
        _usedSet.Add(thing);
        thing.gameObject.SetActive(true);

        return thing;

        
    }

    public void Retrive(T thing)
    {
        if (thing != null )
        {
            thing.gameObject.SetActive(false);
            _usedSet.Remove(thing);
            _unused.Add(thing);
        }

    }

    public void RetriveAll()
    {
        foreach(var thing in _usedSet)
        {
            thing.gameObject.SetActive(false); 
            _unused.Add(thing);
        }
        _usedSet.Clear();
    }

    HashSet<T> _unused = new HashSet<T>();
    HashSet<T> _usedSet = new HashSet<T>();
 
}
