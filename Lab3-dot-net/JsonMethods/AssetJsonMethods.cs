using Lab3_dot_net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;

namespace Lab3_dot_net.JsonMethods
{
    public class AssetJsonMethods
    {
        private readonly string _filePath = "D:\\University\\.Net\\Lab3\\Lab3(.Net)\\Lab3-dot-net\\Lab3-dot-net\\Json Files\\Asset.json";

        public async Task AddAssetsWithSerializer(List<Asset> assets)
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
                    JsonSerializer.Serialize(fs, assets, options);
                    Console.WriteLine("Дані успішно записані");
                }
            }
            else
            {
                Console.WriteLine("Файл не існує.");
            }
        }

        public async Task GetAssetsWithDeserializer()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                List<Asset> assets = await JsonSerializer.DeserializeAsync<List<Asset>>(fs);
                if (assets != null)
                {
                    foreach (var asset in assets)
                    {
                        Console.WriteLine($"Id: {asset.Id}, Iнвентарний номер: {asset.InventoryNumber}, " +
                        $"Назва засобу: {asset.Name}, Первiсна вартiсть: {asset.InitialCost}," +
                        $" DepartmentId: {asset.DepartmentId}, ResponsiblePersonId: {asset.ResponsiblePersonId}");
                    }
                }
                else
                {
                    Console.WriteLine("Файл пустий");
                }
            }
        }

        public void AddAssetsWithJsonDocument(List<Asset> assets)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                var newAssetList = new JsonArray();
                foreach (var asset in assets)
                {
                    var newAsset = new JsonObject
                    {
                        ["Id"] = asset.Id,
                        ["InventoryNumber"] = asset.InventoryNumber,
                        ["Name"] = asset.Name,
                        ["InitialCost"] = asset.InitialCost,
                        ["DepartmentId"] = asset.DepartmentId,
                        ["ResponsiblePersonId"] = asset.ResponsiblePersonId
                    };

                    newAssetList.Add(newAsset);
                }

                var assetObject = new JsonObject
                {
                    ["assets"] = newAssetList
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };

                var jsonString = assetObject.ToJsonString(options);

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

        public void GetAssetsWithJsonDocument()
        {
            string jsonString = File.ReadAllText(_filePath);
            if (jsonString != null)
            {
                List<Asset> assets = new List<Asset>();
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("assets", out JsonElement assetListElement))
                    {
                        foreach (JsonElement assetElement in assetListElement.EnumerateArray())
                        {
                            Asset asset = new Asset(
                                    assetElement.GetProperty("Id").GetInt32(),
                                    assetElement.GetProperty("InventoryNumber").GetInt32(),
                                    assetElement.GetProperty("Name").GetString(),
                                    assetElement.GetProperty("InitialCost").GetDecimal(),
                                    assetElement.GetProperty("DepartmentId").GetInt32(),
                                    assetElement.GetProperty("ResponsiblePersonId").GetInt32());
                            assets.Add(asset);
                        }
                    }
                }

                foreach (var asset in assets)
                {
                    Console.WriteLine($"Id: {asset.Id}, Iнвентарний номер: {asset.InventoryNumber}, " +
                        $"Назва засобу: {asset.Name}, Первiсна вартiсть: {asset.InitialCost}," +
                        $" DepartmentId: {asset.DepartmentId}, ResponsiblePersonId: {asset.ResponsiblePersonId}");
                }
            }
            else
            {
                Console.WriteLine("Файл пустий");
            }
        }

        public void AddAssetsWithJsonNode(List<Asset> assets) 
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                JsonArray jsonArray = new JsonArray();

                foreach (var asset in assets)
                {
                    JsonNode rootNode = JsonNode.Parse("{}");
                    rootNode["Id"] = asset.Id;
                    rootNode["InventoryNumber"] = asset.InventoryNumber;
                    rootNode["AssetName"] = asset.Name;
                    rootNode["InitialCost"] = asset.InitialCost;
                    rootNode["DepartmentId"] = asset.DepartmentId;
                    rootNode["ResponsiblePersonId"] = asset.ResponsiblePersonId;

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

        public void GetAssetsWithJsonNode()
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
                        int inventoryNumber = int.Parse(node["InventoryNumber"].ToString());
                        string assetName = node["AssetName"].ToString();
                        decimal initialCost = decimal.Parse(node["InitialCost"].ToString());
                        int departmentId = int.Parse(node["DepartmentId"].ToString());
                        int responsiblePersonId = int.Parse(node["ResponsiblePersonId"].ToString());

                        Console.WriteLine($"Id: {id}, Iнвентарний номер: {inventoryNumber}, " +
                       $"Назва засобу: {assetName}, Первiсна вартiсть: {initialCost}," +
                       $" DepartmentId: {departmentId}, ResponsiblePersonId: {responsiblePersonId}");

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
