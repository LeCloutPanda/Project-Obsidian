using System;
using System.Linq;
using System.Reflection;
using BaseX;
using FrooxEngine;
using FrooxEngine.LogiX.Data;
using Obsidian.Shaders;
using Obsidian.Hardware;

namespace Obsidian
{
    [ImplementableClass(true)]
    internal class ExecutionHook
    {
#pragma warning disable CS0169
        private static Type? __connectorType;
        private static Type? __connectorTypes;
#pragma warning restore CS0169

        static ExecutionHook()
        {
            try
            {
                Engine.Current.OnReady += () =>
                {
                    ShaderInjection.AppendShaders();
                    //DriverInjector.InitializeHardwareClasses();
                    //QuantityInjector.Inject();
                };
            }
            catch (Exception e)
            {
                UniLog.Log($"Thrown Exception \n{e}");
            }
        }

        private static DummyConnector InstantiateConnector() => new();

        private class DummyConnector : IConnector
        {
            public IImplementable? Owner { get; private set; }

            public void ApplyChanges()
            {
            }

            public void AssignOwner(IImplementable owner) => Owner = owner;

            public void Destroy(bool destroyingWorld)
            {
            }

            public void Initialize()
            {
            }

            public void RemoveOwner() => Owner = null;
        }
    }
    internal static class AttributeInjector
    {
        private static FieldInfo typeInfo =
            typeof(GenericTypes).GetField("types", BindingFlags.NonPublic | BindingFlags.Instance);
        public static void Inject()
        {
            var attribute = typeof(ReadDynamicVariable<>).GetCustomAttribute<GenericTypes>(false, false);
            var typesObject = (Type[]) typeInfo.GetValue(attribute);
            var list = typesObject.ToList();
            list.Add(typeof(SyncFieldList<>));
            var array = list.ToArray();
            typeInfo.SetValue(typesObject, array);
            UniLog.Log("hooked");
        }
    }
}