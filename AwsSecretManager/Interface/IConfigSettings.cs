namespace AwsSecretManager.Interface;

public interface IConfigSettings
{
    string DbSecret
    {
        get;
        set;
    }
    string AwsAccessKey
    {
        get;
        set;
    }
    string AwsSecretKey
    {
        get;
        set;
    }
    string AwsSessionToken
    {
        get;
        set;
    }
}