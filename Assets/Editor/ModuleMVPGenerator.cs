using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

namespace RogueLikeCardSystem
{
    public class ModuleGeneratorWindow : EditorWindow
    {
        private string moduleName = "NewModule";

        [MenuItem("Tools/Module Generator")]
        public static void ShowWindow() => GetWindow<ModuleGeneratorWindow>("Module Generator");

        void OnGUI()
        {
            GUILayout.Label("Generate MVP Module", EditorStyles.boldLabel);
            moduleName = EditorGUILayout.TextField("Module Name", moduleName);

            GUI.enabled = !string.IsNullOrWhiteSpace(moduleName);
            if (GUILayout.Button("Generate"))
            {
                GenerateModule(moduleName.Trim());
            }
            GUI.enabled = true;
        }

        // ──────────────────────────────────────────────────────────────────────────────
        #region Generation
        static readonly string[] rootFolders = { "Scripts", "Prefabs", "Scriptable Objects" };
        static readonly string[] scriptFolders = { "Manager", "Presenter", "Model", "View", "Action", "Scriptable Object" };

        private void GenerateModule(string name)
        {
            string basePath = Path.Combine("Assets/Game", name);
            CreateFolders(basePath);

            GenerateManagerFiles(basePath, name);
            GeneratePresenterFiles(basePath, name);
            GenerateSingleFile(basePath, name, "Model", $"{name}Model", ModelTemplate);
            GenerateSingleFile(basePath, name, "View", $"{name}View", ViewTemplate);
            GenerateSingleFile(basePath, name, "Action", $"{name}Action", ActionTemplate);
            GenerateSingleFile(basePath, name, "Action", $"{name}Event", EventTemplate);

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("Module Generator",
                $"Module \"{name}\" created under Assets/Game/{name}", "OK");
        }

        private static void CreateFolders(string basePath)
        {
            // Game/{ModuleName}/{RootFolders}
            foreach (var root in rootFolders)
            {
                string rootPath = Path.Combine(basePath, root);
                if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);

                // Under Scripts create sub‑folders
                if (root == "Scripts")
                {
                    foreach (var sub in scriptFolders)
                    {
                        string subPath = Path.Combine(rootPath, sub);
                        if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);
                    }
                }
            }
        }

        // ──────────────────────────────────────────────────────────────────────────────
        #region File generators
        private static void GenerateManagerFiles(string basePath, string name)
        {
            var folder = "Manager";
            var path = Path.Combine(basePath, "Scripts", folder);
            string iName = $"I{name}{folder}";
            string cName = $"{name}{folder}";

            WriteIfNotExists(Path.Combine(path, iName + ".cs"), InterfaceTemplate(iName, name, folder));
            WriteIfNotExists(Path.Combine(path, cName + ".cs"), ManagerTemplate(cName, iName, name, folder));
        }

        private static void GeneratePresenterFiles(string basePath, string name)
        {
            var folder = "Presenter";
            var path = Path.Combine(basePath, "Scripts", folder);
            string iName = $"I{name}{folder}";
            string cName = $"{name}{folder}";

            WriteIfNotExists(Path.Combine(path, iName + ".cs"), InterfaceTemplate(iName, name, folder));
            WriteIfNotExists(Path.Combine(path, cName + ".cs"), PresenterTemplate(cName, iName, name, folder));
        }

        private static void GenerateSingleFile(string basePath, string name, string folder,
                                               string className, System.Func<string, string, string, string> template)
        {
            var path = Path.Combine(basePath, "Scripts", folder, className + ".cs");
            WriteIfNotExists(path, template(className, name, folder));
        }

        private static void WriteIfNotExists(string filePath, string contents)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, contents, Encoding.UTF8);
                Debug.Log($"Created: {filePath}");
            }
            else
            {
                Debug.LogWarning($"Skipped (exists): {filePath}");
            }
        }
        #endregion
        #endregion

        // ──────────────────────────────────────────────────────────────────────────────
        #region Templates
        private static string Namespace(string module, string folder)
            => $"RoguelikeCardSystem.Game.{module}.{folder.Replace(" ", string.Empty)}";

        private static string InterfaceTemplate(string interfaceName, string module, string folder) =>
$@"namespace {Namespace(module, folder)}
{{
    public interface {interfaceName}
    {{
    }}
}}";

        private static string ManagerTemplate(string className, string interfaceName, string module, string folder) =>
$@"namespace {Namespace(module, folder)}
{{
    public class {className} : {interfaceName}
    {{
        // TODO: Implement manager logic here
    }}
}}";

        private static string PresenterTemplate(string className, string interfaceName, string module, string folder) =>
$@"namespace {Namespace(module, folder)}
{{
    public class {className} : {interfaceName}
    {{
        // TODO: Implement presenter logic here
    }}
}}";

        private static string ModelTemplate(string className, string module, string folder) =>
$@"namespace {Namespace(module, folder)}
{{
    public class {className}
    {{
        // TODO: Model data & logic
    }}
}}";

        private static string ViewTemplate(string className, string module, string folder) =>
$@"using UnityEngine;

namespace {Namespace(module, folder)}
{{
    public class {className} : MonoBehaviour
    {{
        // TODO: Bind UI / scene references here
    }}
}}";

        private static string ActionTemplate(string className, string module, string folder) =>
$@"namespace {Namespace(module, folder)}
{{
    public class {className}
    {{
        // TODO: Define action behaviour
    }}
}}";

        private static string EventTemplate(string className, string module, string folder) =>
$@"namespace {Namespace(module, folder)}
{{
    public struct {className}
    {{
        // TODO: Define action behaviour
    }}
}}";



        #endregion
    }
}