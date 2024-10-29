public class Program{
    public enum RelationShip{
        Parent,
        Children,
        Sibling
    }
    public class Person{
        public string Name;

        public Person(string name)
        {
            Name = name;
        }
    }
    /**
    Dependency inversion principle states that:
        High levels parts of the system should not depend on low level parts of the system directly,
        it should depend on an abstraction
    
    public class RelationShips{
        private List<(Person, RelationShip, Person)> _relationships = new();
        public void AddParent(Person parent, Person child){
            _relationships.Add((parent, RelationShip.Parent, child));
        }
        public List<(Person, RelationShip, Person)> Relations => _relationships;
    }

    public class Printer{
        public void Print(RelationShips relationShips){
            foreach (var person in relationShips.Relations.Where((person)=>
                person.Item2 == RelationShip.Parent &&
                person.Item1.Name == "John"
            ))
            {
                Console.WriteLine($"John has a child named: {person.Item3.Name}");
            }
        }
    }
    
    In this example, the biggest issue is that "Printer" depends on how "RelationShips" store the relations
    internally, when in most cases it should expose the members through an interface.

    */


    /*
    Now, if we use an abstraction that finds all the children for a particular parent, we can decouple
    how relationships are stored from how normally clients would consume the relationship data
    */
    public interface IRelationShipBrowser{
        public IEnumerable<Person> FindAllChildren(string parentName);
    }

    public class RelationShips : IRelationShipBrowser{
        private List<(Person, RelationShip, Person)> _relationships = new();
        public void AddParent(Person parent, Person child){
            _relationships.Add((parent, RelationShip.Parent, child));
        }

        public IEnumerable<Person> FindAllChildren(string parentName)
        {
            return _relationships.Where((person)=>
                person.Item2 == RelationShip.Parent &&
                person.Item1.Name == "John").Select((item) => item.Item3);
        }
    }

    public class Printer{
        public void Print(RelationShips relationShips){
            foreach (var person in relationShips.FindAllChildren("John"))
            {
                Console.WriteLine($"John has a child named: {person.Name}");
            }
        }
    }

    public static void Main(string[] args)
    {
        var parent = new Person("John");
        var child1 = new Person("Emma");
        var child2 = new Person("Ivan");
        var relationShips = new RelationShips();
        relationShips.AddParent(parent,child1);
        relationShips.AddParent(parent,child2);

        var printer = new Printer();
        printer.Print(relationShips);
    }
}