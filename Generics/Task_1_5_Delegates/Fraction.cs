namespace Task1_5_Delegates
{
    internal class Fraction
    {
        public int A { get; set; }
        public int B { get; set; }
        public Fraction(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
        public override string ToString() => A + " / " + B;
    }
}
