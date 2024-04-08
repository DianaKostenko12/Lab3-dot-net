using Lab3_dot_net.JsonMethods;

namespace Lab3_dot_net
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var dataFilling = new DataFilling();
            var assetMethods = new AssetJsonMethods();
            var departmentMethods = new DepartmentJsonMethods();
            var documentMethods = new DocumentJsonMethods();
            var personMethods = new ResponsiblePersonJsonMethods();
            var assetDocumentMethods = new AssetDocumentJsonMethods();

            var operations = new Dictionary<int, Action>
            {
                { 1, async () => await assetMethods.AddAssetsWithSerializer(dataFilling.assets) },
                { 2, async () => await assetMethods.GetAssetsWithDeserializer() },
                { 3, () => assetMethods.AddAssetsWithJsonDocument(dataFilling.assets) },
                { 4, () => assetMethods.GetAssetsWithJsonDocument() },
                { 5, () => assetMethods.AddAssetsWithJsonNode(dataFilling.assets) },
                { 6, () => assetMethods.GetAssetsWithJsonNode() },
                { 7, async () => await departmentMethods.AddDepartmentsWithSerializer(dataFilling.departments) },
                { 8, async () => await departmentMethods.GetDepartmentsWithDeserializer() },
                { 9, () => departmentMethods.AddDepartmentsWithJsonDocument(dataFilling.departments) },
                { 10, () => departmentMethods.GetAssetsWithJsonDocument() },
                { 11, () => departmentMethods.AddDepartmentsWithJsonNode(dataFilling.departments) },
                { 12, () => departmentMethods.GetDepartmentsWithJsonNode() },
                { 13, async () => await documentMethods.AddDocumentsWithSerializer(dataFilling.documents) },
                { 14, async () => await documentMethods.GetDocumentsWithDeserializer() },
                { 15, () => documentMethods.AddDocumentsWithJsonDocument(dataFilling.documents) },
                { 16, () => documentMethods.GetDocumentsWithJsonDocument() },
                { 17, () => documentMethods.AddDocumentsWithJsonNode(dataFilling.documents) },
                { 18, () => documentMethods.GetDocumentsWithJsonNode() },
                { 19, async () => await personMethods.AddResponsiblePersonsWithSerializer(dataFilling.responsiblePersons) },
                { 20, async () => await personMethods.GetResponsiblePersonsWithDeserializer() },
                { 21, () => personMethods.AddResponsiblePersonsWithJsonDocument(dataFilling.responsiblePersons) },
                { 22, () => personMethods.GetResponsiblePersonsWithJsonDocument() },
                { 23, () => personMethods.AddResponsiblePersonsWithJsonNode(dataFilling.responsiblePersons) },
                { 24, () => personMethods.GetResponsiblePersonsWithJsonNode() },
                { 25, async () => await assetDocumentMethods.AddAssetDocumentsWithSerializer(dataFilling.assetDocuments) },
                { 26, async () => await assetDocumentMethods.GetAssetDocumentsWithDeserializer() },
                { 27, () => assetDocumentMethods.AddAssetDocumentsWithJsonDocument(dataFilling.assetDocuments) },
                { 28, () => assetDocumentMethods.GetAssetDocumentsWithJsonDocument() },
                { 29, () => assetDocumentMethods.AddAssetDocumentsWithJsonNode(dataFilling.assetDocuments) },
                { 30, () => assetDocumentMethods.GetAssetDocumentsWithJsonNode() },
            };

            string answer;
            do
            {
                Console.Clear();
                Console.WriteLine("Виберiть, що бажаєте зробити?");
                foreach (var operation in operations)
                {
                    if(operation.Key == 7 || operation.Key == 13 || operation.Key == 19 || operation.Key == 25)
                    {
                        Console.WriteLine();
                    }
                    Console.WriteLine($"{operation.Key}-{GetOperationName(operation.Key)}");
                }

                int input = Convert.ToInt32(Console.ReadLine());
                if (operations.ContainsKey(input))
                {
                    Console.Clear();
                    operations[input]();
                }
                else
                {
                    Console.WriteLine("Невiрний вибiр.");
                }

                Console.WriteLine("Бажаєте продовжити? (+/-)");
                answer = Console.ReadLine();
            } while (answer == "+");
        }

        static string GetOperationName(int operation)
        {
            switch (operation)
            {
                case 1: return "Серiалiзувати список об'єктiв Asset за допомогою Serializer";
                case 2: return "Десерiалiзувати список об'єктiв Asset за допомогою Deserializer";
                case 3: return "Зберегти список об'єктiв Asset за допомогою JsonDocument";
                case 4: return "Отримати список об'єктiв Asset за допомогою JsonDocument";
                case 5: return "Зберегти список об'єктiв Asset за допомогою JsonNode";
                case 6: return "Отримати список об'єктiв Asset за допомгою JsonNode";
                case 7: return "Серiалiзувати список об'єктiв Department за допомогою Serializer";
                case 8: return "Десерiалiзувати список об'єктiв Department за допомогою Deserializer";
                case 9: return "Зберегти список об'єктiв Department за допомогою JsonDocument";
                case 10: return "Отримати список об'єктiв Department за допомогою JsonDocument";
                case 11: return "Зберегти список об'єктiв Department за допомогою JsonNode";
                case 12: return "Отримати список об'єктiв Department за допомогою JsonNode";
                case 13: return "Серiалiзувати список об'єктiв Document за допомогою Serializer";
                case 14: return "Десерiалiзувати список об'єктiв Document за допомогою Deserializer";
                case 15: return "Зберегти список об'єктiв Document за допомогою JsonDocument";
                case 16: return "Отримати список об'єктiв Document за допомогою JsonDocument";
                case 17: return "Зберегти список об'єктiв Document за допомогою JsonNode";
                case 18: return "Отримати список об'єктiв Document за допомогою JsonNode";
                case 19: return "Серiалiзувати список об'єктiв ResponsiblePerson за допомогою Serializer";
                case 20: return "Десерiалiзувати список об'єктiв ResponsiblePerson за допомогою Deserializer";
                case 21: return "Зберегти список об'єктiв ResponsiblePerson за допомогою JsonDocument";
                case 22: return "Отримати список об'єктiв ResponsiblePerson за допомогою JsonDocument";
                case 23: return "Зберегти список об'єктiв ResponsiblePerson за допомогою JsonNode";
                case 24: return "Отримати список об'єктiв ResponsiblePerson за допомогою JsonNode";
                case 25: return "Серiалiзувати список об'єктiв AssetDocument за допомогою Serializer";
                case 26: return "Десерiалiзувати список об'єктiв AssetDocument за допомогою Deserializer";
                case 27: return "Зберегти список об'єктiв AssetDocument за допомогою JsonDocument";
                case 28: return "Отримати список об'єктiв AssetDocument за допомогою JsonDocument";
                case 29: return "Зберегти список об'єктiв AssetDocument за допомогою JsonNode";
                case 30: return "Отримати список об'єктiв AssetDocument за допомогою JsonNode";
                default: return string.Empty;
            }
        }
    }
}
