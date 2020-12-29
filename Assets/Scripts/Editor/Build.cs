using UnityEditor;
using UnityEngine;

public class BuildScript
{
    static string name = Application.productName.Replace(" ", "");

    [MenuItem("Build/Build Linux Server")]
    public static void BuildLinuxServer()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" };
        buildPlayerOptions.locationPathName = "Builds/Linux/Server/" + name + ".Server.x86_64";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC | BuildOptions.EnableHeadlessMode;

        Debug.Log("Building Linux Server...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Linux Server Done!");
    }

    [MenuItem("Build/Build Linux Client")]
    public static void BuildLinuxClient()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" };
        buildPlayerOptions.locationPathName = "Builds/Linux/" + name + "/" + name + ".x86_64";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;

        Debug.Log("Building Linux Client...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Linux Client Done!");
    }

    [MenuItem("Build/Build Windows Client")]
    public static void BuildWindowsClient()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" };
        buildPlayerOptions.locationPathName = "Builds/Windows/" + name + "/" + name + ".exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;

        Debug.Log("Building Windows Client...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Windows Client Done!");
    }

    [MenuItem("Build/Build All")]
    public static void BuildAll()
    {
        BuildLinuxServer();
        BuildLinuxClient();
        BuildWindowsClient();
    }

}