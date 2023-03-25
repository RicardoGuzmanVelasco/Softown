namespace Softown.Runtime.Domain
{
    public struct Blueprint
    {
        public readonly int floors;
        public readonly int foundationsWidth;
        
        public Blueprint(int floors, int foundationsWidth)
        {
            this.floors = floors;
            this.foundationsWidth = foundationsWidth;
        }
    }
}