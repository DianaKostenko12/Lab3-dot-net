using Lab3_dot_net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab3_dot_net.JsonMethods
{
    public class ResponsiblePersonJsonMethods
    {
        private readonly string _filePath = "D:\\University\\.Net\\Lab3\\Lab3(.Net)\\Lab3-dot-net\\Lab3-dot-net\\Json Files\\ResponsiblePerson.json";

        public async Task AddResponsiblePersonsWithSerializer(List<ResponsiblePerson> persons)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
                {
                    JsonSerializer.Serialize(fs, persons, options);
                    Console.WriteLine("Дані успішно записані");
                }
            }
            else
            {
                Console.WriteLine("Файл не існує.");
            }
        }

        public async Task GetResponsiblePersonsWithDeserializer()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                List<ResponsiblePerson> persons = await JsonSerializer.DeserializeAsync<List<ResponsiblePerson>>(fs);
                if (persons != null)
                {
                    foreach (var person in persons)
                    {
                        Console.WriteLine($"Id: {person.Id}, Прiзвище людини: {person.Surname}, Iм'я: {person.Name}, Номер телефону: {person.Phone}");
                    }
                }
                else
                {
                    Console.WriteLine("Файл пустий");
                }
            }
        }

        public void AddResponsiblePersonsWithJsonDocument(List<ResponsiblePerson> persons)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                var newResponsiblePersonList = new JsonArray();
                foreach (var person in persons)
                {
                    var newResponsiblePerson = new JsonObject
                    {
                        ["Id"] = person.Id,
                        ["Name"] = person.Name,
                        ["Surname"] = person.Surname,
                        ["Phone"] = person.Phone
                    };

                    newResponsiblePersonList.Add(newResponsiblePerson);
                }

                var personObject = new JsonObject
                {
                    ["responsiblePersons"] = newResponsiblePersonList
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };

                var jsonString = personObject.ToJsonString(options);

                using (var stream = new FileStream(_filePath, FileMode.Append, FileAccess.Write))
                using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
                {
                    Indented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }))

                {
                    using JsonDocument document = JsonDocument.Parse(jsonString);
                    JsonElement root = document.RootElement;
                    if (root.ValueKind == JsonValueKind.Object)
                    {
                        writer.WriteStartObject();
                    }
                    else
                    {
                        return;
                    }
                    foreach (JsonProperty property in root.EnumerateObject())
                    {
                        property.WriteTo(writer);
                    }
                    writer.WriteEndObject();
                    Console.WriteLine("Дані успішно додані");
                }
            }
            else
            {
                Console.WriteLine("Файл не існує.");
            }
        }

        public void GetResponsiblePersonsWithJsonDocument()
        {
            string jsonString = File.ReadAllText(_filePath);
            if (jsonString != null)
            {
                List<ResponsiblePerson> persons = new List<ResponsiblePerson>();
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("responsiblePersons", out JsonElement personListElement))
                    {
                        foreach (JsonElement personElement in personListElement.EnumerateArray())
                        {
                            ResponsiblePerson person = new ResponsiblePerson(
                                    personElement.GetProperty("Id").GetInt32(),
                                    personElement.GetProperty("Name").GetString(),
                                    personElement.GetProperty("Surname").GetString(),
                                    personElement.GetProperty("Phone").GetString());
                            persons.Add(person);
                        }
                    }
                }

                foreach (var person in persons)
                {
                    Console.WriteLine($"Id: {person.Id}, Прiзвище людини: {person.Surname}, Iм'я: {person.Name}, Номер телефону: {person.Phone}");
                }
            }
            else
            {
                Console.WriteLine("Файл пустий");
            }
        }

        public void AddResponsiblePersonsWithJsonNode(List<ResponsiblePerson> persons)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                JsonArray jsonArray = new JsonArray();

                foreach (var person in persons)
                {
                    JsonNode rootNode = JsonNode.Parse("{}");
                    rootNode["Id"] = person.Id;
                    rootNode["Name"] = person.Name;
                    rootNode["Surname"] = person.Surname;
                    rootNode["Phone"] = person.Phone;

                    jsonArray.Add(rootNode);
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                // Отримання JSON-рядка з JsonNode
                string jsonString = jsonArray.ToJsonString(options);

                // Запис JSON-рядка у файл

                File.WriteAllText(_filePath, jsonString);

                Console.WriteLine($"Дані записані");
            }
            else
            {
                Console.WriteLine("Файл не існує");
            }
        }

        public void GetResponsiblePersonsWithJsonNode()
        {
            string jsonString = File.ReadAllText(_filePath);
            if (jsonString != null)
            {
                JsonNode rootNode = JsonNode.Parse(jsonString);

                if (rootNode is JsonArray rootArray)
                {
                    foreach (var node in rootArray)
                    {
                        // Отримання значень за ключами
                        int id = int.Parse(node["Id"].ToString());
                        string name = node["Name"].ToString();
                        string surname = node["Surname"].ToString();
                        string phone = node["Phone"].ToString();

                        Console.WriteLine($"Id: {id}, Прiзвище людини: {surname}, " +
                            $"Iм'я: {name}, Номер телефону: {phone}");

                    }
                }
                else
                {
                    Console.WriteLine("Кореневий вузол не є масивом");
                }
            }
            else
            {
                Console.WriteLine("Файл пустий");
            }
        }
    }
}
