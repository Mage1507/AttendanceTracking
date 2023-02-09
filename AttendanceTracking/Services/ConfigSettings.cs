using Newtonsoft.Json.Linq;

namespace AttendanceTracking.Services;


public class ConfigSettings : IConfigSettings
{
    private string _dbSecret;
    public ConfigSettings()
    {
        Init();
    }
    public string Init()
    {
        var secretValues = JObject.Parse(SecretsManagerService.GetSecret());
        if (secretValues != null)
        {
            _dbSecret = secretValues["mssql"].ToString();
        }

        return _dbSecret;
    }
    public string DbSecret
    {
        get
        {
            return _dbSecret;
        }
        set
        {
            _dbSecret = value;
        }
    }

}
public interface IConfigSettings
{
    string DbSecret
    {
        get;
        set;
    }

}
