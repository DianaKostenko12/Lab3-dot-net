using Lab3_dot_net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Lab3_dot_net.JsonMethods
{
    public class DepartmentJsonMethods
    {
        private readonly string _filePath = "D:\\University\\.Net\\Lab3\\Lab3(.Net)\\Lab3-dot-net\\Lab3-dot-net\\Json Files\\Department.json";
        public async Task AddDepartmentsWithSerializer(List<Department> departments)
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
                    JsonSerializer.Serialize(fs, departments, options);
                    Console.WriteLine("Дані успішно записані");
                }
            }
            else
            {
                Console.WriteLine("Файл не існує.");
            }
        }

        public async Task GetDepartmentsWithDeserializer()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                List<Department> departments = await JsonSerializer.DeserializeAsync<List<Department>>(fs);
                if (departments != null)
                {
                    foreach (var department in departments)
                    {
                        Console.WriteLine($"Вiддiл: {department.Id} - {department.DepartmentName}");
                    }
                }
                else
                {
                    Console.WriteLine("Файл пустий");
                }
            }
        }

        public void AddDepartmentsWithJsonDocument(List<Department> departments)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                var newDepartmentList = new JsonArray();
                foreach (var department in departments)
                {
                    var newDepartment = new JsonObject
                    {
                        ["Id"] = department.Id,
                        ["DepartmentName"] = department.DepartmentName,
                    };

                    newDepartmentList.Add(newDepartment);
                }

                var departmentObject = new JsonObject
                {
                    ["departments"] = newDepartmentList
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };

                var jsonString = departmentObject.ToJsonString(options);

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
                List<Department> departments = new List<Department>();
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("departments", out JsonElement departmentListElement))
                    {
                        foreach (JsonElement departmentElement in departmentListElement.EnumerateArray())
                        {
                            Department department = new Department(
                                    departmentElement.GetProperty("Id").GetInt32(),
                                    departmentElement.GetProperty("DepartmentName").ToString());
                            departments.Add(department);
                        }
                    }
                }

                foreach (var department in departments)
                {
                    Console.WriteLine($"Вiддiл: {department.Id} - {department.DepartmentName}");
                }
            }
            else
            {
                Console.WriteLine("Файл пустий");
            }
        }

        public void AddDepartmentsWithJsonNode(List<Department> departments)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }

                JsonArray jsonArray = new JsonArray();

                foreach (var department in departments)
                {
                    JsonNode rootNode = JsonNode.Parse("{}");
                    rootNode["Id"] = department.Id;
                    rootNode["DepartmentName"] = department.DepartmentName;
                   
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

        public void GetDepartmentsWithJsonNode()
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
                        string departmentName = node["DepartmentName"].ToString();

                        Console.WriteLine($"Вiддiл: {id} - {departmentName}");

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
