FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AttendanceTracking/AttendanceTracking.csproj", "AttendanceTracking/"]
COPY ["AwsS3/AwsS3.csproj", "AwsS3/"]
COPY ["AwsSecretManager/AwsSecretManager.csproj", "AwsSecretManager/"]
COPY ["AwsSES/AwsSES.csproj", "AwsSES/"]
RUN dotnet restore "AttendanceTracking/AttendanceTracking.csproj"
COPY . .
WORKDIR "/src/AttendanceTracking"
RUN dotnet build "AttendanceTracking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AttendanceTracking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AttendanceTracking.dll"]