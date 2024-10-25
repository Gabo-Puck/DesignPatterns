public class Program
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle() { }

        public Rectangle(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public Square()
        {
        }

        public Square(int Width, int Height)
            : base(Width, Height) { }

        /**
        In the following methods, if you replace "override" with "new" you get a completly different behaviour:
        - override 
            completly replaces the base class behaviour when you got a reference to "Square" object
        - new 
            functions are only used with a reference to the square
        The word reference is really important in this context:
            Rectangle square1 = new Rectangle();
            square1.Width = 10;
            Rectangle square2 = new Square();
            square2.Width = 10;
        Both declarations are perfectly valid in C#. Despite being both used as rectangles (ie they reference a Rectangle),
        "square1" is being instantiated as Rectangle while "square2" is instantiated as a Square.
        This allows to some interesting behaviour as mentioned earlier.
        If you used the keyword "new", when you assign property "Width" both objects are going to use
        the setter from Rectangle
        Now, If you used the keyword "override", square1 is going to use the setter from Rectangle and
        square2 is going to use the setter from Square.

        Now, using new keyword in this context violates the Liskov Substitution Principle. 
        Why...?
        Well, this principle says that a base class (Reactangle) should be able to be replaced by a subtype class (Square)
        and should still behave as the subtype class, but in this cases both setters have a very different functionality
        */
        public override int Width
        {
            set { base.Height = base.Width = value; }
        }
        public override int Height
        {
            set { base.Height = base.Width = value; }
        }
    }

    public static int Area(Rectangle rectangle) => rectangle.Width * rectangle.Height;

    public static int Main(string[] args)
    {
        var rectangle = new Rectangle(10, 5);
        Console.WriteLine($"{rectangle}, Area: {Area(rectangle)}");

        Rectangle square = new Square();
        square.Height = 10;
        Console.WriteLine($"{square}, Area: {Area(square)}");
        return 0;
    }
}
