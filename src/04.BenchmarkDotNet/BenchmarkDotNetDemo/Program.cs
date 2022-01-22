using BenchmarkDotNet.Running;

BenchmarkSwitcher
    .FromAssembly(typeof(Program).Assembly)
    .Run(args);

//dotnet run --configuration Release --framework net48 --runtimes net48 netcoreapp31 netcoreapp50 net60 --filter * --join