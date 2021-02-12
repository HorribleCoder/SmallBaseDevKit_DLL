using System;
using System.Collections.Generic;
using System.Text;

namespace SmallBaseDevKit.USH.State
{
    public static class StateExtention
    {
        /// <summary>
        /// Конвертирвать текущее состояние в нужное.
        /// </summary>
        /// <typeparam name="T">Тип состояние в которое конвертируется.</typeparam>
        /// <param name="state">Текущее состояние.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this IState state) where T: IState
        {
            return (T)state;
        }
    }
}
