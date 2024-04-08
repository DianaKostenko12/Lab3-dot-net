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
    public class DocumentJsonMethods
    {
        private readonly string _filePath = "D:\\University\\.Net\\Lab3\\Lab3(.Net)\\Lab3-dot-net\\Lab3-dot-net\\Json Files\\Document.json";

        public async Task AddDocumentsWithSerializer(List<Document> documents)
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
                    JsonSerializer.Serialize(fs, documents, options);
                    Console.WriteLine("Дані успішно записані");
                }
            }
            else
            {
                Console.WriteLine("Файл не існує.");
            }
        }

        public async Task GetDocumentsWithDeserializer()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                List<Document> documents = await JsonSerializer.DeserializeAsync<List<Document>>(fs);
                if (documents != null)
                {
                    foreach (var document in documents)
                    {
                        Console.WriteLine($"Документ: {document.Id} - {document.DocumentName}");
                    }
                }
                else
                {
                    Console.WriteLine("Файл пустий");
                }
            }
        }

        public void AddDocumentsWithJsonDocument(List<Document> documents)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                var newDocumentList = new JsonArray();
                foreach (var document in documents)
                {
                    var newDocument = new JsonObject
                    {
                        ["Id"] = document.Id,
                        ["DocumentName"] = document.DocumentName,
                    };

                    newDocumentList.Add(newDocument);
                }

                var documentObject = new JsonObject
                {
                    ["documents"] = newDocumentList
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };

                var jsonString = documentObject.ToJsonString(options);

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

        public void GetDocumentsWithJsonDocument()
        {
            string jsonString = File.ReadAllText(_filePath);
            if (jsonString != null)
            {
                List<Document> documents = new List<Document>();
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("documents", out JsonElement documentListElement))
                    {
                        foreach (JsonElement documentElement in documentListElement.EnumerateArray())
                        {
                            Document documentModel = new Document(
                                    documentElement.GetProperty("Id").GetInt32(),
                                    documentElement.GetProperty("DocumentName").ToString());
                            documents.Add(documentModel);
                        }
                    }
                }

                foreach (var document in documents)
                {
                    Console.WriteLine($"Документ: {document.Id} - {document.DocumentName}");
                }
            }
            else
            {
                Console.WriteLine("Файл пустий");
            }
        }

        public void AddDocumentsWithJsonNode(List<Document> documents)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                JsonArray jsonArray = new JsonArray();

                foreach (var document in documents)
                {
                    JsonNode rootNode = JsonNode.Parse("{}");
                    rootNode["Id"] = document.Id;
                    rootNode["DocumentName"] = document.DocumentName;

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

        public void GetDocumentsWithJsonNode()
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
                        string documentName = node["DocumentName"].ToString();

                        Console.WriteLine($"Документ: {id} - {documentName}");

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
