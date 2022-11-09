using System.Text.Json;

namespace LINQ_Bestellungen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonArtikel = @"C:\Users\ita2-TN16\Downloads\alleArtikel.json";
            string jsonBestellungen = @"C:\Users\ita2-TN16\Downloads\bestellung.json";
            FileStream filestreamArtikel = new FileStream(jsonArtikel, FileMode.Open);
            FileStream filestreamBestellung = new FileStream(jsonBestellungen, FileMode.Open);
            List<Artikel> artikel = JsonSerializer.Deserialize<List<Artikel>>(filestreamArtikel);
            Bestellung bestellung = JsonSerializer.Deserialize<Bestellung>(filestreamBestellung);

            var Iwas = from a in artikel
                       join p in bestellung.AllePositionen
                       on a.Artikelnummer equals p.Artikelnummer
                       select new
                       {
                           a.Artikelnummer,
                           a.Name,
                           p.Anzahl,
                           summe = p.Anzahl * a.Preis
                       };

            double summe = 0;
            foreach (var item in Iwas)
            {
                //Console.WriteLine(item.Artikelnummer+" "+item.Name+"\nAnzahl:  "+item.Anzahl +"\nSumme: "+item.summe);
                Console.WriteLine($"Artikelnummer: {item.Artikelnummer, -10} Atrikelname: {item.Name,-40}  Anzahl:  {item.Anzahl,-2} Summe: {item.summe, 5}");
                
                summe += item.summe;
            }
            Console.WriteLine("Gesamtsumme:  "+summe);
            //foreach (var item in artikel)
            //{
                
            //    Console.WriteLine(item.Artikelnummer+" "+item.Name);
            //}
            Console.ReadLine();
        }
    }
}