using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class PostBuildStep {
    // Set the IDFA request description:
    //const string k_TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";

#if UNITY_IOS
    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode) {
        if (buildTarget == BuildTarget.iOS) {
            AddPListValues(buildTarget, pathToXcode);
        }
    }

    // Implement a function to read and write values to the plist file:
    static void AddPListValues(BuildTarget buildTarget, string pathToXcode) {
        // Retrieve the plist file from the Xcode project directory:
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();


        // Read the values from the plist file:
        plistObj.ReadFromString(File.ReadAllText(plistPath));

        // Set values from the root object:
        PlistElementDict plistRoot = plistObj.root;

        // Set the description key-value in the plist:
        plistRoot.SetString("NSUserTrackingUsageDescription", "");

        // Save changes to the plist:
        File.WriteAllText(plistPath, plistObj.WriteToString());

        Dictionary<string, string> folders = new Dictionary<string, string>();
        List<string> localizeList = new List<string>();

        localizeList.Add("en.lproj");
        localizeList.Add("ko.lproj");
        localizeList.Add("ja.lproj");
        localizeList.Add("ru.lproj");
        localizeList.Add("zh-Hant.lproj");
        localizeList.Add("zh-Hans.lproj");
        foreach(string data in localizeList)
        {
            if (!Directory.Exists(".BuildOutput/XCode/" + data))
                Directory.CreateDirectory("./BuildOutput/Xcode/" + data);

            //FileUtil.CopyFileOrDirectory("./BuildOutput/iOS_Localize/" + data, "./BuildOutput/Xcode/" + data + "/InfoPlist.strings");
            
            folders.Add(data, "./BuildOutput/Xcode/");
        }
        PBXProject pBXProject = new PBXProject();
        pBXProject.ReadFromFile(pathToXcode + "/Unity-iPhone.xcodeproj/project.pbxproj");
        string targetGuid = pBXProject.TargetGuidByName("Unity-iPhone");
        foreach (KeyValuePair<string, string> folder in folders)
        {
            string guid = pBXProject.AddFolderReference(folder.Value + folder.Key, folder.Key, PBXSourceTree.Source);
            pBXProject.AddFileToBuild(targetGuid, guid);
        }
    }
#endif
}