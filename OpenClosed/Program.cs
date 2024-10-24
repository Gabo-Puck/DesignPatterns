namespace OpenClosed
{
    public enum Color
    {
        Green,
        Black,
        Orange,
    }

    public enum Size
    {
        Small,
        Medium,
        Large,
    }

    public class Product
    {
        public string Name = default!;
        public Color Color;
        public Size Size;

        public Product(string Name, Color Color, Size Size)
        {
            this.Name = Name;
            this.Color = Color;
            this.Size = Size;
        }
    }
    //Abstract logic into common interfaces that are open to extension, but closed to modification
    public interface ISpecification<T>
    {
        bool IsSatisfied(T value);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> values, ISpecification<T> specification);
    }

    //Create specific implementation for every use case
    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product value)
        {
            return _color == value.Color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product value)
        {
            return _size == value.Size;
        }
    }
    //The ideal is to add more classes rather than modify what you already have working
    public class AndSpecification<T> : ISpecification<T>{
        private readonly ISpecification<T> _firstSpecification;
        private readonly ISpecification<T> _secondSpecification;
        public AndSpecification(ISpecification<T> _firstSpecification, ISpecification<T> _secondSpecification){
            this._firstSpecification = _firstSpecification;
            this._secondSpecification = _secondSpecification;
        }

        public bool IsSatisfied(T value)
        {
            return _firstSpecification.IsSatisfied(value) && _secondSpecification.IsSatisfied(value);
        }
    }
    public class FilterProducts : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> values, ISpecification<Product> specification)
        {
            foreach (var p in values)
            {
                if (specification.IsSatisfied(p))
                    yield return p;
            }
        }
    }

    /*
    -- This is an example of the exact same functionality that violates Open-Closed principle,
    -- In order to add more filters, you have to modify "ProductFilter"
    public class ProductFilter
      {
        // let's suppose we don't want ad-hoc queries on products
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
          foreach (var p in products)
            if (p.Color == color)
              yield return p;
        }

        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
          foreach (var p in products)
            if (p.Size == size)
              yield return p;
        }

        public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
          foreach (var p in products)
            if (p.Size == size && p.Color == color)
              yield return p;
        } // state space explosion
          // 3 criteria = 7 methods

        // OCP = open for extension but closed for modification
      }
    */

    public class Program
    {
        public static int Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var orange = new Product("Orange", Color.Orange, Size.Small);
            var car = new Product("Car", Color.Black, Size.Large);
            var products = new List<Product>() { apple, orange, car };

            var filter = new FilterProducts();

            Console.WriteLine("Small products");
            var smallProductSpecification = new SizeSpecification(Size.Small);
            foreach (var item in filter.Filter(products, smallProductSpecification))
            {
                Console.WriteLine($"- {item.Name}");
            }

            Console.WriteLine("Green products");
            var greenProductSpecification = new ColorSpecification(Color.Green);
            foreach (var item in filter.Filter(products, greenProductSpecification))
            {
                Console.WriteLine($"- {item.Name}");
            }

            Console.WriteLine("Small and green products");
            var smallAndGreenSpecification = new AndSpecification<Product>(smallProductSpecification, greenProductSpecification);
            foreach (var item in filter.Filter(products, smallAndGreenSpecification))
            {
                Console.WriteLine($"- {item.Name}");
            }
            return 0;
        }
    }
}
