namespace Test.Domain.Extensions;

public static class GenericTypeExtensions
{
    public static string GetGenericTypeName(this Type type)
    {
        string empty = string.Empty;
        string genericTypeName;
        if (type.IsGenericType)
        {
            string str = string.Join(",", ((IEnumerable<Type>) type.GetGenericArguments()).Select<Type, string>((Func<Type, string>) (t => t.Name)).ToArray<string>());
            genericTypeName = type.Name.Remove(type.Name.IndexOf('`')) + "<" + str + ">";
        }
        else
            genericTypeName = type.Name;
        return genericTypeName;
    }

    public static string GetGenericTypeName(this object @object) => GenericTypeExtensions.GetGenericTypeName(@object.GetType());
}