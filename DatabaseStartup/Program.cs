using DatabaseStartup.Filling;
using static System.Console;
using Total = DatabaseStartup.Declaration.Total;

Write(
    @$"GO
CREATE DATABASE Checkers;

GO
USE Checkers;
--Declaration
{Total.Table}
{Total.Function}
--Filling
{Records.Total}");