using System.Xml;

namespace FootballStats.Database;

public static class ConfigHandler
{
    private static string Database { get; set; }
    private static string Username { get; set; }
    private static string Password { get; set; }
    private static string Address { get; set; }
    private static string Port { get; set; }

    public static readonly string ConnectionString;

    static ConfigHandler()
    {
        XmlDocument doc = new();
        doc.Load("Config.xml");

        var node = doc.SelectSingleNode("/configuration/connectionStrings/KFL55");

        Database = node.Attributes["database"].Value;
        Username = node.Attributes["username"].Value;
        Password = node.Attributes["password"].Value;
        Address = node.Attributes["address"].Value;
        Port = node.Attributes["port"].Value;

        ConnectionString = $"Server={Address};Port={Port};Database={Database};User Id={Username};Password={Password};";
    }
}