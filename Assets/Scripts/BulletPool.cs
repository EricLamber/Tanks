using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BulletPool<T> where T : MonoBehaviour
{
    private T m_Bulletprefab;
    private bool m_AutoExpand;
    private Transform m_Conteiner;

    public T Bulletprefab => m_Bulletprefab;
    public bool AutoExpand => m_AutoExpand;
    public Transform Conteiner => m_Conteiner;

    private List<T> m_Pool;

    public BulletPool(T bullet, int count, bool expanding)
    {
        m_Bulletprefab = bullet;
        m_AutoExpand = expanding;
        m_Conteiner = null;

        CreatePool(count);
    }

    public BulletPool(T bullet, int count, bool expanding, Transform conteiner)
    {
        m_Bulletprefab = bullet;
        m_AutoExpand = expanding;
        m_Conteiner = conteiner;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        m_Pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(bool isActieByDefalt = false)
    {
        T bullet = Object.Instantiate(m_Bulletprefab, m_Conteiner);

        bullet.gameObject.SetActive(isActieByDefalt);

        m_Pool.Add(bullet);
        return bullet;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (T item in m_Pool)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                element = item;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if(HasFreeElement(out T element)) 
            return element;

        if(m_AutoExpand)
            return CreateObject(true);

        throw new System.Exception($"Has no free element in pool of type {typeof(T)}");
    }
}
