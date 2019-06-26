FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out
FROM microsoft/dotnet:aspnetcore-runtime AS runtime-env
WORKDIR /app
COPY --from=build-env /app/src/Chat.Ui/out .
ENTRYPOINT ["dotnet", "Chat.Ui.dll"]
