using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebApp.Aids
{
    public static class CreateNew
    {
        public static T Instance<T>()
        {
            T function()
            {
                var type = typeof(T);
                var instance = Instance(type);
                var value = (T)instance;
                return value;
            }
            var def = default(T);
            var result = Safe.Run(function, def);
            return result;
        }
        public static object Instance(Type t)
        {
            return Safe.Run(() =>
            {
                var constructor = getFirstOrDefaultConstructorInfo(t);
                var parameters = constructor.GetParameters();
                var values = setRandomParameterValues(parameters);
                return invokeConstructor(constructor, values);
            }, null);
        }
#pragma warning disable IDE1006 // Naming Styles
        private static object invokeConstructor(ConstructorInfo ci, object[] values)
#pragma warning restore IDE1006 // Naming Styles
        {
            return values.Length == 0 ? ci.Invoke(null) : ci.Invoke(values);
        }
#pragma warning disable IDE1006 // Naming Styles
        private static object[] setRandomParameterValues(ParameterInfo[] parameters)
#pragma warning restore IDE1006 // Naming Styles
        {
            var values = new List<object>();
            foreach (var p in parameters)
            {
                var t = p.ParameterType;
                var value = GetRandom.Value(t);
                values.Add(value);
            }
            return values.ToArray();
        }
#pragma warning disable IDE1006 // Naming Styles
        private static ConstructorInfo getFirstOrDefaultConstructorInfo(Type t)
#pragma warning restore IDE1006 // Naming Styles
        {
            var constructors = t.GetConstructors();
            return constructors.Length == 0 ? null : constructors[0];
        }
    }
}
