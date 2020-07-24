# Build image
FROM microsoft/dotnet AS build-env
RUN mkdir -p /usr/local/src/tetrix \
 && mkdir -p /usr/local/bin/tetrix
WORKDIR /usr/local/src/tetrix
ADD . .
RUN dotnet publish tetrix.csproj --output /usr/local/bin/tetrix -c Release

# Runtime image
FROM microsoft/dotnet AS runtime-env
RUN mkdir /game
WORKDIR /game
COPY --from=build-env /usr/local/bin/tetrix .

ENTRYPOINT ["dotnet", "src.dll"]
