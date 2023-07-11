namespace Projektni1Arhitekture
{
    internal class Memorija
    {
        public string adresa;
        public long podaci;
        public int velicina;

        private static long adressAllocationPoint = 0;

        public Memorija()
        {
            adresa = "0x" + $"{adressAllocationPoint}";
            podaci = 0;
            velicina = 64;
            adressAllocationPoint += 1;
        }
    }
}