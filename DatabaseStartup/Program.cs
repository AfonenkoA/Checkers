using DatabaseStartup.Declaration;
using static System.Console;

Write(
    @$"GO
CREATE DATABASE Checkers;

--Declaration
{Total.Table}
{Total.Function}
--Filling
{DatabaseStartup.Filling.Total.Filling}");