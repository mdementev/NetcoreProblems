namespace NetcoreProblems
{
    public class MyHashSet
    {
        private readonly List<int> _values;
        public MyHashSet()
        {
            _values = new List<int>();
        }

        public void Add(int key)
        {
            if (!_values.Contains(key))
            {
                _values.Add(key);
            }
        }

        public void Remove(int key)
        {
            if (_values.Contains(key))
            {
                _values.Remove(key);
            }
        }

        public bool Contains(int key)
        {
            return _values.Contains(key);
        }
    }
}