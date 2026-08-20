#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using _Project.Scripts.Gameplay.Core.EntitiesCore;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor
{
    [InitializeOnLoad]
    public static class EntityComponentGenerator
    {
        private const string OutputFile =
            "Assets/_Project/Scripts/Gameplay/Core/EntitiesCore/EntityPartial.cs";

        private const string EntityNamespace =
            "_Project.Scripts.Gameplay.Core.EntitiesCore";

        static EntityComponentGenerator()
        {
            EditorApplication.delayCall += Generate;
        }

        [MenuItem("Tools/Entities/Generate Entity")]
        public static void Generate()
        {
            var componentTypes = FindComponentTypes();

            if (componentTypes.Count == 0)
            {
                Debug.LogWarning(
                    "[EntityComponentGenerator] No IEntityComponent implementations found.");

                return;
            }

            var code = GenerateCode(componentTypes);

            var directory = Path.GetDirectoryName(OutputFile);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(OutputFile))
            {
                var oldCode = File.ReadAllText(OutputFile);

                if (oldCode == code)
                    return;
            }

            File.WriteAllText(
                OutputFile,
                code,
                new UTF8Encoding(false));

            AssetDatabase.ImportAsset(OutputFile);

            Debug.Log(
                $"[EntityComponentGenerator] Generated {OutputFile} " +
                $"from {componentTypes.Count} components.");
        }

        private static List<Type> FindComponentTypes()
        {
            return TypeCache
                .GetTypesDerivedFrom<IEntityComponent>()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    !type.IsGenericType)
                .OrderBy(type => type.Name)
                .ToList();
        }

        private static string GenerateCode(
            IReadOnlyList<Type> componentTypes)
        {
            var usings = CollectUsings(componentTypes);

            var sb = new StringBuilder();

            foreach (var ns in usings)
                sb.AppendLine($"using {ns};");

            sb.AppendLine();

            sb.AppendLine($"namespace {EntityNamespace}");
            sb.AppendLine("{");

            sb.AppendLine("    public partial class Entity");
            sb.AppendLine("    {");

            foreach (var componentType in componentTypes)
                GenerateComponent(sb, componentType);

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static SortedSet<string> CollectUsings(
            IReadOnlyList<Type> componentTypes)
        {
            var usings = new SortedSet<string>();

            foreach (var componentType in componentTypes)
            {
                CollectTypeNamespaces(
                    componentType,
                    usings);

                var valueType = GetValueType(componentType);

                if (valueType != null)
                {
                    CollectTypeNamespaces(
                        valueType,
                        usings);
                }
            }

            usings.Remove(EntityNamespace);

            return usings;
        }

        private static void CollectTypeNamespaces(
            Type type,
            ISet<string> namespaces)
        {
            if (type == null)
                return;

            if (!string.IsNullOrEmpty(type.Namespace))
                namespaces.Add(type.Namespace);

            if (type.IsGenericType)
            {
                foreach (var argument in type.GetGenericArguments())
                {
                    CollectTypeNamespaces(
                        argument,
                        namespaces);
                }
            }

            if (type.IsArray)
            {
                CollectTypeNamespaces(
                    type.GetElementType()!,
                    namespaces);
            }
        }

        private static void GenerateComponent(
            StringBuilder sb,
            Type componentType)
        {
            var componentName = componentType.Name;

            var valueType = GetValueType(componentType);

            if (valueType == null)
            {
                Debug.LogWarning(
                    $"[EntityComponentGenerator] " +
                    $"{componentName} does not have a public Value field/property. " +
                    $"Skipping.");

                return;
            }

            var valueTypeName = GetFriendlyTypeName(valueType);

            sb.AppendLine();

            sb.AppendLine(
                $"        public {componentName} {componentName}C => " +
                $"GetComponent<{componentName}>();");

            sb.AppendLine(
                $"        public {valueTypeName} {componentName} => " +
                $"{componentName}C.Value;");

            sb.AppendLine();

            sb.AppendLine(
                $"        public Entity Add{componentName}({valueTypeName} value)");

            sb.AppendLine(
                $"            => AddComponent(new {componentName}() {{ Value = value }});");
        }

        private static Type? GetValueType(
            Type componentType)
        {
            var field = componentType.GetField(
                "Value",
                BindingFlags.Instance |
                BindingFlags.Public);

            if (field != null)
                return field.FieldType;

            var property = componentType.GetProperty(
                "Value",
                BindingFlags.Instance |
                BindingFlags.Public);

            if (property != null)
                return property.PropertyType;

            return null;
        }

        private static string GetFriendlyTypeName(Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            var name = type.Name;

            var genericIndex = name.IndexOf('`');

            if (genericIndex >= 0)
                name = name.Substring(
                    0,
                    genericIndex);

            var arguments = type
                .GetGenericArguments()
                .Select(GetFriendlyTypeName);

            return $"{name}<{string.Join(", ", arguments)}>";
        }
    }
}

#endif