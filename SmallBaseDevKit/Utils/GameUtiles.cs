using System;
using System.Collections.Generic;
/// <summary>
/// Класс со всякой "полезностью" для игры.
/// </summary>
public static class GameUtiles
{
    /// <summary>
    /// Метод поиска в связном списке определенного объекта, с учетом выполнения поискового условия.
    /// </summary>
    /// <typeparam name="T">Тип объекта.</typeparam>
    /// <param name="list">Текущий связный список объектов.</param>
    /// <param name="equalCallback">Условие соотвествия поиска.</param>
    /// <param name="pivotObject">Сравниваемый объект.</param>
    /// <returns><see cref="bool"/></returns>
    public static bool ContainObjectInLinkedList<T>(LinkedList<T> list, Func<object,object,bool> equalCallback, object pivotObject)
    {
        bool result = false;
        var startNode = list.First;
        var endNode = list.Last;
       
        for(int i = 0; i < list.Count; ++i)
        {
            if(equalCallback(pivotObject, startNode.Value) || equalCallback(pivotObject, endNode.Value))
            {
                result = true;
                break;
            }
            if(Equals(startNode.Value, endNode.Value))
            {
                break;
            }
            startNode = startNode.Next;
            endNode = endNode.Previous;
        }

        return result;
    }
    /// <summary>
    /// Метод по получению объекта из связного списка, с учетом выполнения поискового условия.
    /// </summary>
    /// <typeparam name="T">Тип объекта.</typeparam>
    /// <param name="list">Текущий связный список.</param>
    /// <param name="equalCallback">Условие поиска.</param>
    /// <param name="pivotObject">Объект сравнивания.</param>
    /// <param name="findObject">Найденый объект.</param>
    /// <returns><see cref="bool"/></returns>
    public static bool TryGetObjectInLinkedList<T>(LinkedList<T> list, Func<object, object, bool> equalCallback, object pivotObject, out T findObject)
    {
        findObject = default;

        var startNode = list.First;
        var endNode = list.Last;

        for(int i = 0; i < list.Count; ++i)
        {
            if(equalCallback(pivotObject, startNode.Value))
            {
                findObject = startNode.Value;
                break;
            }
            if(equalCallback(pivotObject, endNode.Value))
            {
                findObject = endNode.Value;
                break;
            }
            if(Equals(startNode.Value, endNode.Value))
            {
                break;
            }
            startNode = startNode.Next;
            endNode = endNode.Previous;
        }

        return findObject != null;
    }
}
