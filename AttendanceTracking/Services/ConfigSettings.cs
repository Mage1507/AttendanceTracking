using Newtonsoft.Json.Linq;

namespace AttendanceTracking.Services;


public class ConfigSettings : IConfigSettings
{
    private string _key1;
    private string _key2;
    public ConfigSettings()
    {
        Init();
    }
    public string Init()
    {
        var secretValues = JObject.Parse(SecretsManagerService.GetSecret());
        if (secretValues != null)
        {
            _key1 = secretValues["mssql"].ToString();
        }

        return _key1;
    }
    public string Key1
    {
        get
        {
            return _key1;
        }
        set
        {
            _key1 = value;
        }
    }

}
public interface IConfigSettings
{
    string Key1
    {
        get;
        set;
    }

}
