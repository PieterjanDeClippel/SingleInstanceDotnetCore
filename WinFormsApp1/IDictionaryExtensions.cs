namespace WinFormsApp1;

internal static class IDictionaryExtensions
{
    public static Dictionary<string, object> AsDictionary(this System.Collections.IDictionary dict)
    {
        var keys = new object[dict.Count];
        dict.Keys.CopyTo(keys, 0);

        return keys.ToDictionary(k => k.ToString()!, k => dict[k]!);
    }
}
