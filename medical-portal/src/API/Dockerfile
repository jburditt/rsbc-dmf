ARG BUILD_ID
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG BUILD_ID

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY . .
RUN dotnet restore "./medical-portal/src/API/RSBC.DMF.MedicalPortal.API/"
RUN dotnet test "./medical-portal/src/API/RSBC.DMF.MedicalPortal.API/" /p:InformationalVersion="$BUILD_ID" -c Release

FROM build AS publish
ARG BUILD_ID

RUN dotnet publish /p:InformationalVersion="$BUILD_ID" "./medical-portal/src/API/RSBC.DMF.MedicalPortal.API/RSBC.DMF.MedicalPortal.API.csproj" -c Release -o /app/publish --no-restore 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "RSBC.DMF.MedicalPortal.API.dll"]