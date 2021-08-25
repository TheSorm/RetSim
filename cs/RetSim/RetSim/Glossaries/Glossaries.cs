namespace RetSim
{
    public static partial class Glossaries
    {
        public static void Initialize()
        {
            Auras.Initialize();
            Spells.Initialize();
            Procs.Initialize();
            Auras.AddProcs();
        }
    }
}