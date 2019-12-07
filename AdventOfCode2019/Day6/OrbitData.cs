namespace AdventOfCode2019.Day6
{
    public class OrbitData
    {
        public string Centre;
        public string Orbiter;

        public OrbitData(string mapData)
        {
            var parts = mapData.Split(')');
            Centre = parts[0];
            Orbiter = parts[1];
        }
    }
}
