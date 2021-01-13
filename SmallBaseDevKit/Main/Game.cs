using SmallBaseDevKit.Main;
using SmallBaseDevKit.GameModule;
using SmallBaseDevKit.USH.Unit;

namespace SmallBaseDevKit
{
    /// <summary>
    /// Класс для реализации быстрых команд.
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// Метод по созданию нового игрового юнита с учетом его настроек.
        /// </summary>
        /// <typeparam name="T">Тип юнита.</typeparam>
        /// <returns>Реализация юнита.</returns>
        public static T CreateUnit<T>() where T: IUnit
        {
            var unit = GameInstance.Instance.GetGameModule<UnitModule>().GetUnit<T>();
            unit.CreateUnit();
            return unit;
        }

        public static void DestroyUnit<U>(U unit) where U : IUnit
        {
            var i_unit = (IUnit)unit;
            i_unit.DestroyUnit();
        }
    }
}
