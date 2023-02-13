using AwsSecretManager.Interface;
using AwsSecretManager.Services;
using Newtonsoft.Json.Linq;

namespace AwsSecretManager.Settings;

public class ConfigSettings : IConfigSettings
{
    private string _dbSecret;
    private string _awsAccessKey;
    private string _awsSecretKey;
    private string _awsSessionToken;
    public ConfigSettings()
    {
        Init();
    }
    public void Init()
    {
        var secretValues = JObject.Parse(SecretsManagerService.GetSecret());
        if (secretValues != null)
        {
            _dbSecret = secretValues["mssql"].ToString();
            _awsAccessKey = secretValues["AWSAccessKey"].ToString();
            _awsSecretKey = secretValues["AWSSecretKey"].ToString();
            _awsSessionToken = secretValues["AWSSessionToken"].ToString();
            
        }
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
    public string AwsAccessKey
    {
        get
        {
            return _awsAccessKey;
        }
        set
        {
            _awsAccessKey = value;
        }
    }
    public string AwsSecretKey
    {
        get
        {
            return _awsSecretKey;
        }
        set
        {
            _awsSecretKey = value;
        }
    }
    public string AwsSessionToken
    {
        get
        {
            return _awsSessionToken;
        }
        set
        {
            _awsSessionToken = value;
        }
    }
}