using Norm.Library.Concrete;
using NormTest.Console.Models;

SqlServerQuery sqlServerQuery = new SqlServerQuery();

var result = await sqlServerQuery.QueryAsync<UserVM>(new Norm.Library.Core.Models.Query
{
    TableName = "Users",
    Columns = new string[] { "Email", "Phone" },
    Size = 50,
}, new Norm.Library.Core.Models.SqlConnectionModel
{
    Database = "LoginExample",
    IntegratedSecurity = true,
    Password = "",
    Server = "DESKTOP-0Q6K9DL",
    UserId = ""
});


Console.WriteLine(result.Email + " " + result.Phone);

//foreach (var user in result)
//{
//    Console.WriteLine(user.Email + " " + user.Phone);
//}