using Norm.Library.Concrete;
using NormTest.Console.Models;

SqlServerQuery sqlServerQuery = new SqlServerQuery();

var result = await sqlServerQuery.QueryAsync<List<UserVM>>(new Norm.Library.Core.Models.Query
{
    TableName = "Users",
    Column = "FirstName",
    Size = 50,
}, new Norm.Library.Core.Models.SqlConnectionModel
{
    Database = "LoginExample",
    IntegratedSecurity = true,
    Password = "",
    Server = "DESKTOP-0Q6K9DL",
    UserId = ""
});

foreach (var user in result)
{
    Console.WriteLine(user.FirstName);
}