using System;
using static System.Reflection.BindingFlags;

namespace Softown.Runtime.Domain
{
    public class Class
    {
        public int PublicMethods { get; }
        public int Properties { get; }

        public Class(Type type)
        {
            PublicMethods = type.GetMethods(Public | Instance | DeclaredOnly).Length;
            Properties = type.GetProperties(Public | Instance | DeclaredOnly).Length;
        }
    }
}