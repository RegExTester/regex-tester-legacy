FROM microsoft/aspnetcore
LABEL name="regex-tester-api-dotnet"
ENTRYPOINT ["dotnet", "RegExTester.Api.DotNet.dll"]
ARG source=.
WORKDIR /app
EXPOSE 80
COPY $source .