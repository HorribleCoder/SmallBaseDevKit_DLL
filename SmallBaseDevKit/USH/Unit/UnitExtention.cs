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
    /// <summary>
    /// Добавить визульную реализацию юнита.
    /// </summary>
    /// <param name="unit">Юнит.</param>
    /// <param name="visualPrototype">Прототип визуала юнита.</param>
    public static void AddUnitVisual(this IUnitVisual unit, GameObject visualPrototype)
    {
        unit.AddUnitVisual(visualPrototype);
    }
    /// <summary>
    /// Сбросить настройку визуальной реализации юнита.
    /// </summary>
    /// <param name="unit">Юнит.</param>
    public static void RemoveUnitVisual(this IUnitVisual unit)
    {
        unit.RemoveUnitVisual();
    }
}
