using System;
using UnityEngine;

using SmallBaseDevKit.GameException;
using SmallBaseDevKit.USH.Unit;

public static class UnitExtention
{
    /// <summary>
    /// Расширение для IUnit, пробуем прочитать данные игровой единицы.
    /// </summary>
    /// <typeparam name="T">Тип данных.</typeparam>
    /// <param name="unit">Текущий юнит.</param>
    /// <param name="data">Данные.</param>
    /// <exception cref="Exception">Если у текущего юнита нет реализации интерфеса IUnitWithData.</exception>
    public static void ReadUnitData<T>(this IUnit unit, out T data) where T : ScriptableObject
    {
        data = default;
        try
        {
            if (unit is IUnitWithData)
            {
                var iUnitData = (IUnitWithData)unit;
                data = iUnitData.ReadUnitData<T>();
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception e)
        {
            ExceptionHandler.ExceptionProcessExecute(e, $"Unit - {unit.GetType().Name} don't release interface IUnitWithData!");
        }
    }
}
