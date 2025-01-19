using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace reflections
{
    internal class PropertyHelper
    {
        // let's make caching for this delegates to avoid creating it from scrath 
        private static readonly Dictionary<string, Delegate> cache = new ();


        private static readonly MethodInfo WrapPropertyMethodType = typeof(PropertyHelper)
            .GetMethod(nameof(WrapProperty));

        public static Func<object, TResult> MakePropertyFast<TResult>(PropertyInfo property)
        {
            var method = property.GetMethod;
            var declaringType = property.DeclaringType;

            Delegate methodDelegate = null;

            //part of caching
            if (!cache.ContainsKey(method.Name))
            {
                var genericDelegate = typeof(Func<,>).MakeGenericType(new Type[] { declaringType, typeof(TResult) });
                methodDelegate = method.CreateDelegate(genericDelegate);
                cache.Add(method.Name, methodDelegate);
            }
            else
                methodDelegate = cache[method.Name];

            // this the new part 
            // this like WrapProperty<TClass, TResult> 
            var WrapPropertyDelegate = WrapPropertyMethodType.MakeGenericMethod(declaringType, typeof(TResult)); // this like creating generic type of delegate but this for method 


            var result = WrapPropertyDelegate.Invoke(null, new[] { methodDelegate }); // this to pass the paramter he want 

            return (Func<object, TResult>)result;
        }
        public static Func<object, TResult> WrapProperty<TClass, TResult>(Func<TClass, TResult> del)
                => instance => del((TClass)instance);
    }
}
