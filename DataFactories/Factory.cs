using DataStuff;
using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public interface IProduct
    {
        void Initialize();
    }
    public abstract class Factory
    {
        public abstract IProduct CreateProduct(DataArgs data);

    }
    public static class FactoryManager
    {
        static Dictionary<Type, Factory> _factories = new Dictionary<Type, Factory>();
        public static void AddFactory(Type type, Factory factory)
        {
            if (!_factories.ContainsKey(type))
            {
                _factories.Add(type, factory);
            }
        }

        public static Factory GetFactory(Type type)
        {
            if (_factories.ContainsKey(type))
            {
                return _factories[type];
            }
            _factories.Add(type, (Factory)Activator.CreateInstance(type));
            return _factories[type];
        }
    }
}