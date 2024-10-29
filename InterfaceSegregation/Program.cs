public class Program
{
    public class Document() { }


    // public interface IMultiFunctionalDevice
    // {
    //     void Print(Document document);
    //     void Fax(Document document);
    //     void Scan(Document document);
    // }

    // public class MultiFunctionalDevice : IMultiFunctionalDevice
    // {
    //     public void Fax(Document document)
    //     {
    //         Console.WriteLine("Fax... Done!");
    //     }

    //     public void Print(Document document)
    //     {
    //         Console.WriteLine("Print... Done!");
    //     }

    //     public void Scan(Document document)
    //     {
    //         Console.WriteLine("Scan... Done!");
    //     }
    // }

    // public class BasicPrinter : IMultiFunctionalDevice
    // {
    //     //implementing innecessary methdods, violation of Interface Segregation Principle
    //     //create smaller and specific interfaces
    //     public void Fax(Document document)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public void Print(Document document)
    //     {
    //         Console.WriteLine("Print basic... Done!");
    //     }

    //     public void Scan(Document document)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }

    //It's better to create specific interfaces for every behaviour

    public interface IPrinter{
        void Print(Document document);
    }
    
    public interface IScanner{
        void Scan(Document document);
    }
    
    public interface IFax{
        void Fax(Document document);
    }

    //You can even compose simpler interfaces into more complex ones
    public interface IMultiFunctionalDevice: IPrinter, IScanner, IFax{

    }

    //Every class implements the necessary methods
    public class ClassicPrinter : IPrinter
    {
        public void Print(Document document)
        {
            Console.WriteLine("Classic printing");
        }
    }

    //You can define classes that implements compund interfaces
    public class MultiFunctionalDevice : IMultiFunctionalDevice
    {
        private IPrinter _printer;
        private IFax _fax;
        private IScanner _scanner;
        public MultiFunctionalDevice(IPrinter printer, IFax fax, IScanner scanner)
        {
            _printer = printer;
            _fax = fax;
            _scanner = scanner;
        }

        //Decorators!!
        public void Fax(Document document)
        {
            _fax.Fax(document);
        }

        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Scan(Document document)
        {
            _scanner.Scan(document);
        }
    }

    public static void Main(string[] args) { }
}
