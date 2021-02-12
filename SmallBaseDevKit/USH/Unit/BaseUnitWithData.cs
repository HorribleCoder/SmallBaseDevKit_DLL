using UnityEngine;


namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Базовый класс игровой единицы, в которой хранятся данные настройки./>
    /// </summary>
    /// <typeparam name="UnitData">Входные данные настройки юнита, формат - <see cref="ScriptableObject"/>.</typeparam>
    public abstract class BaseUnitWithData<UnitData>: BaseUnit , IUnitWithData
        where UnitData: ScriptableObject
    {
        private UnitData _data;

        void IUnitWithData.SetUnitData<T>(T data)
        {
            _data = data as UnitData;
        }

        T IUnitWithData.ReadUnitData<T>()
        {
            return _data as T;
        }
    }
}
