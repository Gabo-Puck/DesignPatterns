using System.Security.Cryptography.X509Certificates;

namespace DesignPatterns.Solid{
    public class Journal{
        private List<string> entries = new List<string>();
        public int AddEntry(string entry){
            entries.Add(entry);
            return entries.Count();
        }
        public void RemoveEntry(int index){
            entries.RemoveAt(index);
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }
    
    public class Persistance{
        public void SaveToFile(string filename, Journal journal, bool overwrite = false){
            if(overwrite || !File.Exists(filename)){
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }
    public class Demo{
        static void Main(string[] args){
            var journal = new Journal();
            journal.AddEntry("I started my course today");
            journal.AddEntry("I walked 10k steps");
            var persistance = new Persistance();
            var filePath = @"C:\Users\PC\Code\DesignPatterns\Solid\JournalEntry.txt";
            persistance.SaveToFile(filePath, journal, true);
        }
    }
}