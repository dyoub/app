# Dyoub App

Online application for commercial business.

[![Build status](https://ci.appveyor.com/api/projects/status/5hjvkkjw2ec3sm1m?svg=true)](https://ci.appveyor.com/project/marxjmoura/app)

## Before you start

- Install Visual Studio 2015 and Web Essentials
- Install MySQL Database

## How to run

1. Copy `Settings.Sample` to `Settings` (do not remove `Settings.Sample`)

2. Configure the connection string on `Settings/Dyoub.App.config` (if needed)

3. Open Package Manager Console window from Visual Studio menubar:  
`TOOLS > NuGet Package Manger > Package Manager Console`

4. Select `Package source: All` and `Default project: Dyoub.App`

5. Type command to restore packages: `Update-Packages`

6. Type command to run migrations: `Update-Database`

7. Make sure that `Dyoub.App` is the starttup project and press F5

## How to test

1. Open Test Explorer window from Visual Studio menubar:  
`TEST > Windows > Test Explorer`

2. From test explorer window click on the link `Run all`
