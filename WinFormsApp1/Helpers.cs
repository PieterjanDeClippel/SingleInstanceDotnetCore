using System.Text.RegularExpressions;

namespace WinFormsApp1;

internal static partial class Helpers
{
    [GeneratedRegex(@"ClickOnce_ActivationData_[0-9]+")]
    private static partial Regex FileRegex();

    internal static IEnumerable<string> GetLaunchFiles()
    {
        return Environment.GetEnvironmentVariables().AsDictionary()
            .Where(v => FileRegex().IsMatch(v.Key))
            .Select(v => v.Value)
            .OfType<string>();
    }
}
