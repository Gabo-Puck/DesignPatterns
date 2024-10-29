using System.Text;

public class Program
{
    public class HtmlElement
    {
        public string Name;
        public string Text;
        private const int indentationSpaces = 2;
        public List<HtmlElement> elements = new();

        public HtmlElement()
        {
            Name = string.Empty;
            Text = string.Empty;
        }

        public HtmlElement(string name, string text)
        {
            Text = text;
            Name = name;
        }

        private string ToStringImp(int indentationLevel)
        {
            var sb = new StringBuilder();
            var indent = new string(' ', indentationLevel * indentationSpaces);
            sb.AppendLine($"{indent}<{Name}>");
                if (!string.IsNullOrEmpty(Text)) { 
                    sb.Append(' ', indentationLevel * indentationSpaces + 1);
                    sb.AppendLine(Text);
                }
                elements.ForEach(e => sb.AppendLine(e.ToStringImp(indentationLevel + 1)));
            sb.AppendLine($"{indent}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImp(0);
        }
    }

    public class HtmlBuilder
    {
        public HtmlElement rootElement = new HtmlElement();

        public HtmlBuilder(string name)
        {
            rootElement.Name = name;
        }

        public void AddHtmlElement(string childName, string childText)
        {
            var htmlElement = new HtmlElement(childName, childText);
            rootElement.elements.Add(htmlElement);
        }

        public override string ToString()
        {
            return rootElement.ToString();
        }
    }

    public static void Main(string[] args)
    {
        var sb = new StringBuilder();
        sb.Append("<p>").Append("Hello word");
        sb.Append("</p>");
        Console.WriteLine(sb);
        sb.Clear();
        var texts = new List<string>() { "Hello", "World" };
        sb.Append("<ul>");
        texts.ForEach(t => sb.Append($"<li>{t}</li>"));
        sb.Append("</ul>");
        Console.WriteLine(sb);
        var htmlBuilder = new HtmlBuilder("li");
        htmlBuilder.AddHtmlElement("li", "Hello");
        htmlBuilder.AddHtmlElement("li", "world!");
        Console.WriteLine(htmlBuilder);
    }
}
