using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;

namespace RESTCalculatorConsumer
{
    public enum CalculationType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    class Program
    {
        //HUSK "http://" foran URI!
        private const string CalculatorUri = "http://localhost:59226/api/Calculator/";

        //Consumer virker ikke ved almindelig start, medmindre du laver en pause.
        //Start service individuelt via Debug > New Instance først, derefter consumer på samme måde.
        static void Main(string[] args)
        {
            //Brug af async metode
            Data d0 = new Data(25, 7);
            Console.WriteLine(Async(d0, CalculationType.Add));

            //VIGTIGT VED BRUG AF METODE UDEN ASYNC!!!
            //Giv REST tid til at starte op, ellers skal de startes individuelt.
            Console.WriteLine("Wait for the server to wake up.");
            Console.ReadLine();

            Data d = new Data(25, 7);
            Console.WriteLine(NotAsync(d, CalculationType.Add));
            
            Data d2 = new Data(7, 5);
            Console.WriteLine(NotAsync(d2, CalculationType.Subtract));

            Data d3 = new Data(20, 4);
            Console.WriteLine(NotAsync(d3, CalculationType.Multiply));

            Data d4 = new Data(30, 3);
            Console.WriteLine(NotAsync(d4, CalculationType.Divide));

            Console.ReadLine();
        }

        //Metode uden async, kræver ventetid ved almindelig start.
        public static string NotAsync(Data data, CalculationType type)
        {
            using (HttpClient client = new HttpClient())
            {
                //Vores data bliver skrevet ud så vi kan se hvad vi arbejder med.
                Console.WriteLine("Data " + data);
                //Vores data serialiseres til json.
                var jsonString = JsonConvert.SerializeObject(data);
                //Vores json string bliver skrevet ud.
                Console.WriteLine("json string: " + jsonString);
                //Vi gør vores jsonstring klar til at blive sendt afsted.
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                //Vores content bliver skrevet ud med hjælp fra ToString().
                Console.WriteLine("content: : " + content.ToString());
                //Vores URI bliver skrevet ud.
                Console.WriteLine("CalculatorUri: " + CalculatorUri);
                //Vores content bliver nu sendt afsted med Post, samt bliver vores metode type tilføjet til vores URI.
                HttpResponseMessage response = client.PostAsync(CalculatorUri + type, content).Result;
                //Vi afventer svar for at få vores content til en string.
                string str = response.Content.ReadAsStringAsync().Result;
                //Vi deserialiserer vores str, så den ikke er json mere.
                Int32 sumStr = JsonConvert.DeserializeObject<Int32>(str);
                //Vi returnerer vores str, eller sumStr
                return str;
            }
        }

        //Async metode brug
        public static async Task<string> Async(Data data, CalculationType type)
        {
            using (HttpClient client = new HttpClient())
            {
                //Vores data bliver skrevet ud så vi kan se hvad vi arbejder med.
                Console.WriteLine("Data " + data);
                //Vores data serialiseres til json.
                var jsonString = JsonConvert.SerializeObject(data);
                //Vores json string bliver skrevet ud.
                Console.WriteLine("json string: " + jsonString);
                //Vi gør vores jsonstring klar til at blive sendt afsted.
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                //Vores content bliver skrevet ud med hjælp fra ToString().
                Console.WriteLine("content: : " + content.ToString());
                //Vores URI bliver skrevet ud.
                Console.WriteLine("CalculatorUri: " + CalculatorUri);
                //Vores content bliver nu sendt afsted med Post, samt bliver "Add" tilføjet til vores URI.
                HttpResponseMessage response = await client.PostAsync(CalculatorUri + type, content);
                //Vi afventer svar for at få vores content til en string.
                string str = await response.Content.ReadAsStringAsync();
                //Vi deserialiserer vores str, så den ikke er json mere.
                Int32 sumStr = JsonConvert.DeserializeObject<Int32>(str);
                //Vi returnerer vores str, eller sumStr
                return str;
            }
        }
    }
}
