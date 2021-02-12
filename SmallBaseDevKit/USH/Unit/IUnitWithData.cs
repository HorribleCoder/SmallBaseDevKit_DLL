using UnityEngine;

namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Интрефес для юнитов с данными типа - <see cref="ScriptableObject"/>
    /// </summary>
    public interface IUnitWithData
    {
        /// <summary>
        /// Добавить игровой единице данные.
        /// </summary>
        /// <param name="data">Данные.</param>
        void SetUnitData<T>(T data) where T : ScriptableObject;
        /// <summary>
        /// Прочитать данные у игровой единицы.
        /// </summary>
        /// <returns></returns>
        T ReadUnitData<T>() where T: ScriptableObject;
    }
}
