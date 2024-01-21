using Microsoft.EntityFrameworkCore;
using TechDrop.Data.Models;
using TechDrop.Data.Models.ForProduct;
using TechDrop.Data.Models.ForProduct.ForProcessor;

namespace TechDrop.Data;

public sealed class TechDropDbContext : DbContext
{
    #region DbSets for User

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;

    #endregion

    #region DbSets for Product

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<ProductPicture> ProductPictures { get; set; } = null!;
    public DbSet<Picture> Pictures { get; set; } = null!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

    #region DbSets for Processor

    public DbSet<Processor> Processors { get; set; } = null!;
    public DbSet<GraphCore> GraphCores { get; set; } = null!;
    public DbSet<Socket> Sockets { get; set; } = null!;
    public DbSet<PciExpress> PciExpresses { get; set; } = null!;
    public DbSet<RamType> RamTypes { get; set; } = null!;
    public DbSet<ProcessorRamType> ProcessorRamTypes { get; set; } = null!;

    #endregion

    #endregion

    public TechDropDbContext()
    {
        if (Database.EnsureCreated())
        {
            #region Заполнение справочников + Users

            var osOpened = new OrderStatus { Name = "Открыт" };
            var osAccepted = new OrderStatus { Name = "Заказ принят" };
            var osAssembly = new OrderStatus { Name = "Заказ на сборке" };
            var osDelivery = new OrderStatus { Name = "Заказ передан в службу доставки" };
            var osCame = new OrderStatus { Name = "Заказ в пункте выдачи" };
            var osClosed = new OrderStatus { Name = "Завершен" };
            var osCancelled = new OrderStatus { Name = "Отменен" };
            OrderStatuses.AddRange(osOpened, osAccepted, osAssembly, osDelivery, osCame, osClosed, osCancelled);

            var rtDdr3 = new RamType { Name = "DDR3" };
            var rtDdr4 = new RamType { Name = "DDR4" };
            var rtDdr5 = new RamType { Name = "DDR5" };
            RamTypes.AddRange(rtDdr3, rtDdr4, rtDdr5);

            var pe3 = new PciExpress { Name = "PCI-E 3.0" };
            var pe4 = new PciExpress { Name = "PCI-E 4.0" };
            var pe5 = new PciExpress { Name = "PCI-E 5.0" };
            PciExpresses.AddRange(pe3, pe4, pe5);

            var sAm4 = new Socket { Name = "AM4" };
            var sAm5 = new Socket { Name = "AM5" };
            var sLga1200 = new Socket { Name = "LGA 1200" };
            var sLga1700 = new Socket { Name = "LGA 1700" };
            var ssWrx8 = new Socket { Name = "sWRX8" };
            var sLga2066 = new Socket { Name = "LGA 2066" };
            Sockets.AddRange(sAm4, sAm5, sLga1200, sLga1700, ssWrx8, sLga2066);

            var gcVega3 = new GraphCore
                { Model = "AMD Radeon Vega 3", MaxFrequency = 1100, ExecutiveBlocks = 3, ShadingUnits = 192 };
            GraphCores.AddRange(gcVega3);


            var pcProcessor = new ProductCategory
            {
                Name = "Процессоры", Picture = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\categories\processor.png")
            };
            var pcMotherboard = new ProductCategory
            {
                Name = "Материнские платы", Picture = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\categories\motherboard.png")
            };
            var pcVideocard = new ProductCategory
            {
                Name = "Видеокарты", Picture = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\categories\videocard.png")
            };
            var pcRam = new ProductCategory
            {
                Name = "Оперативная память", Picture = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\categories\ram.png")
            };
            var pcRom = new ProductCategory
            {
                Name = "Жёсткие диски", Picture = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\categories\rom.png")
            };
            var pcCase = new ProductCategory
            {
                Name = "Корпуса", Picture = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\categories\case.png")
            };
            ProductCategories.AddRange(pcProcessor, pcMotherboard, pcVideocard, pcRam, pcRom, pcCase);


            var mAmd = new Manufacturer { Name = "AMD" };
            var mIntel = new Manufacturer { Name = "Intel" };
            Manufacturers.AddRange(mAmd, mIntel);

            var pAthlon3000G = new Picture
                { Resource = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\processors\Athlon 3000G.png") };
            var pI310105F = new Picture
                { Resource = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\processors\Core i3-10105F.png") };
            var pRyzen54500 = new Picture
                { Resource = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\processors\Ryzen 5 4500.png") };
            var pI312100F = new Picture
                { Resource = FileConverter.GetBinaryFile(@"..\TechDrop.Data\src\processors\Core i3-12100F.png") };
            Pictures.AddRange(pAthlon3000G, pI310105F, pRyzen54500, pI312100F);

            var userPsn = new User { Email = "psn@ya.ru", Password = "123123", Balance = 0 };
            Users.AddRange(userPsn);

            SaveChanges();

            #endregion

            #region заполнение Orders, Processor

            var oPsn1 = new Order
            {
                Cost = 13800,
                UserId = userPsn.UserId,
                OrderStatusId = osClosed.OrderStatusId
            };

            Orders.AddRange(oPsn1);

            var prAthlon3000G = new Processor
            {
                Model = "Athlon 3000G",
                SocketId = sAm4.SocketId,
                Year = 2019,
                CoolingSystem = false,
                Cores = 2,
                Threads = 4,
                PerformanceCores = 2,
                EnergyCores = 0,
                L2 = 1,
                L3 = 4,
                TechProcess = 14,
                BaseFrequency = 3.5,
                FreeMultiplier = true,
                RamCapacity = 64,
                RamChannels = 2,
                RamMaxFrequency = 2666,
                Tdp = 35,
                MaxTemp = 95,
                GraphCoreId = gcVega3.GraphCoreId,
                PciExpressId = pe3.PciExpressId,
                PciExpressLines = 16
            };
            var prI310105F = new Processor
            {
                Model = "Core i3-10105F",
                SocketId = sLga1200.SocketId,
                Year = 2020,
                CoolingSystem = false,
                Cores = 4,
                Threads = 8,
                PerformanceCores = 4,
                EnergyCores = 0,
                L2 = 1,
                L3 = 6,
                TechProcess = 14,
                BaseFrequency = 3.7,
                MaxFrequency = 4.4,
                FreeMultiplier = false,
                RamCapacity = 128,
                RamChannels = 2,
                RamMaxFrequency = 2666,
                Tdp = 65,
                MaxTemp = 100,
                PciExpressId = pe3.PciExpressId,
                PciExpressLines = 16
            };
            var prRyzen54500 = new Processor
            {
                Model = "Ryzen 5 4500",
                SocketId = sAm4.SocketId,
                Year = 2019,
                CoolingSystem = false,
                Cores = 6,
                Threads = 12,
                PerformanceCores = 6,
                EnergyCores = 0,
                L2 = 3,
                L3 = 8,
                TechProcess = 7,
                BaseFrequency = 3.6,
                MaxFrequency = 4.1,
                FreeMultiplier = true,
                RamCapacity = 128,
                RamChannels = 2,
                RamMaxFrequency = 3200,
                Tdp = 65,
                MaxTemp = 95,
                PciExpressId = pe3.PciExpressId,
                PciExpressLines = 24
            };
            var prI312100F = new Processor
            {
                Model = "Core i3-12100F",
                SocketId = sLga1700.SocketId,
                Year = 2022,
                CoolingSystem = false,
                Cores = 4,
                Threads = 8,
                PerformanceCores = 4,
                EnergyCores = 0,
                L2 = 5,
                L3 = 12,
                TechProcess = 10,
                BaseFrequency = 3.3,
                MaxFrequency = 4.3,
                FreeMultiplier = false,
                RamCapacity = 128,
                RamChannels = 2,
                RamMaxFrequency = 4800,
                Tdp = 89,
                MaxTemp = 100,
                PciExpressId = pe5.PciExpressId,
                PciExpressLines = 20
            };
            Processors.AddRange(prAthlon3000G, prI310105F, prRyzen54500, prI312100F);

            SaveChanges();

            #endregion

            #region заполнение ProcessorRamTypes

            var prt1 = new ProcessorRamType
            {
                ProcessorId = prAthlon3000G.ProcessorId,
                RamTypeId = rtDdr4.RamTypeId,
                Processor = prAthlon3000G,
                RamType = rtDdr4
            };
            var prt2 = new ProcessorRamType
            {
                ProcessorId = prI310105F.ProcessorId,
                RamTypeId = rtDdr4.RamTypeId,
                Processor = prI310105F,
                RamType = rtDdr4
            };
            var prt3 = new ProcessorRamType
            {
                ProcessorId = prRyzen54500.ProcessorId,
                RamTypeId = rtDdr4.RamTypeId,
                Processor = prRyzen54500,
                RamType = rtDdr4
            };
            var prt4 = new ProcessorRamType
            {
                ProcessorId = prI312100F.ProcessorId,
                RamTypeId = rtDdr4.RamTypeId,
                Processor = prI312100F,
                RamType = rtDdr4
            };
            var prt5 = new ProcessorRamType
            {
                ProcessorId = prI312100F.ProcessorId,
                RamTypeId = rtDdr5.RamTypeId,
                Processor = prI312100F,
                RamType = rtDdr5
            };
            ProcessorRamTypes.AddRange(prt1, prt2, prt3, prt4, prt5);
            
            SaveChanges();

            #endregion

            #region заполнение Products

            var athlon3000G = new Product
            {
                Description =
                    "Процессор AMD Athlon 3000G OEM с поддержкой технологии виртуализации вполне подходит для" +
                    " установки в домашний или рабочий компьютер. 2-ядерная архитектура процессора," +
                    " созданная на основе техпроцесса GlobalFoundries 14LPP, обеспечивает высокую скорость" +
                    " выполнения заданных операций в 4-поточном режиме. Уровень тепловыделения при этом будет" +
                    " всего 35 Вт. Базовую рабочую частоту в 3.5 ГГц можно увеличить благодаря разблокированному" +
                    " множителю, что позволит повысить тем самым производительность процессора. Процессор AMD" +
                    " Athlon 3000G OEM работает с оперативной памятью формата DDR4, объемом до 64 ГБ. Встроенное" +
                    " графическое ядро с максимальной частотой 1100 МГц предусматривает высокое качество графики" +
                    " при просмотре видео или работе в офисных программах. 6-линейный встроенный контроллер" +
                    " PCI-E 3.0 обеспечивает стабильную и высокоскоростную передачу данных при подключении" +
                    " процессора к материнской плате. Виртуализация позволит расширить функциональность компьютера," +
                    " запустив на нем другую ОС.",
                Cost = 6800,
                Quantity = 6,
                Discount = 0,
                ProductCategoryId = pcProcessor.ProductCategoryId,
                ManufacturerId = mAmd.ManufacturerId,
                ProcessorId = prAthlon3000G.ProcessorId,
                ProductCategory = pcProcessor,
                Manufacturer = mAmd,
                Processor = prAthlon3000G
            };
            var i310105F = new Product
            {
                Description =
                    "Процессор Intel Core i3-10105F можно использовать в составе различных сборок благодаря" +
                    " сбалансированной производительности, обеспеченной 4-ядерной конфигурацией и работой с" +
                    " частотой 3.7-4.4 ГГц. Благодаря применению при создании чипсета 14-нанометрового техпроцесса" +
                    " он обладает низким тепловыделением 65 Вт и небольшим энергопотреблением. В конфигурации модели" +
                    " присутствуют только производительные ядра, которые позволяют модели одновременно выполнять до" +
                    " 8 вычислительных операций. Процессор Intel Core i3-10105F совместим с оперативной памятью " +
                    "DDR4 объемом до 128 ГБ и поддерживает двухканальный режим работы ОЗУ. В устройстве " +
                    "используется встроенный контроллер PCI-E 3.0 с 16 линиями со скоростью передачи каждой " +
                    "до 1 ГБ/с. Для подключения к материнской плате модель использует сокет LGA 1200.",
                Cost = 7000,
                Quantity = 11,
                Discount = 0,
                ProductCategoryId = pcProcessor.ProductCategoryId,
                ManufacturerId = mIntel.ManufacturerId,
                ProcessorId = prI310105F.ProcessorId,
                ProductCategory = pcProcessor,
                Manufacturer = mIntel,
                Processor = prI310105F
            };
            var ryzen54500 = new Product
            {
                Description =
                    "Процессор AMD Ryzen 5 4500 OEM способен стать основой геймерского ноутбука или ПК. Для этого " +
                    "он имеет 6 ядер с 12 потоками. Процессор выполнен на архитектуре Zen 2 и изготовлен по " +
                    "технологии 7 нм. Базовая частота устройства составляет 3.6 ГГц при возможности разгона до " +
                    "4.1 ГГц. Оно также поддерживает технологии Precision Boost и Precision Boost Overdrive для " +
                    "максимального повышения производительности в автоматическом режиме. Поддержка технологии " +
                    "виртуализации – еще один плюс AMD Ryzen 5 4500 OEM, который способны оценить геймеры и " +
                    "другие опытные пользователи.",
                Cost = 8500,
                Quantity = 4,
                Discount = 0,
                ProductCategoryId = pcProcessor.ProductCategoryId,
                ManufacturerId = mAmd.ManufacturerId,
                ProcessorId = prRyzen54500.ProcessorId,
                ProductCategory = pcProcessor,
                Manufacturer = mAmd,
                Processor = prRyzen54500
            };
            var i312100F = new Product
            {
                Description =
                    "4-ядерный процессор Intel Core i3-12100F OEM – отличный выбор для пользователей, желающих собрать" +
                    " универсальный компьютер для дома или офиса. Производительность модели достаточна для эффективной" +
                    " работы с типовыми программами. Преимуществом процессора является высокая базовая частота – 3.3 ГГц." +
                    " В турборежиме частота может расти до 4.3 ГГц. Процессор Intel Core i3-12100F OEM отличается " +
                    "большим объемом кэш-памяти. В наличии 5-мегабайтный кэш L2 и 12-мегабайтный кэш L3. " +
                    "Кэш-память влияет на скорость выполнения любых операций. Совместимые типы оперативной памяти – " +
                    "DDR4 и DDR5. Объем ОЗУ может достигать 128 ГБ. TDP процессора – 89 Вт. Показатель учитывается " +
                    "при выборе устройства теплоотвода. В комплектацию модели кулер не включен. Максимальная рабочая " +
                    "температура процессора – 100.",
                Cost = 9800,
                Quantity = 13,
                Discount = 0,
                ProductCategoryId = pcProcessor.ProductCategoryId,
                ManufacturerId = mIntel.ManufacturerId,
                ProcessorId = prI312100F.ProcessorId,
                ProductCategory = pcProcessor,
                Manufacturer = mIntel,
                Processor = prI312100F
            };
            Products.AddRange(athlon3000G, i310105F, ryzen54500, i312100F);

            SaveChanges();

            #endregion

            #region заполнение таблиц-связей(кроме processorRamTypes)

            var picAthlon3000G = new ProductPicture
            {
                ProductId = athlon3000G.ProductId,
                PictureId = pAthlon3000G.PictureId,
                Product = athlon3000G,
                Picture = pAthlon3000G
            };
            var picI310105F = new ProductPicture
            {
                ProductId = i310105F.ProductId,
                PictureId = pI310105F.PictureId,
                Product = i310105F,
                Picture = pI310105F
            };
            var picRyzen54500 = new ProductPicture
            {
                ProductId = ryzen54500.ProductId,
                PictureId = pRyzen54500.PictureId,
                Product = ryzen54500,
                Picture = pRyzen54500
            };
            var picI312100F = new ProductPicture
            {
                ProductId = i312100F.ProductId,
                PictureId = pI312100F.PictureId,
                Product = i312100F,
                Picture = pI312100F
            };
            ProductPictures.AddRange(picAthlon3000G, picI310105F, picRyzen54500, picI312100F);

            
            var opPsn1 = new OrderProduct
            {
                ProductId = athlon3000G.ProductId,
                Quantity = 1,
                OrderId = oPsn1.OrderId,
                Order = oPsn1,
                Product = athlon3000G
            };
            var opPsn2 = new OrderProduct
            {
                ProductId = i310105F.ProductId,
                Quantity = 1,
                OrderId = oPsn1.OrderId,
                Order = oPsn1,
                Product = i310105F
            };
            OrderProducts.AddRange(opPsn1, opPsn2);


            var cPsn1 = new Cart
            {
                Quantity = 2,
                UserId = userPsn.UserId,
                ProductId = ryzen54500.ProductId,
                User = userPsn,
                Product = ryzen54500
            };
            var cPsn2 = new Cart
            {
                Quantity = 1,
                UserId = userPsn.UserId,
                ProductId = i312100F.ProductId,
                User = userPsn,
                Product = i312100F
            };
            Carts.AddRange(cPsn1,cPsn2);

            SaveChanges();

            #endregion

        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TechDrop;Username=postgres;Password=123");
    }
}