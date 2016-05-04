using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// This test evaluates the 
    /// </summary>
    public class XmlReadingTest : ITest
    {
        public string Name { get { return "XML Reading Test";  } }

        public void Run()
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Xml Reading Test");
            Console.WriteLine();


            var xmlData = Resources.SamplePoints;

            // TODO: 
            // Determine for each parameter stored in the variable below, the average value, lowest and highest number.
            // Example output
            // parameter   LOW AVG MAX
            // temperature   x   y   z
            // pH            x   y   z
            // Chloride      x   y   z
            // Phosphate     x   y   z
            // Nitrate       x   y   z

            PrintOverview(xmlData);

            Console.WriteLine("------------------------------------");
        }

        private static void PrintOverview(string xml)
        {
            XmlDocument quoteDocument = new XmlDocument();
            quoteDocument.LoadXml(xml);


            var doc = XDocument.Parse(xml);

            var measurements = doc.Descendants("measurement")
                   .ToList();

            float res = 0;
            var temperatureList = measurements.Descendants("param")
                .Where(e => (string)e.Attribute("name") == "temperature"
                &&
                float.TryParse(e.Value, out res))
                .Select(e => res)
               .ToList();

            var phList = measurements.Descendants("param")
                .Where(e => (string)e.Attribute("name") == "pH"
                &&
                float.TryParse(e.Value, out res))
                .Select(e => res)
               .ToList();

            var chlorideList = measurements.Descendants("param")
                .Where(e => (string)e.Attribute("name") == "Chloride"
                 &&
                float.TryParse(e.Value, out res))
                .Select(e => res)
                .ToList();

            var phosphateList = measurements.Descendants("param")
               .Where(e => (string)e.Attribute("name") == "Phosphate"
               &&
               float.TryParse(e.Value, out res))
               .Select(e => res)
              .ToList();

            var nitrateList = measurements.Descendants("param")
               .Where(e => (string)e.Attribute("name") == "Nitrate"
                &&
               float.TryParse(e.Value, out res))
               .Select(e => res)
               .ToList();

            Console.WriteLine(@"Parameter   LOW  AVG  MAX");
            Console.WriteLine((string.Format("temperature {0}  {1}  {2}", temperatureList.Min(), (float)temperatureList.Average(), temperatureList.Max())));
            Console.WriteLine((string.Format("ph          {0}  {1}  {2}", phList.Min(), phList.Average(), phList.Max())));
            Console.WriteLine((string.Format("Chloride    {0}  {1}  {2}", chlorideList.Min(), chlorideList.Average(), chlorideList.Max())));
            Console.WriteLine((string.Format("Phosphate   {0}  {1}  {2}", phosphateList.Min(), phosphateList.Average(), phosphateList.Max())));
            Console.WriteLine((string.Format("Nitrate     {0}  {1}  {2}", nitrateList.Min(), nitrateList.Average(), nitrateList.Max())));
            
        }



    }
}
