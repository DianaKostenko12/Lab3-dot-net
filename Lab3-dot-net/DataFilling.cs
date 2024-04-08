using Lab3_dot_net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dot_net
{
    public class DataFilling
    {
        public List<Asset> assets = new List<Asset>
               {
                    new Asset(1, 1101, "Верстат токарний", 1500, 1, 1),
                    new Asset(2, 1102, "Прес гiдравлiчний", 2000, 1, 1),
                    new Asset(3, 1103, "Конвеєр лiнiйний", 3000, 1, 2),
                    new Asset(4, 1201, "Комп'ютер офiсний", 800, 2, 2),
                    new Asset(5, 1202, "Принтер лазерний", 400, 2, 3),
                    new Asset(6, 1203, "Сканер документiв", 300, 2, 3),
                    new Asset(7, 1301, "Осцилограф цифровий", 1200, 3, 4),
                    new Asset(8, 1302, "Тепловiзор FLIR", 2500, 3, 4),
                    new Asset(9, 1303, "Лазерний вимiрювач дистанцiї", 150, 3, 5),
                    new Asset(10, 1401, "Банкiвський термiнал", 500, 4, 5),
                    new Asset(11, 1402, "Касовий апарат", 700, 4, 6),
                    new Asset(12, 1403, "Калькулятор офiсний", 50, 4, 6),
                    new Asset(13, 1501, "Вантажiвка велика", 20000, 5, 7),
                    new Asset(14, 1502, "Складськi стелажi", 1000, 5, 7),
                    new Asset(15, 1503, "Палетнi стiйки", 800, 5, 8),
                    new Asset(16, 1204, "Копiр Xerox", 600, 2, 8),
                    new Asset(17, 1205, "Мультимедiйний проектор", 1000, 2, 9),
                    new Asset(18, 1504, "Вантажний електрокар", 5000, 5, 10),
                    new Asset(19, 1505, "Осцилограф цифровий", 1200, 3, 4)
               };

        public List<Department> departments = new List<Department>
              {
                  new Department(1, "Виробничий пiдроздiл"),
                  new Department(2, "Адмiнiстративний пiдроздiл"),
                  new Department(3, "Технiчний пiдроздiл"),
                  new Department(4, "Фiнансовий пiдроздiл"),
                  new Department(5, "Логiстичний пiдроздiл")
              };

        public List<Document> documents = new List<Document>
                {
                    new Document(1, "Акт приймання-передачi"),
                    new Document(2, "Акт списання основних засобiв"),
                    new Document(3, "Акт приймання на облiк основних засобiв"),
                    new Document(4, "Прибуткова накладна на основний засiб"),
                    new Document(5, "Акт перемiщення основних засобiв мiж пiдроздiлами"),
                    new Document(6, "Акт введення в експлуатацiю нового основного засобу"),
                    new Document(7, "Акт зняття з експлуатацiї основного засобу"),
                    new Document(8, "Звiт про ремонт основних засобiв"),
                    new Document(9, "Довiдка про стан основних засобiв"),
        };
        public List<ResponsiblePerson> responsiblePersons = new List<ResponsiblePerson>
        {
                    new ResponsiblePerson(1, "Iван", "Петров", "+380991234567"),
                    new ResponsiblePerson(2, "Марiя", "Iванова", "+380992345678"),
                    new ResponsiblePerson(3, "Петро", "Сидоров", "+380993456789"),
                    new ResponsiblePerson(4, "Олена", "Коваленко", "+380994567890"),
                    new ResponsiblePerson(5, "Андрiй", "Бондаренко", "+380995678901"),
                    new ResponsiblePerson(6, "Наталiя", "Мороз", "+380996789012"),
                    new ResponsiblePerson(7, "Сергiй", "Поляков", "+380997890123"),
                    new ResponsiblePerson(8, "Оксана", "Шевченко", "+380998901234"),
                    new ResponsiblePerson(9, "Вiталiй", "Козлов", "+380999012345"),
                    new ResponsiblePerson(10, "Юлiя", "Семенова", "+380999123456")
                };


        public List<AssetDocument> assetDocuments = new List<AssetDocument>
               {
                    new AssetDocument(1, 1, new DateTime(2023, 1, 15)),
                    new AssetDocument(2, 2, new DateTime(2023, 2, 28)),
                    new AssetDocument(3, 2, new DateTime(2023, 4, 5)),
                    new AssetDocument(4, 3, new DateTime(2023, 7, 30)),
                    new AssetDocument(5, 7, new DateTime(2023, 8, 8)),
                    new AssetDocument(6, 3, new DateTime(2023, 9, 16)),
                    new AssetDocument(7, 1, new DateTime(2023, 8, 8)),
                    new AssetDocument(8, 6, new DateTime(2023, 11, 15)),
                    new AssetDocument(9, 5, new DateTime(2023, 12, 28)),
                    new AssetDocument(10,7, new DateTime(2024, 1, 10)),
                    new AssetDocument(1, 4, new DateTime(2024, 2, 5)),
                    new AssetDocument(2, 8, new DateTime(2024, 3, 20)),
                    new AssetDocument(3, 5, new DateTime(2024, 4, 12)),
                    new AssetDocument(4, 4, new DateTime(2024, 5, 30)),
                    new AssetDocument(5, 6, new DateTime(2024, 6, 8)),
                    new AssetDocument(6, 9, new DateTime(2024, 7, 16)),
                    new AssetDocument(11, 6, new DateTime(2023, 11, 15)),
                    new AssetDocument(12, 5, new DateTime(2023, 12, 28)),
                    new AssetDocument(13,7, new DateTime(2024, 1, 10)),
                    new AssetDocument(14, 4, new DateTime(2024, 2, 5)),
                    new AssetDocument(15, 8, new DateTime(2024, 3, 20)),
                    new AssetDocument(16, 5, new DateTime(2024, 4, 12)),
                    new AssetDocument(17, 4, new DateTime(2024, 5, 30)),
                    new AssetDocument(18, 6, new DateTime(2024, 6, 8)),
                    new AssetDocument(18, 9, new DateTime(2024, 7, 16)),
               };
    }
}
