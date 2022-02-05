using StackExchange.Profiling.Storage;

SqlServerStorage sqlServerStorage = new("");
var sqlScripts = sqlServerStorage.TableCreationScripts;

sqlScripts.ForEach(Console.WriteLine);

Console.ReadLine();