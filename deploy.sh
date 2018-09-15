cd ~/NetCore/Guide/src/Guide
dotnet publish -r linux-arm -c Release
ssh pi@192.168.0.101 "pkill guide"
scp -r ~/NetCore/Guide/src/Guide/bin/Release/netcoreapp2.1/linux-arm/publish/* pi@192.168.0.101:~/bots/Guide
ssh pi@192.168.0.101 "~/bots/Guide/Guide &"
