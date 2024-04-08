using Lab3_dot_net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Lab3_dot_net.JsonMethods
{
    public class AssetDocumentJsonMethods
    {
        private readonly string _filePath = "D:\\University\\.Net\\Lab3\\Lab3(.Net)\\Lab3-dot-net\\Lab3-dot-net\\Json Files\\AssetDocument.json";

        public async Task AddAssetDocumentsWithSerializer(List<AssetDocument> documents)
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

        public async Task GetAssetDocumentsWithDeserializer()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                List<AssetDocument> documents = await JsonSerializer.DeserializeAsync<List<AssetDocument>>(fs);
                if (documents != null)
                {
                    foreach (var document in documents)
                    {
                        Console.WriteLine($"Id основного засобу: {document.AssetId} Id документу: {document.DocumentId} " +
                            $"Дата виконання операції: {document.Date}");
                    }
                }
                else
                {
                    Console.WriteLine("Файл пустий");
                }
            }
        }

        public void AddAssetDocumentsWithJsonDocument(List<AssetDocument> documents)
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
                        ["AssetId"] = document.AssetId,
                        ["DocumentId"] = document.DocumentId,
                        ["Date"] = document.Date,
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

        public void GetAssetDocumentsWithJsonDocument()
        {
            string jsonString = File.ReadAllText(_filePath);
            if (jsonString != null)
            {
                List<AssetDocument> documents = new List<AssetDocument>();
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("documents", out JsonElement documentListElement))
                    {
                        foreach (JsonElement documentElement in documentListElement.EnumerateArray())
                        {
                            AssetDocument assetDocument = new AssetDocument(
                                    documentElement.GetProperty("AssetId").GetInt32(),
                                    documentElement.GetProperty("DocumentId").GetInt32(),
                                    documentElement.GetProperty("Date").GetDateTime());
                            documents.Add(assetDocument);
                        }
                    }
                }

                foreach (var document in documents)
                {
                    Console.WriteLine($"Id основного засобу: {document.AssetId} Id документу: {document.DocumentId} " +
                           $"Дата виконання операції: {document.Date}");
                }
            }
            else
            {
                Console.WriteLine("Файл пустий");
            }
        }

        public void AddAssetDocumentsWithJsonNode(List<AssetDocument> documents)
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
                    rootNode["AssetId"] = document.AssetId;
                    rootNode["DocumentId"] = document.DocumentId;
                    rootNode["Date"] = document.Date;

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

        public void GetAssetDocumentsWithJsonNode()
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
                        int assetId = int.Parse(node["AssetId"].ToString());
                        int documentId = int.Parse(node["DocumentId"].ToString());
                        DateTime date = DateTime.Parse(node["Date"].ToString());

                        Console.WriteLine($"Id основного засобу: {assetId} Id документу: {documentId} " +
                        $"Дата виконання операції: {date}");

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
