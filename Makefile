all: build

build:
	dotnet publish -p:PublishSingleFile=true -p:PublishTrimmed=true -r linux-arm64 --self-contained=true -c Release
