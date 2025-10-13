# DataPersistence

## Status
![](https://img.shields.io/badge/unity-2022.3+-000.svg)
![Tests](https://img.shields.io/badge/Tests-Passed-brightgreen.svg)

## Description
DataPersistence is a robust and flexible data saving and loading system for Unity, providing secure and efficient data storage with support for multiple storage backends, encryption, and serialization methods.

## Table of Contents
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Manual Installation](#manual-installation)
    - [UPM Installation](#upm-installation)
- [Features](#features)
- [Basic Usage](#basic-usage)
- [Advanced Usage](#advanced-usage)
  - [Custom Storage Providers](#custom-storage-providers)
  - [Custom Cryptography](#custom-cryptography)
  - [Custom Serializers](#custom-serializers)
- [API Reference](#api-reference)
  - [ISaveService](#isaveservice)
- [Testing](#testing)
- [License](#license)

## Getting Started
### Prerequisites
- [GIT](https://git-scm.com/downloads)
- [Unity](https://unity.com/releases/editor/archive) 2022.3+

### Manual Installation
1. Download the .unitypackage from the [releases](https://github.com/DanilChizhikov/DataPersistence/releases/) page.
2. Import com.dtech.data-persistence.x.x.x.unitypackage into your project.

### UPM Installation
1. Open the manifest.json file in your project's Packages folder.
2. Add the following line to the dependencies section:
    ```json
    "com.dtech.data-persistence": "https://github.com/DanilChizhikov/DataPersistence.git",
    ```
3. Unity will automatically import the package.

If you want to set a target version, DataPersistence uses the `v*.*.*` release tag so you can specify a version like #v0.1.0.

For example `https://github.com/DanilChizhikov/DataPersistence.git#v0.1.0`.

## Features
- 🔒 Secure data encryption (AES-256)
- 🗄️ Multiple storage providers (Binary, PlayerPrefs)
- 🔄 JSON serialization
- 🧪 Comprehensive test coverage
- ⚡ Asynchronous operations
- 🛡️ Type-safe data handling

## Basic Usage
```csharp
using DTech.DataPersistence;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ISaveService _saveService;

    private void Start()
    {
        // Initialize with default configuration
        var serializer = new UnityJsonSerializer();
        var storage = new BinaryStorageProvider(new BinaryProviderOptions(".save"));
        var crypto = new AesCryptographer(new AesConfig("your-secure-password"));
        
        _saveService = new SaveService(serializer, crypto, storage);
        
        // Save data
        SaveGameData();
    }

    private async void SaveGameData()
    {
        var gameData = new GameData
        {
            PlayerName = "Player1",
            Score = 1000,
            Level = 5
        };

        await _saveService.SaveAsync("save1", gameData);
        Debug.Log("Game saved successfully!");
    }

    private async Task<GameData> LoadGameData()
    {
        var defaultData = new GameData();
        return await _saveService.LoadAsync("save1", defaultData);
    }
}

[System.Serializable]
public class GameData
{
    public string PlayerName;
    public int Score;
    public int Level;
}
```

## Advanced Usage

### Custom Storage Providers
Implement `IStorageProvider`to create your own storage solution:
```csharp
public class CustomStorageProvider : IStorageProvider
{
    public bool ContainsKey(string key) { /* ... */ }
    public Task<bool> WriteAsync(string key, string value) { /* ... */ }
    public Task<StorageReadResponse> ReadAsync(string key, string defaultValue) { /* ... */ }
    public void Remove(string key) { /* ... */ }
}
```

### Custom Cryptography
Implement `ICryptographer`to create your own cryptography solution:
```csharp
public class CustomCryptographer : ICryptographer
{
    public string Encrypt(string data) { /* ... */ }
    public string Decrypt(string data) { /* ... */ }
}
```

### Custom Serializers
Implement `ISerializer`to create your own serialization solution:
```csharp
public class CustomSerializer : ISerializer
{
    public string Serialize(object obj) { /* ... */ }
    public T Deserialize<T>(string data) { /* ... */ }
}
```

## API Reference

### ISaveService
- `Task SaveAsync<T>(string key, T value, bool isCrypted = true)` - Saves data with the specified key
- `Task<T> LoadAsync<T>(string key, T defaultValue)` - Loads data by key
- `bool HasSave(string key)` - Checks if data exists for the key
- `void Remove(string key)` - Removes data for the key

## Testing
The project includes a comprehensive set of unit tests that verify the correct operation of the object pool. All tests are passing successfully.

To run the tests:
1. Open the Test Runner window (Window > General > Test Runner)
2. Select PlayMode tests
3. Click "Run All"

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.