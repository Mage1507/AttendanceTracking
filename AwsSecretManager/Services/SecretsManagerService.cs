using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace AwsSecretManager.Services;

public class SecretsManagerService
{
    public static string GetSecret()
    {
        string secretName = "attendance-tracking";
        string region = "us-east-1";
        string secret = "";
        MemoryStream memoryStream = new MemoryStream();
        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
        GetSecretValueRequest request = new GetSecretValueRequest();
        request.SecretId = secretName;
        request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.
        GetSecretValueResponse response = null;
        try
        {
            response = client.GetSecretValueAsync(request).Result;
        }
        catch (Exception e)
        {
            throw e;
        }

        if (response.SecretString != null)
        {
            return secret = response.SecretString;
        }
        else
        {
            memoryStream = response.SecretBinary;
            StreamReader reader = new StreamReader(memoryStream);
            string decodedBinarySecret =
                System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            return decodedBinarySecret;
        }
    }
}