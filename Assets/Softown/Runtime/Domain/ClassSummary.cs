using System;
using static System.Reflection.BindingFlags;

namespace Softown.Runtime.Domain
{
    public class ClassSummary
    {
        public int PublicMethods { get; }
        public int Properties { get; }

        public ClassSummary(Type type)
        {
            Properties = type.GetProperties(Public | Instance | DeclaredOnly).Length;
            PublicMethods = type.GetMethods(Public | Instance | DeclaredOnly).Length;
            PublicMethods -= Properties * 2;
        }
    }
}