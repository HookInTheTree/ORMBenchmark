using FactoryMethod;
using FactoryMethod.Controllers;
using FactoryMethod.Data;
using FactoryMethod.Interfaces;
using FactoryMethod.Models;
using System.Collections;
using System.Diagnostics;

(string, int)[] testData = new (string, int)[]
{
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark10000;", 10000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark20000;", 20000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark30000;", 30000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark40000;", 40000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark50000;", 50000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark60000;", 60000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark70000;", 70000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark80000;", 80000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark90000;", 90000),
    //("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark100000;", 100000)
};

Stopwatch stopwatch = new Stopwatch();

List<ICrudableInt> controllersInt;

List<ICrudableGuid> controllersGuid;

List<ICrudableNpgsqlGuid> controllersNpgsqlGuid;

List<ICrudableNpgsqlInt> controllersNpgsqlInt;

List<Point> SelectResults = new List<Point>();
List<Point> SelectWhereResults = new List<Point>();
List<Point> UpdateWhereResutls = new List<Point>();
int iterationsCount = 1000;

foreach (var data in testData)
{
    Console.WriteLine();
    Console.WriteLine(data.Item1);
    controllersInt = new List<ICrudableInt>()
    {
        new DapperInt(data.Item1),
        new EfCoreIntTracking(new ApplicationDbContextFactory().CreateDbContext(data.Item1)),
        new EfCoreIntNoTracking(new ApplicationDbContextFactory().CreateDbContext(data.Item1))
    };
    controllersGuid = new List<ICrudableGuid>()
    {
        new DapperGuid(data.Item1),
        new EfCoreGuidTracking(new ApplicationDbContextFactory().CreateDbContext(data.Item1)),
        new EfCoreGuidNoTracking(new ApplicationDbContextFactory().CreateDbContext(data.Item1))
    };
    controllersNpgsqlGuid = new List<ICrudableNpgsqlGuid>()
    {
        new NpgsqlDataAdapterGuid(data.Item1),
        new NpgsqlCommandGuid(data.Item1)
    };
    controllersNpgsqlInt = new List<ICrudableNpgsqlInt>()
    {
        new NpgsqlCommandInt(data.Item1),
        new NpgsqlDataAdapterInt(data.Item1)
    };

    List<Person2> personsToUpdate = controllersGuid[0].Select().Take(1000).ToList();
    //Guid
    foreach (var controller in controllersGuid)
    {
        stopwatch.Start();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.Select();
        }
        stopwatch.Stop();
        SelectResults.Add(new Point()
        {
            Name = controller.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(controller.Name + "  Select was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.SelectWhere("Новый1");
        }
        stopwatch.Stop();
        SelectWhereResults.Add(new Point()
        {
            Name = controller.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(controller.Name + "  SelectWhere was finished");


        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.UpdateWhere(personsToUpdate[i].Id, "Обновлено");
        }
        stopwatch.Stop();
        UpdateWhereResutls.Add(new Point()
        {
            Name = controller.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(controller.Name + "  UpdateWhere was finished");
    }

    //Int
    foreach (var intController in controllersInt)
    {
        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.Select();
        }
        stopwatch.Stop();
        SelectResults.Add(new Point()
        {
            Name = intController.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(intController.Name + "  Select was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.SelectWhere("Новый1");
        }
        stopwatch.Stop();
        SelectWhereResults.Add(new Point()
        {
            Name = intController.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(intController.Name + "  SelectWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.UpdateWhere(i, "Обновлено");
        }
        stopwatch.Stop();
        UpdateWhereResutls.Add(new Point()
        {
            Name = intController.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(intController.Name + "  UpdateWhere was finished");

    }

    //Npgsql Guid
    foreach (var npgsqlGuidController in controllersNpgsqlGuid)
    {
        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.Select();
        }
        stopwatch.Stop();
        SelectResults.Add(new Point()
        {
            Name = npgsqlGuidController.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(npgsqlGuidController.Name + "  Select was finished");


        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.SelectWhere("Новый1");
        }
        stopwatch.Stop();
        SelectWhereResults.Add(new Point()
        {
            Name = npgsqlGuidController.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(npgsqlGuidController.Name + "  Select was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.UpdateWhere(personsToUpdate[i].Id, "Обновлено");
        }
        stopwatch.Stop();
        UpdateWhereResutls.Add(new Point()
        {
            Name = npgsqlGuidController.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(npgsqlGuidController.Name + "  Select was finished");
    }

    //Npgsql Int
    foreach (var npgsqlIntController in controllersNpgsqlInt)
    {
        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.Select();
        }
        stopwatch.Stop();
        SelectResults.Add(new Point()
        {
            Name = npgsqlIntController.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(npgsqlIntController.Name + "  Select was finished");


        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.SelectWhere("Новый1");
        }
        stopwatch.Stop();
        SelectWhereResults.Add(new Point()
        {
            Name = npgsqlIntController.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(npgsqlIntController.Name + "  Select was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.UpdateWhere(i, "Обновлено");
        }
        stopwatch.Stop();
        UpdateWhereResutls.Add(new Point()
        {
            Name = npgsqlIntController.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = iterationsCount
        });
        Console.WriteLine(npgsqlIntController.Name + "  Select was finished");
    }
}

Console.WriteLine();
Console.ReadKey();