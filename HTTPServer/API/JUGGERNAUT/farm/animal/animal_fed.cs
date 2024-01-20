﻿using System.Xml.Linq;

namespace HTTPServer.API.JUGGERNAUT.farm.animal
{
    public class animal_fed
    {
        public static string? ProcessFed(Dictionary<string, string>? QueryParameters)
        {
            if (QueryParameters != null)
            {
                string? user = QueryParameters["user"];
                string? type = QueryParameters["type"];
                string? id = QueryParameters["id"];
                string? posix = QueryParameters["posix"];
                string? permBoost = QueryParameters["permBoost"];

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(posix) && !string.IsNullOrEmpty(permBoost))
                {
                    Directory.CreateDirectory($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data");

                    if (File.Exists($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data/{user}.xml"))
                        File.WriteAllText($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data/{user}.xml",
                                UpdateFedAttributes(File.ReadAllText($"{HTTPServerConfiguration.APIStaticFolder}/juggernaut/farm/User_Data/{user}.xml"), id, type, posix, permBoost));

                    return string.Empty;
                }
            }

            return null;
        }

        private static string UpdateFedAttributes(string xmlData, string id, string type, string posix, string permBoost)
        {
            try
            {
                XDocument xdoc = XDocument.Parse(xmlData);

                XElement? animalToUpdate = xdoc.Descendants("animal")
                    .FirstOrDefault(a => a.Element("id")?.Value == id && a.Element("t")?.Value == type);

                if (animalToUpdate != null)
                {
                    animalToUpdate.Element("lf").Value = posix;
                    animalToUpdate.Element("pbu").Value = permBoost;
                }

                return xdoc.ToString();
            }
            catch (Exception)
            {

            }

            return xmlData;
        }
    }
}
