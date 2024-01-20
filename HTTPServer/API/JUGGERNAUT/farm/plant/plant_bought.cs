﻿using System.Xml.Linq;
using System.Xml;

namespace HTTPServer.API.JUGGERNAUT.farm.plant
{
    public class plant_bought
    {
        public static string? ProcessBought(Dictionary<string, string>? QueryParameters)
        {
            if (QueryParameters != null)
            {
                string? user = QueryParameters["user"];
                string? type = QueryParameters["type"];
                string? id = QueryParameters["id"];
                string? amount = QueryParameters["amount"];

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(amount))
                {
                    Directory.CreateDirectory($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data");

                    if (File.Exists($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data/{user}.xml"))
                    {
                        // Load the XML string into an XmlDocument
                        XmlDocument xmlDoc = new();
                        xmlDoc.LoadXml(AddPlantEntry(File.ReadAllText($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data/{user}.xml"), type, id));

                        // Find the <gold> element
                        XmlElement? goldElement = xmlDoc.SelectSingleNode("/xml/resources/gold") as XmlElement;

                        if (goldElement != null)
                        {
                            try
                            {
                                int remaininggold = int.Parse(goldElement.InnerText) - int.Parse(amount);

                                if (remaininggold < 0)
                                    remaininggold = 0;

                                // Replace the value of <gold> with a new value
                                goldElement.InnerText = remaininggold.ToString();
                            }
                            catch (Exception)
                            {
                                // Not Important
                            }

                            File.WriteAllText($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data/{user}.xml", xmlDoc.OuterXml);
                        }
                    }

                    return string.Empty;
                }
            }

            return null;
        }

        private static string AddPlantEntry(string xmlData, string type, string id)
        {
            XDocument xdoc = XDocument.Parse(xmlData);

            XElement newAnimal = new("plant",
                new XElement("t", type),
                new XElement("l", 1),
                new XElement("id", id),
                new XElement("lw", 0),
                new XElement("pbu", 0),
                new XElement("tbu", 0)
            );

            xdoc.Descendants("plants").First().Add(newAnimal);

            return xdoc.ToString();
        }
    }
}
