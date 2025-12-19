using System.Runtime.CompilerServices;

namespace ApiPagination.Tests;

public class Config
{
    [Fact]
    public void VerifyTestChecks() =>
        VerifyChecks.Run();
}

public static class StaticSettingsUsage
{
    public static VerifySettings VerifySettings { get; private set; }

    [ModuleInitializer]
    public static void Config()
    {
        VerifySettings = new VerifySettings();
        VerifySettings.DisableDiff();
     
        VerifierSettings.DisableRequireUniquePrefix();
        const string snapshotsDirectory = "__spnashots__";
        Verifier.DerivePathInfo(((file, directory, type, method) =>
        {
            return new PathInfo(
                Path.Join(directory, snapshotsDirectory),
                type.ToString(),
                method.Name);
        }));
    }
}
