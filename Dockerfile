FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./GreetingClassLibrary/. ./GreetingClassLibrary/
RUN dotnet restore GreetingClassLibrary/

COPY GreetingApp/*.csproj ./GreetingApp/
RUN dotnet restore GreetingApp/

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "GreetingApp/out/GreetingApp.dll"]