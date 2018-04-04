# Service manager

Service manager configurable for any Windows service

Basic configuration file

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="Services" type="QualisysServiceManager.Sections.ServiceSection, QualisysServiceManager" />
  </configSections>
  <Services>
    <Service
      Index="1"
      DisplayName="Qualisys Test Service"
      Name="QualisysTestService"
    />
  </Services>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
</configuration>
```

Administration

![Qualisys Service Manager - Administration](https://github.com/anayarojo/qualisys-service-manager/blob/master/img/administration-20180404.JPG)

Log viewer

![Qualisys Service Manager - Log Viewer](https://github.com/anayarojo/qualisys-service-manager/blob/master/img/log-viewer-20180404.JPG)

Configuration

![Qualisys Service Manager - Configuration](https://github.com/anayarojo/qualisys-service-manager/blob/master/img/configuration-20180404.JPG)

