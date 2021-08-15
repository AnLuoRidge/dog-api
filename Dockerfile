FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY /release/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "dog-api.dll"]
EXPOSE 5000