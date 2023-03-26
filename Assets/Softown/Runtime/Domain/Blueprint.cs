using Castle.Core.Internal;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public readonly struct Blueprint
    {
        public string BuildingName { get; }
        public int Floors { get; }
        public int FoundationsWidth { get; }

        public Blueprint(int floors, int foundationsWidth) : this("unnamed", floors, foundationsWidth) { }

        public Blueprint(string buildingName, int floors, int foundationsWidth)
        {
            Assert.IsFalse(buildingName.IsNullOrEmpty());
            Assert.IsTrue(floors >= 0);
            Assert.IsTrue(foundationsWidth >= 0);

            this.BuildingName = buildingName;
            this.Floors = floors;
            this.FoundationsWidth = 1 + foundationsWidth;
        }

        public static Blueprint Blank => new();
    }
}