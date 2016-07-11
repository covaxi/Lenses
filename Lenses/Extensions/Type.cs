using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lenses.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAnonymous(this Type type)
        {
            // FROM http://stackoverflow.com/questions/2483023/how-to-test-if-a-type-is-anonymous
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>", StringComparison.InvariantCultureIgnoreCase) || type.Name.StartsWith("VB$", StringComparison.InvariantCultureIgnoreCase))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
                && type.GetConstructors().Length == 1;
        }

        public static T With<T, TProp>(this T obj, Expression<Func<T, TProp>> getter, TProp value)
        {
            var type = obj.GetType();
            var properties = type.GetProperties().ToDictionary(p => p.Name);
            var name = ((MemberExpression)getter.Body).Member.Name;
            if (type.IsAnonymous()) 
            {
                var constructor = type.GetConstructors()[0];
                var parameters = constructor.GetParameters().Select(
                    p => p.Name == name ? value : properties[p.Name].GetMethod.Invoke(obj, new object[0])).ToArray();
                return (T)constructor.Invoke(parameters);
            }

            var result = (T)type.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
            foreach(var prop in type.GetProperties()) {
                prop.SetMethod.Invoke(result, new[] { prop.Name == name ? value : prop.GetMethod.Invoke(obj, new object[0]) });
            }

            return result;
        }
    }
}
