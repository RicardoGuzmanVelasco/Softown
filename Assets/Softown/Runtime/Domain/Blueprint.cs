using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public readonly struct Blueprint
    {
        public int Floors { get; }
        public int FoundationsWidth { get; }

        public Blueprint(int floors, int foundationsWidth)
        {
            Assert.IsTrue(floors >= 0);
            Assert.IsTrue(foundationsWidth >= 0);

            this.Floors = floors;
            this.FoundationsWidth = foundationsWidth + 1;
        }

        public static Blueprint Blank => new();
    }
}