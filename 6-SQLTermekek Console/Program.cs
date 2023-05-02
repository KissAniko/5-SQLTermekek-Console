using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_SQLTermekek_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kérem a kategóriát");
            string kategoria = Console.ReadLine();

            MySqlConnection adatbazisKapcsolat = new MySqlConnection("datasource = 127.0.0.1; " +
                "port = 3306; database = hardver; username = root; password =;");
            adatbazisKapcsolat.Open();

            String SQLSelect = "SELECT Gyártó, " +
                "COUNT(*) as darabSzám, " +
                "MAX(Ár) as maxÁr, " +
                "AVG(Ár) as átlagÁr " +
                "FROM termékek " +
                $" WHERE kategória = '{kategoria}'" +
                " GROUP BY Gyártó;";

           MySqlCommand SQLParancs = new MySqlCommand(SQLSelect, adatbazisKapcsolat);
            MySqlDataReader eredmenytOlvaso = SQLParancs.ExecuteReader();

            while (eredmenytOlvaso.Read())
            {
                Console.Write(eredmenytOlvaso.GetString("Gyártó").PadRight(30, '.'));
                Console.Write(eredmenytOlvaso.GetString("darabSzám").PadLeft(4, '_') +"db");
                Console.Write(eredmenytOlvaso.GetString("maxÁr").PadLeft(20) + "Ft");
                string atlagAr = $"{eredmenytOlvaso.GetDouble("átlagÁr"):f1}";
                Console.WriteLine(atlagAr.PadLeft(15) + "Ft");
            }


           eredmenytOlvaso.Close();
            adatbazisKapcsolat.Close();
        }
    }
}
