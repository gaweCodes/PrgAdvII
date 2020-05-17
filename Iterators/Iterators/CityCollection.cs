using System.Collections.Generic;

namespace Iterators
{
    internal class CityCollection
    {
        private readonly string[] _cities = {"Bern", "Basel", "Zürich", "Rapperswil", "Genf"};
        public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>) _cities).GetEnumerator();
        public IEnumerable<string> Reverse
        {
            get
            {
                for (var i = _cities.Length - 1; i >= 0; i--) yield return _cities[i];
            }
        }
    }
}
