namespace DataSetTest.Models
{
    public class Product
    {
        private  static int _index = 0;
        private string _name;

        public int Id_product { get { return _index += 1; } }
        public string Name {
            get {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _name = string.Join("", "Item", _index.ToString());
                }

                return _name;
            } }
    }
}