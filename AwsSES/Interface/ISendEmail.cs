namespace AwsSES.Interface;

public interface ISendEmail
{
    public Task<bool> send(string awsAccessKey,string awsSecretKey,string awsSessionToken,int managerId);
}