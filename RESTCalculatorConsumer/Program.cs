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
        private const string CalculatorUri = "http://localhost:59226/api/Calculator/";
        private CalculationType Type;

        //Consumer virker ikke ved almindelig start. Start service individuelt via Debug > New Instance først, derefter consumer.
        static void Main(string[] args)
        {
            Data d = new Data(25, 7);
            Console.WriteLine(Async(d, CalculationType.Add));
            
            Data d2 = new Data(7, 5);
            Console.WriteLine(Async(d2, CalculationType.Subtract));

            Data d3 = new Data(20, 4);
            Console.WriteLine(Async(d3, CalculationType.Multiply));

            Data d4 = new Data(30, 3);
            Console.WriteLine(Async(d4, CalculationType.Divide));

            Console.ReadLine();
        }

        public static string Async(Data data, CalculationType type)
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
                HttpResponseMessage response = client.PostAsync(CalculatorUri + type, content).Result;
                //Vi afventer svar for at få vores content til en string.
                string str = response.Content.ReadAsStringAsync().Result;
                //Vi deserialiserer vores str, så den ikke er json mere.
                Int32 sumStr = JsonConvert.DeserializeObject<Int32>(str);
                //Vi returnerer vores str, eller sumStr
                return str;
            }
        }

    }
}
