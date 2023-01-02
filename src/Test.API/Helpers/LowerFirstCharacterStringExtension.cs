namespace Test.API.Helpers;

public static class LowerFirstCharacterStringExtension
{
    public static string FirstCharToLower(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToLower(str[0]) + str.Substring(1);
    }
}